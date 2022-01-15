namespace JobListingApp.AppCommons
{
    public class MailRequest
    {
        public string ToEmail { get; set; }

        public string DisplayName { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        //  public List<IFormFile> Attachments { get; set; }

    }
}
