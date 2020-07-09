using IoTNotifier.Core.DTO;
using IoTNotifier.Core.Exceptions;
using IoTNotifier.Core.Model;
using IoTSmsNotifier.Core.Repositories;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoTNotifier.Core.Repositories
{
    public class DataStorageRepository : IDataStorageRepository
    {
        public DataStorageRepository()
        {

        }

        public DataStorageRepository(string address)
        {
            this.address = address;
        }

        readonly string address = "http://localhost:5000/api/subscriptions";

        public IList<Subscription> GetSubscriptions()
        {
            try
            {
                using (var client = new RestClient(new Uri(address)))
                {
                    var request = new RestRequest(Method.GET);
                    var response = client.Execute<IEnumerable<SubscriptionDTO>>(request).Result;

                    if (response.IsSuccess)
                    {
                        var listOfSubscriptions = SubscriptionMapper(response.Data);

                        return listOfSubscriptions;
                    }
                    else
                    {
                        throw new ExternalResourcesException($"Connections error. Server returned {response.StatusDescription}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExternalResourcesException($"Connections error", ex);
            }                      
        }

        public bool SaveSubscription(Subscription subscription)
        {
            try
            {
                using (var client = new RestClient(new Uri(address)))
                {
                    var request = new RestRequest(Method.POST);
                    request.AddBody(new SubscriptionDTO()
                    {
                        City = subscription.City,
                        PhoneNumber = subscription.PhoneNumber,
                        TimeToSend = subscription.TimeToSend,
                        DailySubscription = subscription.DailySubscription
                    });

                    var response = client.Execute<SubscriptionDTO>(request).Result;

                    return response.IsSuccess;
                }
            }
            catch (Exception ex)
            {
                throw new ExternalResourcesException($"Connections error", ex);
            }
           
        }

        public bool UpdateLastNotificationDate(int subscriptionId, DateTime date)
        {
            try
            {
                using (var client = new RestClient(new Uri(address)))
                {
                    var request = new RestRequest($"{subscriptionId}", Method.GET);
                   

                    var response = client.Execute<SubscriptionDTO>(request).Result;

                    if(!response.IsSuccess)
                    {
                        throw new Exception(response.StatusDescription);
                    }

                    SubscriptionDTO subscriptionDTO = response.Data;

                    subscriptionDTO.LastNotificationDate = date;

                    request = new RestRequest($"{subscriptionId}", Method.PUT);
                    request.AddBody(subscriptionDTO);

                    response = client.Execute<SubscriptionDTO>(request).Result;

                    return response.IsSuccess;
                }
            }
            catch (Exception ex)
            {
                throw new ExternalResourcesException($"Connections error", ex);
            }

        }

        public void ClearDatabase()
        {
            try
            {
                using (var client = new RestClient(new Uri(address)))
                {
                    var request = new RestRequest(Method.DELETE);

                    var response = client.Execute<IEnumerable<SubscriptionDTO>>(request).Result;


                    if (!response.IsSuccess)
                    {
                        throw new Exception(response.StatusDescription);
                    }

                    if(response.Data.Count() != 0)
                    {
                        throw new Exception("Data has been not deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ExternalResourcesException($"Connections error", ex);
            }
        }

        private IList<Subscription> SubscriptionMapper(IEnumerable<SubscriptionDTO> subscriptionDTOs)
        {
            return subscriptionDTOs.Select(x => new Subscription()
            {
                Id = x.Id,
                City = x.City,
                PhoneNumber = x.PhoneNumber,
                TimeToSend = x.TimeToSend,
                LastNotificationDate = x.LastNotificationDate,
                DailySubscription = x.DailySubscription
            }).ToList();
        }       
    }
}
