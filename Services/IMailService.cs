using DevMail.Infrastructre.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevMail.Services
{
    public interface IMailService
    {
        Task<SendResult> SendEmailAsync(NewMail createMailModel);
         
    }
}
