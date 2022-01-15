namespace JobListingApp.AppModels.Models
{
    public class JobApplication : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string JobId { get; set; }
        public Job Job { get; set; }
    }
}
