
using System;
using System.Collections.Generic;
using System.Text;
using IoTNotifier.Core.Repositories;
using IoTSmsNotifier.Core.Repositories;
using IoTNotifier.Core.Model;
using System.Linq;
using IoTNotifier.Core.Extensions;

namespace IoTNotifier.Core.Services
{
    public class PollutionServices : IPollutionService
    {
        IDataStorageRepository dataStorageRepository;
        IPollutionInfoRepository pollutionInfoRepository;
        INotificationRepository notificationRepository;
        DateTime timeToCheckPollution;        

        public PollutionServices()
        {
            dataStorageRepository = new DataStorageRepository();
            pollutionInfoRepository = new PollutionInfoRepository();
            notificationRepository = new NotificationRepository();
            DateTime dateTimeNow = DateTime.Now;
            timeToCheckPollution = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day, dateTimeNow.Hour, 0, 0).AddHours(1);
            ListOfActualPollutions = GetPollution("katowice");
        }

        public IList<IPollution> ListOfActualPollutions;

        public IList<IPollution> GetPollution(string city)
        {            
            return pollutionInfoRepository.GetPollutions("katowice");
        }

        public bool NotifyUser(string[] reciver, string content)
        {
            return notificationRepository.SendNotification(reciver, content);
        }

        public bool SubscribeCycleNotification(string city, string phoneNumber, DateTime timeToSend)
        {
            return dataStorageRepository.SaveSubscription(new Subscription(city, phoneNumber, timeToSend));
        }

        public bool SubscribeWarnings(string city, string phoneNumber)
        {
            return dataStorageRepository.SaveSubscription(new Subscription(city, phoneNumber));
        }

        public string GetMessageContent(IList<IPollution> listOfPollution)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Symbol - wartość (mikrogram/m3) - indeks jakości");

            foreach (var polution in listOfPollution)
            {                 
                stringBuilder.AppendLine(polution.SymbolOfPollution + " - " + polution.ValueOfPollution.ToString("n2") + " - " + polution.GetAirQuality().ToAirQualityString());
            }

            stringBuilder.AppendLine("Aby anulowac subskrypcję wyślij maila: admin@projektsystemu.pl");

            return stringBuilder.ToString();
        }

        public void CycleMainMethod()
        {

            if (timeToCheckPollution >= DateTime.Now)
              return;

            ListOfActualPollutions = GetPollution("katowice");

            timeToCheckPollution = timeToCheckPollution.AddHours(1);

            //nie wysyłamy sms przed 6 rano i po 22
            if (DateTime.Now.Hour < 6 || DateTime.Now.Hour > 21)
                return;

            var listOfSubscribers = (dataStorageRepository.GetSubscriptions()).Where(x => x.LastNotificationDate.Date < DateTime.Now.Date);
                        
            IList<string> listOfPhoneNumbersToSend = new List<string>();
            IList<Subscription> listOfSubscriptionsToSend = new List<Subscription>();

            foreach (var subscriber in listOfSubscribers)
            {
                if (subscriber.DailySubscription && subscriber.TimeToSend.Value.Hour <= DateTime.Now.Hour)
                {
                    listOfPhoneNumbersToSend.Add(subscriber.PhoneNumber);
                    listOfSubscriptionsToSend.Add(subscriber);
                }
            }
            
            if(listOfPhoneNumbersToSend.Count > 0)
            {
                if (NotifyUser(listOfPhoneNumbersToSend.ToArray(), GetMessageContent(ListOfActualPollutions)))
                {
                    foreach (var subscription in listOfSubscriptionsToSend)
                    {
                        dataStorageRepository.UpdateLastNotificationDate(subscription.Id, DateTime.Now);
                    }                    
                }
            }

           if (ListOfActualPollutions.Any(x => x.GetAirQuality() == AirQuality.BZly || x.GetAirQuality() == AirQuality.Zly))
            {
                foreach (var subscriber in listOfSubscribers)
                {
                    if (!subscriber.DailySubscription)
                    {
                        listOfPhoneNumbersToSend.Add(subscriber.PhoneNumber);
                        listOfSubscriptionsToSend.Add(subscriber);
                    }
                }

                if (listOfPhoneNumbersToSend.Count > 0)
                {
                    if (NotifyUser(listOfPhoneNumbersToSend.ToArray(), GetMessageContent(ListOfActualPollutions)))
                    {
                        foreach (var subscription in listOfSubscriptionsToSend)
                        {
                            dataStorageRepository.UpdateLastNotificationDate(subscription.Id, DateTime.Now);
                        }
                    }
                }
            }

        }        
    }
}
