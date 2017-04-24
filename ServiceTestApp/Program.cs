using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Epicode.Interfaces;
using Epicode.Notification.Email;
using Epicode.Notification.Processor;
using Topshelf;

namespace ServiceTestApp
{

    public class Program
    {
        static void Main(string[] args)
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
}
