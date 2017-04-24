using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;

namespace EpiCode.Notification
{
    public class NotificationFactory
    {
        public static INotificationProcessor GetNotificationProcessor(string notificationType)
        {
            using (IApplicationContext cntx = ContextRegistry.GetContext())
            {
                try
                {
                    INotificationProcessor notificationProcessor =
                        (INotificationProcessor) cntx.GetObject(notificationType);
                    return notificationProcessor;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                
            }
            //INotificationProcessor notificationProcessor;
            //switch (notificationType)
            //{
            //    case NotificationType.Email:
            //        notificationProcessor = new Email();
            //        break;
            //    case NotificationType.Sms:
            //        notificationProcessor = new Sms();
            //        break;
            //}
            //return notificationProcessor;
        }
    }

    public interface INotificationProcessor
    {
    }
}
