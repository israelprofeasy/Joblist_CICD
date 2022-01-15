using JobListingApp.AppModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobListingApp.AppModels.DTOs
{
    public class JobDetailDto
    {

        [Required]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 700 characters")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 70 characters")]
        public string Company { get; set; }

        [Required]
        //  [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 20 characters")]
        public string IndustryId { get; set; }

        [Required]
        //[StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 20 characters")]
        public string CategoryId { get; set; }

        [Required]
        //[StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 20 characters")]
        public Locations Location { get; set; }

        [Required]
        //[StringLength(20, MinimumLength = 3, ErrorMessage = "Name must not be less than 3 characters and not more than 20 characters")]
        [Display(Name = "Job Nature")]
        public JobNature JobNature { get; set; }

        [Required]

        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Required]
        [Display(Name = "Job Valid Open Days")]
        public int JobValidDays { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Minimum Salary")]

        public decimal MinimumSalary { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Minimum Salary")]
        public decimal MaximumSalary { get; set; }

        public string SalaryRange
        {
            get
            {
                return $"{ MinimumSalary} to {MaximumSalary}";
            }
        }
    }
}
