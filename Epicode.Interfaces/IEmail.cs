using EpiCode.Notification;

namespace Epicode.Interfaces
{
    public interface IEmail
    {
        string From { get; set; }
        string To { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        void SendEmail(IEmail mailMessage);
    }
}
