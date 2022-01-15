using JobListingApp.AppModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobListingApp.AppModels.Models
{
    public class Job : BaseEntity
    {
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Company { get; set; }

        public string IndustryId { get; set; }
        public Industry Industry { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public Locations Location { get; set; }
        [Required]
        public JobNature JobNature { get; set; }
        [Required]
        public string JobDescription { get; set; }
        [Required]
        public int JobValidDays { get; set; }
        [Required]
        [Column(TypeName = "money(18,2)")]
        public decimal MinimumSalary { get; set; }
        [Required]
        [Column(TypeName = "money(18,2)")]
        public decimal MaximumSalary { get; set; }
        public string Deadline
        {
            get
            {
                return DateTime.Now.AddDays(JobValidDays).ToString();
            }
        }
        public List<JobApplication> AppliedJobs { get; set; } = new List<JobApplication>();

    }
}
