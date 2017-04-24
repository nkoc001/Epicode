using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Epicode.Interfaces;
using Epicode.Notification.Processor;
using EpiCode.Notification.SMS;
using Topshelf;

namespace SmsService
{
    public class SmsSender
    {
        private static Timer _timer;

        public SmsSender()
        {
            _timer = new Timer(300) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => DeliverSmsNotification();
        }

        public void Start() { _timer.Start(); File.AppendAllText("C:\\Dev\\bin\\SmsService\\log.txt", "Timer Started \n"); }
        public void Stop() { _timer.Stop(); }

        private static void DeliverSmsNotification()
        {
            File.AppendAllText("C:\\Dev\\bin\\SmsService\\log.txt", "Deliver Method Started \n");

            _timer.Stop();

            INotificationProcessor processor = new NotificationProcessor();
            var config = GetSmsConfiguration();

            processor.ProcessNotificationBatch(config, 2, 500);

            _timer.Start();

            File.AppendAllText("C:\\Dev\\bin\\SmsService\\log.txt", "Deliver Method Done");
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

    public class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<SmsSender>(s =>
                {
                    s.ConstructUsing(name => new SmsSender());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("Sms Notification Service");
                x.SetDisplayName("Vukuzakhe Sms");
                x.SetServiceName("VukuzakheSms");
            }); 
        }
    }
}
