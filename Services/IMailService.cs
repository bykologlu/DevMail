using DevMail.Infrastructure.Models;
using System.Threading.Tasks;

namespace DevMail.Services
{
    public interface IMailService
    {
        Task<SendResult> SendEmailAsync(NewMail createMailModel);
         
    }
}
