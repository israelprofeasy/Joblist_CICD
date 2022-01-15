using System.ComponentModel.DataAnnotations;

namespace JobListingApp.AppModels.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Enter your email address")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public bool RememberMe { get; set; }

    }
}
