using JobListingApp.AppModels.Models;
using System.Collections.Generic;

namespace JobListingApp.AppCores.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(AppUser user, List<string> userRoles);
    }
}
