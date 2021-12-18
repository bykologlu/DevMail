using System;
using System.Collections.Generic;
using System.Text;

namespace DevMail.Infrastructre.Models
{
    public class MailSetting
    {
        public string SenderMailAddress { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
