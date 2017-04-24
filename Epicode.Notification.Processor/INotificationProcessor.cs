using EpiCode.Notification;

namespace Epicode.Notification.Processor
{
    public interface INotificationProcessor
    {
        void ProcessNotification(INotification notification);
    }
}
