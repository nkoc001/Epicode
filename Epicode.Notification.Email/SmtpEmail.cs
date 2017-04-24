using System;
using System.Configuration;
using System.Net.Mail;
using Epicode.DAL;
using Epicode.Interfaces;

namespace Epicode.Notification.Email
{
    public class SmtpEmail : IEmailNotification
    {
        #region Members & Constructors

            public SmtpEmail()
            {
            }

            public SmtpEmail(string from, string to, string subject, string body, bool ishtml)
            {
                From = from;
                To = to;
                Subject = subject;
                Body = body;
                IsHtml = ishtml;
            }
        
            public string From { get; set; }
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public bool IsHtml { get; set; }
        #endregion

        public void  Send(INotificationConfiguration configuration, IEmailNotification message)
        {
            var messageToSend = new MailMessage(message.From, message.To)
            {
                IsBodyHtml = message.IsHtml,
                Subject = message.Subject,
                Body = message.Body
            };

            using (var client = new SmtpClient())
            {
                client.Port = configuration.Port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = configuration.IsSslEnabled;
                client.UseDefaultCredentials = false;
                client.Host = configuration.ServerAddress;
                client.Send(messageToSend);
            }
        }
    }


    public class SmtpEmailConfiguration : INotificationConfiguration
    {
        public SmtpEmailConfiguration()
        {
        }

        public SmtpEmailConfiguration(string serveraddress, string senderaddress, int port, bool issslenabled, string username, string password, bool ishtml)
        {
            ServerAddress = serveraddress;
            SenderAddress = senderaddress;
            Port = port;
            IsSslEnabled = issslenabled;
            UserName = username;
            Password = password;
            IsHtml = ishtml;
        }

        public string ServerAddress { get; set; }
        public string SenderAddress { get; set; }
        public int Port { get; set; }
        public bool IsSslEnabled { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsHtml { get; set; }
    }
}
