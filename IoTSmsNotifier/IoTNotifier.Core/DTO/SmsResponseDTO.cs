using Newtonsoft.Json;

namespace IoTSmsNotifier.Core.Repositories.DTO
{
    public class SmsResponseDTO
    {
        [JsonProperty("reponse")]
        public SMSResponseDetailsDTO Response { get; set; }
    }

}
