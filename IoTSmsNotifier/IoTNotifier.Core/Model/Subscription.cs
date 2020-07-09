using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNotifier.Core.Model
{
    public class Subscription
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? TimeToSend { get; set; }
        public bool DailySubscription;
        public DateTime LastNotificationDate { get; set; }

        public Subscription()
        {

        }

        public Subscription(string city, string phoneNumber, DateTime? timeToSend)
        {
            City = city;
            PhoneNumber = phoneNumber;
            TimeToSend = timeToSend;
            DailySubscription = true;
        }

        public Subscription(string city, string phoneNumber)
        {
            City = city;
            PhoneNumber = phoneNumber;
            TimeToSend = null;
            DailySubscription = false;
        }

        public Subscription(string city, string phoneNumber, DateTime? timeToSend, bool dailySubscription)
        {
            City = city;
            PhoneNumber = phoneNumber;
            TimeToSend = timeToSend;
            DailySubscription = dailySubscription;
        }
    }
}
