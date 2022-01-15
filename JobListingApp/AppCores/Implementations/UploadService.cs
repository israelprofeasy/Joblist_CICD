using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using JobListingApp.AppCommons;
using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppDataAccess.Repository.Interfaces;
using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Implementations
{
    public class UploadService : IUploadService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICVUpload _cvRepo;

        public UploadService(IOptions<CloudinarySettings> config,
            IMapper mapper, UserManager<AppUser> userManager,
            ICVUpload cvRepo)
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
            _mapper = mapper;
            _userManager = userManager;
            _cvRepo = cvRepo;
        }

        public async Task<Tuple<bool, UploadDto>> AddCvAsync(UploadDto model, string userId)
        {
            var res = false;
            try
            {
                var user = await _userManager.Users.Include(x => x.CvUpload).FirstOrDefaultAsync(x => x.Id == userId);

                var photo = _mapper.Map<CvUpload>(model);
                photo.AppUserId = userId;
                var userToUpdate = await _userManager.FindByIdAsync(userId);
                userToUpdate.CvUploadId = photo.Id;
                //photo.AppUser = this.


                // add photo to database
                res = await _cvRepo.Add(photo);
                if (res.Equals(true))
                    await _userManager.UpdateAsync(userToUpdate);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new Tuple<bool, UploadDto>(res, model);
        }

        public async Task<bool> DeletePhotoAsync(string PublicId)
        {
            DeletionParams destroyParams = new DeletionParams(PublicId)
            {
                ResourceType = ResourceType.Image
            };

            DeletionResult destroyResult = _cloudinary.Destroy(destroyParams);
            try
            {

                if (destroyResult.StatusCode.ToString().Equals("OK"))
                {
                    var photo = await _cvRepo.GetCvByPublicId(PublicId);
                    if (photo != null)
                    {
                        var res = await _cvRepo.Delete(photo);
                        if (res)
                            return true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }

        public async Task<CvUpload> GetUserPhotosAsync(string userId)
        {
            try
            {
                var res = await _cvRepo.GetUpload(userId);
                if (res != null)
                    return res;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public async Task<Tuple<bool, UploadDto>> UploadCvAsync(UploadDto model, string userId)
        {

            var uploadResult = new ImageUploadResult();

            using (var stream = model.Photo.OpenReadStream())
            {
                var imageUploadParams = new ImageUploadParams
                {
                    File = new FileDescription(model.Photo.FileName, stream),

                };

                uploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            }
            try
            {
                var status = uploadResult.StatusCode.ToString();

                if (status.Equals("OK"))
                {
                    model.PublicId = uploadResult.PublicId;
                    model.Url = uploadResult.Url.ToString();
                    return new Tuple<bool, UploadDto>(true, model);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new Tuple<bool, UploadDto>(false, model);
        }
    }
}
