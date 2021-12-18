namespace DevMail.Infrastructure.Models
{
    public interface IMailContent
    {
        string Message { get; }
        bool IsTemplate { get; }
    }
}
