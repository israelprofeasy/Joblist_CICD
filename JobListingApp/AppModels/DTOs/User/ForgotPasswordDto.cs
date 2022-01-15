using System.ComponentModel.DataAnnotations;

namespace JobListingApp.AppModels.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
