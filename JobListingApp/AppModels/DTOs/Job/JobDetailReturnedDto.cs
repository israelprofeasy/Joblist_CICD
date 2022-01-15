using System;

namespace JobListingApp.AppModels.DTOs
{
    public class JobDetailReturnedDto
    {
        public string Id { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Industry { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string JobNature { get; set; }
        public string JobDescription { get; set; }
        public string Deadline { get; set; }
        public string DateCreated { get; set; }
        public string SalaryRange { get; set; }

    }
}

