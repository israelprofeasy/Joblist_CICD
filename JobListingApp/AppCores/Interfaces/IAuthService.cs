using JobListingApp.AppModels.DTOs;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Interfaces
{
    public interface IAuthService
    {
        Task<LoginCredDto> Login(string email, string password, bool rememberMe);
    }
}
