using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Epicode.Interfaces;
using Epicode.Notification.Processor;
using EpiCode.Notification.SMS;
using Topshelf;

namespace SmsServiceTestApp
{
    public class Program
    {
        private static void Main()
        {
            INotificationProcessor processor = new NotificationProcessor();
            var config = GetSmsConfiguration();

            processor.ProcessNotificationBatch(config, 2, 500);
        }

        private static INotificationConfiguration GetSmsConfiguration()
        {
            var smsConfig = new SmsConfiguration
            {
                ServerAddress = ConfigurationManager.AppSettings["BulkSmsUrl"],
                UserName = ConfigurationManager.AppSettings["BulkSmsUserName"],
                Password = ConfigurationManager.AppSettings["BulkSmsPassword"]
            };

            return smsConfig;
        }
    }
}
