using System.ComponentModel.DataAnnotations;

namespace JobListingApp.AppModels.Enums
{
    public enum JobNature
    {
        [Display(Name = "Full Time")]
        FullTime,
        [Display(Name = "Part Time")]
        PartTime,
        Contract,
        Internship
    }
}
