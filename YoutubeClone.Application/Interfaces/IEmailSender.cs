using System.Threading.Tasks;

namespace YoutubeClone.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string userEmail, string emailSubject, string message);
    }
}