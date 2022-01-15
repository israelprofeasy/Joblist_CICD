using JobListingApp.AppModels.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Interfaces
{
    public interface IApplication
    {
        Task<ApplicationResponseDto> ApplyForJob(string userId, string jobId);
        Task<List<UserAppliedDto>> JobApplications(string jobId);
        Task<List<JobsAppliedDto>> UserApplications(string userId);

    }
}
