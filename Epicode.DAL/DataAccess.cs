using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Epicode.DAL
{
    public static class DataAccess
    {
        public static IEnumerable<Notification> GetNotificationBatchList(int batchSize, int notificationTypeId)
        {
            try
            {
                using (var db = new NotificationsEntities())
                {
                    db.Database.Connection.Open();
                    return
                        db.Notification.Take(batchSize).Where(a => a.FK_NotificationTypeID == notificationTypeId && a.FK_NotificationStatusID == 1 /*Pending*/).ToList();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("C:\\Dev\\bin\\SmsService\\log.txt", "Error Ocured in Method: GetNotificationBatchList \n Error Details: " + ex.Message);
                return null;
            }
        }

        public static IEnumerable<NotificatonType> GetEnabledNotificationTypes()
        {
            try
            {
                using (var db = new NotificationsEntities())
                {
                    db.Database.Connection.Open();
                    return db.NotificatonType.Where(a => a.IsEnabled).ToList();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("C:\\Dev\\bin\\SmsService\\log.txt", "Error Ocured in Method: GetEnabledNotificationTypes \n Error Details: " + ex.Message);
                return null;
            }
        }

        public static void UpdateNotificationStatus(long notificationId, short statusId, string note)
        {
            try
            {
                using (var db = new NotificationsEntities())
                {
                    db.Database.Connection.Open();

                    var dbNotification = db.Notification.FirstOrDefault(x => x.NotificationID == notificationId);
                    if (dbNotification != null)
                    {
                        dbNotification.LastModified = DateTime.Now;
                        dbNotification.FK_NotificationStatusID = statusId;
                        dbNotification.Note = note;
                    }

                    db.Notification.Attach(dbNotification);
                    db.Entry(dbNotification).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("C:\\Dev\\bin\\SmsService\\log.txt", "Error Ocured in Method: UpdateNotificationStatus \n Error Details: " + ex.Message);
            }
        }

        public static bool NotificationTypeIsEnabled(int notificationType)
        {
            try
            {
                using (var db = new NotificationsEntities())
                {
                    db.Database.Connection.Open();

                    var notificationTypeStatus = db.NotificatonType.FirstOrDefault(a => a.NotificationTypeID == notificationType);
                    return
                        notificationTypeStatus != null && notificationTypeStatus.IsEnabled;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("C:\\Dev\\bin\\SmsService\\log.txt", "Error Ocured in Method: NotificationTypeIsEnabled \n Error Details: " + ex.Message);
                return false;
            }
        }
    }
}
