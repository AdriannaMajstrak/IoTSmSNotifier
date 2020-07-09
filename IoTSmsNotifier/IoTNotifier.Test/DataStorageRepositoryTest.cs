using System;
using IoTNotifier.Core.Repositories;
using IoTSmsNotifier.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoTNotifier.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IoTNotifier.Test
{
    [TestClass]
    public class DataStorageRepositoryTest
    {

        IDataStorageRepository _dataStorageRepository;


        public DataStorageRepositoryTest()
        {
            //_dataStorageRepository = new DataStorageRepository("http://192.168.0.67:5000/api/subscriptions"); ////remote
            _dataStorageRepository = new DataStorageRepository(); ////local
        }

        [TestMethod]
        public void SaveSubscriptionsTest()
        {
            var result = _dataStorageRepository.SaveSubscription(new Subscription("katowice", "512752053", new DateTime(2019, 1, 1), true));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetSubscriptionTest()
        {
            var list = _dataStorageRepository.GetSubscriptions();
        }

        [TestMethod]
        public void UpdateLastNotificationDateTest()
        {
            SaveSubscriptionsTest();
            var now = DateTime.Now;

            var result = _dataStorageRepository.UpdateLastNotificationDate(1, now);
            Assert.IsTrue(result);

            var result2 = _dataStorageRepository.GetSubscriptions();

            Assert.IsTrue(result2.First().LastNotificationDate.ToString() == now.ToString());
        }

        [TestMethod]
        public void ClearDatabseOnRaspberryPi()
        {
            _dataStorageRepository.ClearDatabase();
        }

    }
}
