using Newtonsoft.Json;

namespace IoTSmsNotifier.Core.Repositories.DTO
{
    public class SMSResponseDetailsDTO
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("sentSmsCount")]
        public int SentSmsCount { get; set; }

        [JsonProperty("contentId")]
        public int ContentId { get; set; }

        [JsonProperty("recipientsResults")]
        public object[] RecipientsResults { get; set; }
    }

}
