using System;
using System.Collections.Generic;
using System.Text;

namespace DevMail.Infrastructre.Models
{
    public class SendResult
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public SendResult(string message, bool isSuccess = false)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
