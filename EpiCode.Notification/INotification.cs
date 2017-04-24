using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpiCode.Notification
{
    public interface INotification
    {
        string To { get; set; }

        string MessageBody { get; set; }

        NotificationType Type { get; set; }
    }
}
