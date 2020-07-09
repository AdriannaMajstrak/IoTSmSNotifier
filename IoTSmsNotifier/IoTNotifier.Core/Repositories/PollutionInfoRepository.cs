using IoTNotifier.Core.Repositories.DTO;
using IoTNotifier.Core.Model;
using IoTNotifier.Core.Repositories;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IoTNotifier.Core.Exceptions;

namespace IoTNotifier.Core.Repositories
{
    public class PollutionInfoRepository : IPollutionInfoRepository
    {
        readonly IList<string> listOfSensorsIds = new List<string>()
            {"5343", "5346","5354","5371","5373","5376","5378","5382"};

        readonly string address = "http://api.gios.gov.pl/pjp-api/rest/data/getData/";

        public IList<IPollution> GetPollutions(string city)
        {
            if (!city.Equals("katowice", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new NotImplementedException();
            }

            IList<IPollution> listOfPollutions = new List<IPollution>();

            foreach (var sensor in listOfSensorsIds)
            {
                var polution = GetPollution(sensor);

                if (polution is UnknownPollution)
                {
                    continue;
                }

               if(listOfPollutions.Any(x => x.SymbolOfPollution == polution.SymbolOfPollution))
                {
                    var duplicatePollution = listOfPollutions.First(x => x.SymbolOfPollution == polution.SymbolOfPollution);
                    if (polution.LastMeasurement <= duplicatePollution.LastMeasurement)
                    {
                        continue;
                    }
                    else
                    {
                        listOfPollutions.Remove(duplicatePollution);
                    }
                }

                listOfPollutions.Add(polution);
            }

            return listOfPollutions;
        }

        IPollution GetPollution(string idOfSensor)
        {
            try
            {
                using (var client = new RestClient(new Uri(address)))
                {
                    var request = new RestRequest(idOfSensor, Method.GET);
                    var response = client.Execute<PollutionDTO>(request).Result;

                    if (response.IsSuccess)
                    {
                        var mappedPollution = PollutionMapper(response.Data);

                        if (mappedPollution == null)
                        {
                            throw new ExternalResourcesException("Connections with API GiOS was OK, but server returned null values");
                        }

                        return mappedPollution;
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

        private IPollution PollutionMapper(PollutionDTO pollutionDTO)
        {
            if (pollutionDTO == null || pollutionDTO.TableOfPollutionValuesWithDate == null || !pollutionDTO.TableOfPollutionValuesWithDate.Any())
            {
                return null;
            }

            var elementsWithValuesAndDate = pollutionDTO.TableOfPollutionValuesWithDate
                .Where(x => x.Value != null && x.Date != null);

            if(!elementsWithValuesAndDate.Any())
            {
                return null;
            }

            var maxDate = elementsWithValuesAndDate.Max(x => x.Date);
            float valueOfPolluton = (float)elementsWithValuesAndDate.First(x => x.Date == maxDate).Value;

            switch (pollutionDTO.SymbolOfPollution.ToUpper())
            { 
                case "CO":
                    return new PollutionCO(valueOfPolluton, (DateTime)maxDate);                    
                case "NO2":
                    return new PollutionNO2(valueOfPolluton, (DateTime)maxDate);
                case "SO2":
                    return new PollutionSO2(valueOfPolluton, (DateTime)maxDate);
                case "O3":
                    return new PollutionO3(valueOfPolluton, (DateTime)maxDate);
                case "PM10":
                    return new PollutionPM10(valueOfPolluton, (DateTime)maxDate);
                case "PM2.5":
                    return new PollutionPM25(valueOfPolluton, (DateTime)maxDate);
                default:
                    return new UnknownPollution();
            }
        }
                 

    }

}
