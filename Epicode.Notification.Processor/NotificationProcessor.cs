using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Epicode.DAL;
using Epicode.Interfaces;
using Epicode.Notification.Email;
using Epicode.Notification.Factory;

namespace Epicode.Notification.Processor
{
    public class NotificationProcessor : INotificationProcessor
    {
        public void ProcessNotificationBatch(INotificationConfiguration config, int notificationTypeId, int batchSize)
        {
            if (!NotificationTypeIsEnabled(notificationTypeId)) return;
            long notificationId = 0;
            switch (notificationTypeId)
            {
                case 1:
                    try
                    {
                        var emailNotifications = DataAccess.GetNotificationBatchList(Convert.ToInt16(batchSize), 1 /*Email*/);
                        var smtpConfiguration = config;

                        foreach (var notification in emailNotifications)
                        {
                            notificationId = notification.NotificationID;
                            var notificationToSend = CreateEmailNotification(notification, config.SenderAddress, config.IsHtml);
                            notificationToSend.Send(smtpConfiguration, notificationToSend);
                            DataAccess.UpdateNotificationStatus(notification.NotificationID, 2, "Email Sent");
                        }
                    }
                    catch (Exception ex)
                    {
                        DataAccess.UpdateNotificationStatus(notificationId, 3, string.Format("Error Occured Sending EMail: {0}, Inner Exception: {1}", ex.Message, ex.InnerException));
                    }
                    break;

                case 2:
                    try
                    {
                        var smsNotifications = DataAccess.GetNotificationBatchList(Convert.ToInt16(batchSize), 2 /*Sms*/);
                        var smsConfiguration = config;
                            

                        foreach (var smsNotification in smsNotifications)
                        {
                            notificationId = smsNotification.NotificationID;
                            var notificationToSend = CreateSmsNotification(smsNotification);
                            var sendResult = notificationToSend.Send(smsConfiguration, notificationToSend);

                            DataAccess.UpdateNotificationStatus(smsNotification.NotificationID, 2, "Sms Sent");
                        }
                    }
                    catch (Exception ex)
                    {
                        DataAccess.UpdateNotificationStatus(notificationId, 3, string.Format("Error Occured Sending Sms: {0}, Inner Exception: {1}", ex.Message, ex.InnerException));
                    }
                    break;
            }
        }

        private static bool NotificationTypeIsEnabled(int notificationType)
        {
            return DataAccess.NotificationTypeIsEnabled(notificationType);
        }

        private static IEmailNotification CreateEmailNotification(DAL.Notification notification, string senderAddress, bool isHtml)
        {
            return NotificationFactory.CreateSmtpEmailNotification(senderAddress, notification.NotificationRecipient,
                notification.NotificationSubject, notification.NotificationBody, isHtml);
        }

        private static ISmsNotification CreateSmsNotification(DAL.Notification notification)
        {
            return NotificationFactory.CreateSmsNotification(notification.NotificationRecipient,
                notification.NotificationBody);
        }
    }
}
