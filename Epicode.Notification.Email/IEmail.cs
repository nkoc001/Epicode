using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpiCode.Notification;

namespace Epicode.Notification.Email
{
    public interface IEmail
    {
        void SendSmtpEmail(INotification notification);
    }
}
