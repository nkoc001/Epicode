using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicode.Interfaces
{
    public interface ISmsNotification
    {
       string Recipient { get; set; }
       string Message { get; set; }
       string Send(INotificationConfiguration configuration, ISmsNotification message);
    }
}
