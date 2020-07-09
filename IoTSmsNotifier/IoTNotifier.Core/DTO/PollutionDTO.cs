using Newtonsoft.Json;

namespace IoTNotifier.Core.Repositories.DTO
{
    public class PollutionDTO
    {
        [JsonProperty("key")]
        public string SymbolOfPollution { get; set; }

        [JsonProperty("values")]
        public PollutionValuesDTO[] TableOfPollutionValuesWithDate { get; set; }
    }

}
