using JobListingApp.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Interfaces
{
    public interface ICVUpload : ICrudRepo
    {
        Task<CvUpload> GetUpload(string userId);
        Task<CvUpload> GetCvByPublicId(string PublicId);
        Task<IEnumerable<CvUpload>> GetAllUploads();
    }
}
