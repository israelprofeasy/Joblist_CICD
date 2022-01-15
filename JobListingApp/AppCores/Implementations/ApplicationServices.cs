using AutoMapper;
using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppDataAccess.Repository.Interfaces;
using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Implementations
{
    public class ApplicationServices : IApplication
    {
        private readonly IJobApplicationRepo _jobApplicationRepo;
        private readonly IJobRepository _jobRepository;
        private readonly IUploadService _cVUpload;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ApplicationServices(IJobApplicationRepo jobApplicationRepo, IJobRepository jobRepository, IUploadService cVUpload, IMapper mapper, UserManager<AppUser> userManager)
        {
            _jobApplicationRepo = jobApplicationRepo;
            _jobRepository = jobRepository;
            _cVUpload = cVUpload;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<ApplicationResponseDto> ApplyForJob(string userId, string jobId)
        {
            var checkJob = await _jobRepository.GetJobById(jobId);
            if (checkJob == null)
            {
                return new ApplicationResponseDto { Success = false, Report = "Invalid Job Id" };
            }
            if (DateTime.Parse(checkJob.Deadline) < DateTime.Now)
            {
                return new ApplicationResponseDto { Success = false, Report = "Job Advert is already closed" };
            }
            var checkCv = _cVUpload.GetUserPhotosAsync(userId);
            if (checkCv == null)
            {
                return new ApplicationResponseDto { Success = false, Report = "You need to upload your Cv to you dashboard" };
            }

            try
            {
                var applplication = new JobApplication { AppUserId = userId, JobId = jobId };
                var res = await _jobApplicationRepo.Add(applplication);
                if (res)
                    return new ApplicationResponseDto { Success = true, Report = "Application successfully submitted" };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }

        public async Task<List<UserAppliedDto>> JobApplications(string jobId)
        {
            var checkJob = await _jobRepository.GetJobById(jobId);
            var listofApplicant = new List<UserAppliedDto>();
            if (checkJob != null)
            {
                try
                {
                    var users = await _jobApplicationRepo.JobApplications(jobId);
                    if (users != null)
                    {
                        foreach (var user in users)
                        {
                            listofApplicant.Add(_mapper.Map<UserAppliedDto>(user));
                        }
                        return listofApplicant;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public async Task<List<JobsAppliedDto>> UserApplications(string userId)
        {
            var checkUser = await _userManager.FindByIdAsync(userId);
            var listofJobs = new List<JobsAppliedDto>();
            if (checkUser != null)
            {
                try
                {
                    var jobs = await _jobApplicationRepo.UserApplications(userId);
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                        {
                            listofJobs.Add(_mapper.Map<JobsAppliedDto>(job));
                        }
                        return listofJobs;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }
    }
}
