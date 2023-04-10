using DevMail.Infrastructure.Consts;
using DevMail.Infrastructure.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace DevMail.Services
{
    public class MailManager : IMailService
    {
        private MailSetting _mailSettings;

        public MailManager(IOptions<MailSetting> mailSettings)
        {
            if(mailSettings?.Value?.Host != null &&
               mailSettings?.Value?.SenderMailAddress != null &&
               mailSettings?.Value?.Password != null &&
               mailSettings?.Value?.Port != 0)
                _mailSettings = mailSettings?.Value;
            else
                _mailSettings = null;
        }

        public async Task<SendResult> SendEmailAsync(MailSetting mailSettings, NewMail newMail)
        {
            _mailSettings = _mailSettings ?? mailSettings;

            return await SendEmailAsync(newMail);
        }
        public async Task<SendResult> SendEmailAsync(NewMail newMail)
        {
            if (_mailSettings == null) throw new Exception("Email hesabı yapılandırılmamış.");

            try
            {
                MimeMessage email = new MimeMessage();
                BodyBuilder bodyBuilder = new BodyBuilder();

                email.Sender = MailboxAddress.Parse(_mailSettings.SenderMailAddress);
                email.Subject = newMail.Subject;

                foreach (string receiver in newMail.ReceiverMailAddresses)
                    email.To.Add(MailboxAddress.Parse(receiver));

                if (newMail.AttachedFiles != null)
                    foreach (string file in newMail.AttachedFiles)
                        bodyBuilder.Attachments.Add(file);

                if (newMail.Content.IsTemplate)
                {
                    TemplateContent templateContent = (TemplateContent)newMail.Content;
                    string customizedContent = CustomizeTemplateContent(templateContent);
                    newMail.Content = new MailContent(customizedContent);
                }

                bodyBuilder.HtmlBody = newMail.Content.Message;
                email.Body = bodyBuilder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_mailSettings.SenderMailAddress, _mailSettings.Password);
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }

                return new SendResult(Messages.SuccessSend,true);
            }
            catch(Exception err)
            {
                return new SendResult(err.Message);
            }
        }
         
        private string CustomizeTemplateContent(TemplateContent templateContent)
        {
            string customizedContent = templateContent.HtmlContent;

            customizedContent = customizedContent.Replace("{company_name}", templateContent.Companyname)
                                             .Replace("{company_logo}", templateContent.Logo)
                                             .Replace("{validation_link}", templateContent.ValidationLink)
                                             .Replace("{validation_code}", templateContent.ValidationCode)
                                             .Replace("{message}", templateContent.Message)
                                             .Replace("{action_url}", templateContent.ActionUrl)
                                             .Replace("{company_web}", templateContent.CompanyWeb)
                                             .Replace("{receiver_name}", templateContent.ReceiverName);

            return customizedContent;
        }

    }
}
