using System.ComponentModel.DataAnnotations;

namespace JobListingApp.AppModels.DTOs
{
    public class IndustryDto
    {

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 50 characters")]
        public string Name { get; set; }
    }
}
