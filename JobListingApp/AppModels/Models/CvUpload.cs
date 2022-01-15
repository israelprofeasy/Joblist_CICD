namespace JobListingApp.AppModels.Models
{
    public class CvUpload : BaseEntity
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
        //[Key]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
