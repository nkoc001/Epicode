namespace Epicode.Interfaces
{
    public interface INotification
    {
        string From { get; set; }
        string To { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        bool IsHtml { get; set; }
        NotificationType Type { get; set; }
        void Send(INotificationConfiguration configuration, INotification message);
    }
}
