
namespace DevMail.Infrastructure.Models
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
