using JobListingApp.AppModels.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Interfaces
{
    public interface IIndustryService
    {
        Task<bool> RemoveIndustry(string id);
        Task<bool> UpdateIndustry(string id, IndustryDto industry);
        Task<IndustryReturnedDto> AddIndustry(IndustryDto industry);
        Task<List<IndustryReturnedDto>> GetAllIndustry();
        Task<List<IndustryReturnedDto>> GetIndustries(string name);
        Task<IndustryReturnedDto> GetIndustryById(string id);
        Task<IndustryReturnedDto> GetIndustryByName(string name);
    }
}
