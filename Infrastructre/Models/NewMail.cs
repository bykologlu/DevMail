using System;
using System.Collections.Generic;
using System.Text;

namespace DevMail.Infrastructre.Models
{
    public class NewMail
    {
        public List<string> ReceiverMailAddresses { get; set; }
        public IMailContent Content { get; set; }
        public string Subject { get; set; }
        public List<string> AttachedFiles { get; set; }

        public NewMail()
        {

        }
        public NewMail(List<string> receiverMailAddresses,
                               string subject,
                               string content,
                               List<string> attachedFiles)
        {
            ReceiverMailAddresses = receiverMailAddresses;
            Subject = subject; 
            AttachedFiles = attachedFiles;
            Content = new MailContent(content);
        }

        public NewMail(string receiverMailAddress,
                               string subject,
                               string content)
        {
            ReceiverMailAddresses = new List<string> { receiverMailAddress };
            Content = new MailContent(content);
            Subject = subject;
        }

        public NewMail(string receiverMailAddress,
                               string subject,
                               TemplateContent content)
        {
            ReceiverMailAddresses = new List<string> { receiverMailAddress };
            Subject = subject;
            Content = content;
        }
    }
}
