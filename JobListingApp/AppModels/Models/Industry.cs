using System.Collections.Generic;

namespace JobListingApp.AppModels.Models
{
    public class Industry : BaseEntity
    {
        public string Name { get; set; }
        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
