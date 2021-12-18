using DevMail.Infrastructre.Consts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevMail.Infrastructre.Models
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
