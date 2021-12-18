using System;
using System.Collections.Generic;
using System.Text;

namespace DevMail.Infrastructre.Models
{
    public interface IMailContent
    {
        string Message { get; }
        bool IsTemplate { get; }
    }
}
