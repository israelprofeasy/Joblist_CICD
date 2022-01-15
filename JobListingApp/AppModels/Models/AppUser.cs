using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace JobListingApp.AppModels.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string CvUploadId { get; set; }
        public CvUpload CvUpload { get; set; }
        public List<JobApplication> AppliedJobs { get; set; } = new List<JobApplication>();

    }
}
