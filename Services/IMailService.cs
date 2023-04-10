using DevMail.Infrastructure.Models;
using System.Threading.Tasks;

namespace DevMail.Services
{
    public interface IMailService
    {
        Task<SendResult> SendEmailAsync(NewMail newMail);

        Task<SendResult> SendEmailAsync(MailSetting mailSettings, NewMail newMail);


    }
}
