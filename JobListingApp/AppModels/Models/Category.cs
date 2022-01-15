using System.Collections.Generic;

namespace JobListingApp.AppModels.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
