﻿
using DevMail.Infrastructure.Consts;
using DevMail.Infrastructure.Helpers;

namespace DevMail.Infrastructure.Models
{
    public class TemplateContent : IMailContent
    {
        public TemplateContent(EmailTemplates embedTemplate)
        {
            HtmlContent = FileHelper.ReadFromEmbededTemplateFile(embedTemplate);
            IsTemplate = true;
        }

        public TemplateContent(string templatePath)
        {
            HtmlContent = FileHelper.ReadFromTemplateFilePath(templatePath);
            IsTemplate = true;
        }

        private string _message { get; set; }

        public bool IsTemplate { get; }
        public string HtmlContent { get; }

        public string TemplateFilePath { get; set; }
        public string Companyname { get; set; }
        public string Logo { get; set; }
        public string Message => _message;
        public string ValidationCode { get; set; }
        public string ValidationLink { get; set; }
        public string ReceiverName { get; set; }
        public string ActionUrl { get; set; }

        public string CompanyWeb { get; set; }
        public TemplateContent AddCompanyName(string companyName)
        {
            Companyname = companyName;
            return this;
        }

        public TemplateContent AddLogo(string logoUrl)
        {
            Logo = logoUrl;
            return this;
        }

        public TemplateContent AddMessage(string message)
        {
            _message = message;
            return this;
        }

        public TemplateContent AddValidationCode(string code)
        {
            ValidationCode = code;
            return this;
        }

        public TemplateContent AddValidationLink(string link)
        {
            ValidationLink = link;
            return this;
        }

        public TemplateContent AddReceiverName(string receiverName)
        {
            ReceiverName = receiverName;
            return this;
        }

        public TemplateContent AddActionUrl(string actionUrl)
        {
            ActionUrl = actionUrl;
            return this;
        }

        public TemplateContent AddCompanyWeb(string companyWeb)
        {
            CompanyWeb = companyWeb;
            return this;
        }

    }
}
