using JobListingApp.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Interfaces
{
    public interface IJobApplicationRepo : ICrudRepo
    {
        Task<IEnumerable<AppUser>> JobApplications(string JobId);
        Task<IEnumerable<Job>> UserApplications(string userId);
    }
}
