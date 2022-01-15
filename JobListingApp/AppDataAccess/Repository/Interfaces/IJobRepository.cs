using JobListingApp.AppModels.Enums;
using JobListingApp.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Interfaces
{
    public interface IJobRepository : ICrudRepo
    {

        public Task<IEnumerable<Job>> GetJobs();
        public Task<IEnumerable<Job>> GetJobsByCategory(string categoryId);
        public Task<IEnumerable<Job>> GetJobsByLocation(Locations location);
        public Task<IEnumerable<Job>> GetJobsByIndustry(string industryId);
        public Task<IEnumerable<Job>> GetJobsByNature(JobNature jobNature);
        public Task<IEnumerable<Job>> GetJobsBySalaryRange(decimal minimum, decimal maximum);
        public Task<IEnumerable<Job>> GetJobsByName(string name);
        public Task<Job> GetJobById(string id);
        public Task<bool> JobExists(string jobTitle, string company);
    }
}
