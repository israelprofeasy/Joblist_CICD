using JobListingApp.AppModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppDataAccess.Repository.Interfaces
{
    public interface ICategoryRepository : ICrudRepo
    {
        Task<Category> GetCategoryById(string id);
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetCategories(string name);
        Task<Category> GetCategoryByName(string name);
    }
}
