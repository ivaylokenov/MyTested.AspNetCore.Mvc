using System.Threading.Tasks;

namespace ApplicationParts.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
