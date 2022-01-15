using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Models;
using System;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Interfaces
{
    public interface IUploadService
    {
        public Task<Tuple<bool, UploadDto>> UploadCvAsync(UploadDto model, string userId);
        public Task<Tuple<bool, UploadDto>> AddCvAsync(UploadDto model, string userId);
        public Task<CvUpload> GetUserPhotosAsync(string userId);
        public Task<bool> DeletePhotoAsync(string PublicId);
    }
}
