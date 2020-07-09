using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNotifier.Core.Repositories
{
    public interface INotificationRepository
    {
        bool SendNotification(string[] receiver, string content);
    }
}
