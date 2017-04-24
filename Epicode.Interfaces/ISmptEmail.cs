using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicode.Interfaces
{
    public interface ISmptEmail
    {
        INotificationConfiguration GetSmtpConfiguration();

        void SendSmtpEmail(INotificationConfiguration smtpConfiguration, ISmptEmail mailMessage);
    }
}
