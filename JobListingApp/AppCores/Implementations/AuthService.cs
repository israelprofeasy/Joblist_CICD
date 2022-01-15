using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IJwtService _jWTService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, IJwtService jWTService)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _jWTService = jWTService;
        }
        public async Task<LoginCredDto> Login(string email, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var isCorrect = await _userManager.CheckPasswordAsync(user, password);
            var token = "";
            var response = new LoginCredDto { status = false };
            if (user != null && isCorrect == true)
            {
                try
                {
                    var res = await _signinManager.PasswordSignInAsync(user, password, rememberMe, false);

                    if (!res.Succeeded)
                    {
                        return response;
                    }

                    // get jwt token
                    var userRoles = await _userManager.GetRolesAsync(user);
                    token = _jWTService.GenerateToken(user, userRoles.ToList());
                    response.status = true;
                    response.Id = user.Id;
                    response.token = token;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return response;
            }

            return response;
        }
    }
}
