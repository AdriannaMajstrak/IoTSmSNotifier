using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTNotifier.DatabaseApi.Model
{
    public class Subscription
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("timeToSend")]
        public DateTime? TimeToSend { get; set; }

        [JsonProperty("dailySubscription")]
        public bool DailySubscription { get; set; }

        [JsonProperty("lastNotificationDate")]
        public DateTime LastNotificationDate { get; set; }


    }
}
