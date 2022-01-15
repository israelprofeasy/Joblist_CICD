using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JobListingApp.AppModels.DTOs
{
    public class UploadDto
    {
        [Required]
        public IFormFile Photo { get; set; }

        public string PublicId { get; set; }
        public string Url { get; set; }
    }
}
