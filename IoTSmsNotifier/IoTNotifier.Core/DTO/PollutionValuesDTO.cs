using Newtonsoft.Json;
using System;

namespace IoTNotifier.Core.Repositories.DTO
{
    public class PollutionValuesDTO
    {
        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("value")]
        public float? Value { get; set; }
    }

}
