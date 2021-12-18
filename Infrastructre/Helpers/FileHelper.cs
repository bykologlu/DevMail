using DevMail.Infrastructre.Consts;
using DevMail.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DevMail.Infrastructre.Helpers
{
    public static class FileHelper
    {
        public static string ReadFromTemplateFilePath(string filePath)
        {
            string fileContent;

            using (StreamReader reader = new StreamReader(filePath))
            {
                fileContent = reader.ReadToEnd();
                reader.Close();
            }

            return fileContent;
        }

        public static string ReadFromEmbededTemplateFile(EmailTemplates template)
        {
            string fileContent;

            Stream templateStream = typeof(MailManager).Assembly.GetManifestResourceStream($"DevMail.Infrastructre.Templates.{template}.html");

            using (StreamReader reader = new StreamReader(templateStream))
            {
                fileContent = reader.ReadToEnd();
                reader.Close();
            }

            return fileContent;
        }
    }
}
