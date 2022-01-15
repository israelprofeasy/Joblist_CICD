namespace JobListingApp.AppModels.DTOs
{
    public class UserDetailReturnedDto
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public CvUploadReturnedDto CvUpload { get; set; }
    }
}
