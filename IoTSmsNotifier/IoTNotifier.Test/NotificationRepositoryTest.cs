using System;
using IoTNotifier.Core.Repositories;
using IoTNotifier.Core.Services;
using IoTSmsNotifier.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoTNotifier.Test
{
    [TestClass]
    public class NotificationRepositoryTest
    {
        [TestMethod]
        public void SendNotificationTest()
        {
            INotificationRepository notificationRepository = new NotificationRepository();
           Assert.IsTrue(notificationRepository.SendNotification(new [] {"512752053"}, "CO - 282,48g/m3 -Bardzo dobry\r\nNO2 - 30,68g/m3 -Bardzo dobry\r\nSO2 - 8,15g/m3 -Bardzo dobry\r\nO3 - 17,02g/m3 -Bardzo dobry\r\nPM10 - 37,19g/m3 -Dobry\r\nPM2,51234567891vldfjvilsdjm lzx,dlfsdrofwregtwerg"));
        }

        [TestMethod]
        public void ToDelete()
        {
            //PollutionInfoRepository pollutionInfoRepository = new PollutionInfoRepository();
           // var res = pollutionInfoRepository.GetPollutions("katowice");

            PollutionServices ps = new PollutionServices();
           // var message = ps.GetMessageContent(res);

            ps.CycleMainMethod();
            
        }
    }
}
