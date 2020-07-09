using IoTNotifier.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNotifier.Core.Repositories
{
    public interface IDataStorageRepository
    {
        bool SaveSubscription(Subscription subscription);

        IList<Subscription> GetSubscriptions();

        bool UpdateLastNotificationDate(int subscriptionId, DateTime date);

        void ClearDatabase();

    }
}
