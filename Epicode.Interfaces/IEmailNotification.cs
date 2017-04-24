using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicode.Interfaces
{
    public interface IEmailNotification
    {
        string From { get; set; }
        string To { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        bool IsHtml { get; set; }
        void Send(INotificationConfiguration configuration, IEmailNotification message);
    }
}
