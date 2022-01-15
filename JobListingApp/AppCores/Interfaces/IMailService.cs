using JobListingApp.AppCommons;
using System.Threading.Tasks;

namespace JobListingApp.AppCores.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
