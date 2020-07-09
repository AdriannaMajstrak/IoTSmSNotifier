using IoTNotifier.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNotifier.Core.Services
{
    public interface IPollutionService
    {
        IList<IPollution> GetPollution(string city);

        bool SubscribeWarnings(string city, string phoneNumber);

        bool SubscribeCycleNotification(string city, string phoneNumber, DateTime timeToSend);

        bool NotifyUser(string[] reciver, string content);

        void CycleMainMethod();
    }
}
