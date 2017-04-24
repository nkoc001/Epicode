using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using Epicode.Interfaces;
using Epicode.Core.Helpers;

namespace EpiCode.Notification.SMS
{
    public class Sms : ISmsNotification
    {
        public string Recipient { get; set; }
        public string Message { get; set; }

        public Sms()
        {
        }

        public Sms(string recipient, string message)
        {
            Recipient = recipient;
            Message = message;
        }

        public string Send(INotificationConfiguration configuration, ISmsNotification message)
        {
            var queryStr = SmsNotificationHelper.GenerateSevenBitSms(configuration, message);

            var sendResult = SmsNotificationHelper.SendSms(configuration.ServerAddress, queryStr);

            return sendResult;
        }
    }

    public class SmsConfiguration : INotificationConfiguration
    {
        public string ServerAddress { get; set; }
        public string SenderAddress { get; set; }
        public int Port { get; set; }
        public bool IsSslEnabled { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsHtml { get; set; }
    }
}
