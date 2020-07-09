using IoTSmsNotifier.Core.Repositories.DTO;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNotifier.Core.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        string adress = "https://promosms.com/api/rest/send-sms/";

        public bool SendNotification(string[] receivers, string content)
        {
            try
            {
                using (var client = new RestClient(new Uri(adress)))
                {
                    var request = new RestRequest("??", Method.POST);
                    request.AddParameter("text", content);
                    request.AddParameter("type", 1);
                    request.AddParameter("long-sms", true);
                    request.AddParameter("special-chars", true);

                    foreach (var reciver in receivers)
                    {
                        request.AddParameter("recipients[]", reciver);
                    }

                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    var result = client.Execute<SmsResponseDTO>(request).Result;
                                        
                    return result.IsSuccess;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }

}
