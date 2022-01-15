using JobListingApp.AppCommons;
using JobListingApp.AppCores.Interfaces;
using JobListingApp.AppModels.DTOs;
using JobListingApp.AppModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobListingApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<AppUser> _userMgr;
        private readonly IAuthService _authService;
        public AuthController(IAuthService auth, UserManager<AppUser> userManager)
        {
            _userMgr = userManager;
            _authService = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO model)
        {

            var user = await _userMgr.FindByEmailAsync(model.email);
            if (user == null)
            {
                ModelState.AddModelError("Invalid", "Credentials provided by the user is invalid");
                return BadRequest(Utilities.BuildResponse<object>(false, "Invalid credentials", ModelState, null));
            }

            // check if user's email is confirmed
            if (await _userMgr.IsEmailConfirmedAsync(user))
            {
                var res = await _authService.Login(model.email, model.password, model.RememberMe);

                if (!res.status)
                {
                    ModelState.AddModelError("Invalid", "Credentials provided by the user is invalid");
                    return BadRequest(Utilities.BuildResponse<object>(false, "Invalid credentials", ModelState, null));
                }
                if (user.IsActive == false)
                {
                    ModelState.AddModelError("Access Denied", "Account Deactivated Contact Admin");
                    return BadRequest(Utilities.BuildResponse<object>(false, "Account Deactivated", ModelState, null));
                }

                return Ok(Utilities.BuildResponse(true, "Login is sucessful!", null, res));
            }

            ModelState.AddModelError("Invalid", "User must first confirm email before attempting to login");
            return BadRequest(Utilities.BuildResponse<object>(false, "Email not confirmed", ModelState, null));
        }
    }
}

