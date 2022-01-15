using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Interfaces
{
    public interface IJobServices
    {
        Task<JobPreviewDto> AddJob(JobDetailDto job);
        Task<bool> UpdateJob(string id, JobDetailDto job);
        Task<bool> RemoveJob(string id);
        Task<JobDetailReturnedDto> GetJobById(string id);
        Task<List<JobPreviewDto>> GetAllJobs();
        Task<List<JobPreviewDto>> GetJobsByCategory(string categoryId);
        Task<List<JobPreviewDto>> GetJobsByIndustry(string industryId);
        Task<List<JobPreviewDto>> GetJobsByLocation(Locations location);
        Task<List<JobPreviewDto>> GetJobsByNature(JobNature nature);
        Task<List<JobPreviewDto>> GetJobsBySalaryRange(decimal minimum, decimal maximum);
        Task<List<JobPreviewDto>> GetJobsByName(string name);
    }
}
