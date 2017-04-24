using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicode.Interfaces
{
    public interface INotificationProcessor
    {
        void ProcessNotificationBatch(INotificationConfiguration config, int batchSize, int notificationType);
    }
}
