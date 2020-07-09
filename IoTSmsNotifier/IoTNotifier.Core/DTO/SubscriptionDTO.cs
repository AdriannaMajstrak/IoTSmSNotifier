using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.DTO
{
    public class SubscriptionDTO
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
