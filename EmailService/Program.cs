using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Epicode.Interfaces;
using Epicode.Notification.Email;
using Epicode.Notification.Processor;
using Topshelf;

namespace EmailService
{
    public class SmsSender
    {
        private readonly Timer _timer;

        public SmsSender()
        {
            _timer = new Timer(300000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => DeliverSmtpEmailNotification();
        }

        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }

        private static void DeliverSmtpEmailNotification()
        {
            INotificationProcessor processor = new NotificationProcessor();
            var config = GetSmtpConfiguration();

            processor.ProcessNotificationBatch(config, 50, 1);
        }

        private static INotificationConfiguration GetSmtpConfiguration()
        {
            var smtpConfig = new SmtpEmailConfiguration
            {
                ServerAddress = ConfigurationManager.AppSettings["SmtpServerAddress"],
                Port = Convert.ToInt16(ConfigurationManager.AppSettings["SmtpServerPort"]),
                IsSslEnabled = false,
                UserName = ConfigurationManager.AppSettings["SmtpServerUsername"],
                Password = ConfigurationManager.AppSettings["SmtpServerPassword"],
                SenderAddress = ConfigurationManager.AppSettings["SmtpSenderAddress"],
                IsHtml = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailFormatIsHtml"])
            };

            return smtpConfig;
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
                x.SetDescription("Email Notification Service");
                x.SetDisplayName("Vukuzakhe Email");
                x.SetServiceName("VukuzakheEmail");
            });
        }
    }
}
