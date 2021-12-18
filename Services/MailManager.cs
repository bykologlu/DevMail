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
        private readonly MailSetting _mailSettings;

        public MailManager(IOptions<MailSetting> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<SendResult> SendEmailAsync(NewMail createMailModel)
        {
            try
            {
                MimeMessage email = new MimeMessage();
                BodyBuilder bodyBuilder = new BodyBuilder();

                email.Sender = MailboxAddress.Parse(_mailSettings.SenderMailAddress);
                email.Subject = createMailModel.Subject;

                foreach (string receiver in createMailModel.ReceiverMailAddresses)
                    email.To.Add(MailboxAddress.Parse(receiver));

                if (createMailModel.AttachedFiles != null)
                    foreach (string file in createMailModel.AttachedFiles)
                        bodyBuilder.Attachments.Add(file);

                if (createMailModel.Content.IsTemplate)
                {
                    TemplateContent templateContent = (TemplateContent)createMailModel.Content;
                    string customizedContent = CustomizeTemplateContent(templateContent);
                    createMailModel.Content = new MailContent(customizedContent);
                }

                bodyBuilder.HtmlBody = createMailModel.Content.Message;
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
                                             .Replace("{message}", templateContent.Message);

            return customizedContent;
        }

    }
}
