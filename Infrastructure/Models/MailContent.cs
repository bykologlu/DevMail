using DevMail.Infrastructure.Consts;

namespace DevMail.Infrastructure.Models
{

    public class MailContent : IMailContent
    {

        public bool IsTemplate { get; }
        public string Message { get;}

        public MailContent()
        {
        }
        public MailContent(string bodyMessage)
        {
            Message = bodyMessage;
        }
        public TemplateContent UseTemplate(EmailTemplates embedTemplate)
        {
            return new TemplateContent(embedTemplate);
        }

        public TemplateContent UseTemplate(string templatePath)
        {
            return new TemplateContent(templatePath);
        }
    }




}
