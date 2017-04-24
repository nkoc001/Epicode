using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epicode.Interfaces;
using Epicode.Notification.Email;
using EpiCode.Notification.SMS;

namespace Epicode.Notification.Factory
{
    public class NotificationFactory
    {
        public static IEmailNotification CreateSmtpEmailNotification(string from, string to, string subject, string body, bool isHtml)
        {
            return new SmtpEmail(from, to, subject, body, isHtml);
        }

        public static ISmsNotification CreateSmsNotification(string recipient, string message)
        {
            return new Sms(recipient, message);
        }
    }
}
