using Epicode.Interfaces;

namespace Epicode.Notification.Email
{
    public class PopEmail : IEmailNotification
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        public void Send(INotificationConfiguration configuration, IEmailNotification message)
        {
            throw new System.NotImplementedException();
        }
    }
}
