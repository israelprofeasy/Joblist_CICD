using AutoMapper;
using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppDataAccess.Repository.Interfaces;
using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Enums;
using JobListingApp.AppModels.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Implementations
{
    public class JobServices : IJobServices
    {
        private readonly IJobRepository _jobRepo;
        private readonly ICategoryService _categoryServices;
        private readonly IIndustryService _industryService;
        private readonly IMapper _mapper;

        public JobServices(IJobRepository jobRepository, ICategoryService categoryServices, IIndustryService industryService, IMapper mapper)
        {
            _jobRepo = jobRepository;
            _categoryServices = categoryServices;
            _industryService = industryService;
            _mapper = mapper;
        }
        public async Task<JobPreviewDto> AddJob(JobDetailDto job)
        {
            var check = await _jobRepo.JobExists(job.JobTitle, job.Company);
            if (!check)
            {
                try
                {
                    var newJob = _mapper.Map<Job>(job);
                    var res = await _jobRepo.Add(newJob);
                    if (res)
                    {
                        return _mapper.Map<JobPreviewDto>(newJob);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return null;
        }

        public async Task<List<JobPreviewDto>> GetAllJobs()
        {
            var listOfJobs = new List<JobPreviewDto>();
            var count = await _jobRepo.RowCount();
            if (count > 0)
            {
                try
                {
                    var jobs = await _jobRepo.GetJobs();
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                        {
                            listOfJobs.Add(_mapper.Map<JobPreviewDto>(job));
                        }
                        return listOfJobs;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public async Task<JobDetailReturnedDto> GetJobById(string id)
        {
            try
            {
                var job = await _jobRepo.GetJobById(id);

                if (job != null)
                {
                    // var category =await _categoryServices.GetCategoryById(job.CategoryId);
                    // var industry = await _industryService.GetIndustryById(job.IndustryId);

                    var result = _mapper.Map<JobDetailReturnedDto>(job);
                    result.Category = job.Category.Name;//category.Name;
                    result.Industry = job.Industry.Name;// industry.Name;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;

        }

        public async Task<List<JobPreviewDto>> GetJobsByCategory(string categoryId)
        {
            var listOfJobs = new List<JobPreviewDto>();
            var count = await _jobRepo.RowCount();
            if (count > 0)
            {
                try
                {
                    var jobs = await _jobRepo.GetJobsByCategory(categoryId);
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                        {
                            listOfJobs.Add(_mapper.Map<JobPreviewDto>(job));
                        }
                        return listOfJobs;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public async Task<List<JobPreviewDto>> GetJobsByIndustry(string industryId)
        {
            var listOfJobs = new List<JobPreviewDto>();
            var count = await _jobRepo.RowCount();
            if (count > 0)
            {
                try
                {
                    var jobs = await _jobRepo.GetJobsByIndustry(industryId);
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                        {
                            listOfJobs.Add(_mapper.Map<JobPreviewDto>(job));
                        }
                        return listOfJobs;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public async Task<List<JobPreviewDto>> GetJobsByLocation(Locations location)
        {
            var listOfJobs = new List<JobPreviewDto>();
            var count = await _jobRepo.RowCount();
            if (count > 0)
            {
                try
                {
                    var jobs = await _jobRepo.GetJobsByLocation(location);
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                        {
                            listOfJobs.Add(_mapper.Map<JobPreviewDto>(job));
                        }
                        return listOfJobs;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public async Task<List<JobPreviewDto>> GetJobsByName(string name)
        {
            var listOfJobs = new List<JobPreviewDto>();
            var count = await _jobRepo.RowCount();
            if (count > 0)
            {
                try
                {
                    var jobs = await _jobRepo.GetJobsByName(name);
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                        {
                            listOfJobs.Add(_mapper.Map<JobPreviewDto>(job));
                        }
                        return listOfJobs;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public async Task<List<JobPreviewDto>> GetJobsByNature(JobNature nature)
        {
            var listOfJobs = new List<JobPreviewDto>();
            var count = await _jobRepo.RowCount();
            if (count > 0)
            {
                try
                {
                    var jobs = await _jobRepo.GetJobsByNature(nature);
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                        {
                            listOfJobs.Add(_mapper.Map<JobPreviewDto>(job));
                        }
                        return listOfJobs;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public async Task<List<JobPreviewDto>> GetJobsBySalaryRange(decimal minimum, decimal maximum)
        {
            var listOfJobs = new List<JobPreviewDto>();
            var count = await _jobRepo.RowCount();
            if (count > 0)
            {
                try
                {
                    var jobs = await _jobRepo.GetJobsBySalaryRange(minimum, maximum);
                    if (jobs != null)
                    {
                        foreach (var job in jobs)
                        {
                            listOfJobs.Add(_mapper.Map<JobPreviewDto>(job));
                        }
                        return listOfJobs;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return null;
        }

        public async Task<bool> RemoveJob(string id)
        {
            var job = await _jobRepo.GetJobById(id);
            if (job != null)
            {
                try
                {

                    var res = await _jobRepo.Delete(job); //_jobRepo.Delete(job);
                    if (res)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }

        public async Task<bool> UpdateJob(string id, JobDetailDto job)
        {
            var result = await _jobRepo.GetJobById(id);
            var success = false;
            if (result != null)
            {
                var updatedJob = _mapper.Map<Job>(job);
                updatedJob.Id = id;
                try
                {
                    var res = await _jobRepo.Update(updatedJob);
                    if (res)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return success;


            }
            return success;

        }
    }
}
