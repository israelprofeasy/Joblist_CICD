using JobListingApp.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Interfaces
{
    public interface IIndustryRepository : ICrudRepo
    {
        Task<Industry> GetIndustryById(string id);
        Task<IEnumerable<Industry>> GetAll();
        Task<Industry> GetIndustryByName(string name);
        Task<IEnumerable<Industry>> GetIndustries(string name);
    }
}
