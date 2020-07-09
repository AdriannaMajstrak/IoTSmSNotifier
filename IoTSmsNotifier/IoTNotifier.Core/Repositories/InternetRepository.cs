using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Repositories
{
    public class InternetRepository : IInternetRepository
    {
        public bool CheckInternetConnection()
        {
            using (var client = new RestClient(new Uri("http://google.com")))
            {
                var request = new RestRequest(Method.GET);

                return client.Execute(request).Result.IsSuccess;
            }
        }
    }
}
