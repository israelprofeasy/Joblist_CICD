using System;

namespace JobListingApp.AppModels.DTOs
{
    public class JobPreviewDto
    {
        public string Id { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        //public Locations Location { get; set; }

        public string JobNature { get; set; }
        // public JobNature JobNature { get; set; }
        public string Deadline { get; set; }
        public string DateCreated { get; set; }
        public string SalaryRange { get; set; }
    }
}
