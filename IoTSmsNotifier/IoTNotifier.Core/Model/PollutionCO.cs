using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Model
{
    public class PollutionCO : IPollution
    {
        public PollutionCO(float valueOfPollution, DateTime lastMeasurement)
        {
            SymbolOfPollution = "CO";
            ValueOfPollution = valueOfPollution;
            LastMeasurement = lastMeasurement;
        }

        public string SymbolOfPollution { get; }
        public float ValueOfPollution { get; }
        public DateTime LastMeasurement { get; }        

        public AirQuality GetAirQuality()
        {
            if (ValueOfPollution <= 2000)
                return AirQuality.BDobry;
            else if (ValueOfPollution <= 6000)
                return AirQuality.Dobry;
            else if (ValueOfPollution <= 10000)
                return AirQuality.Umiarkowany;
            else if (ValueOfPollution <= 14000)
                return AirQuality.Dostateczny;
            else if (ValueOfPollution <= 20000)
                return AirQuality.Zly;
            else if (ValueOfPollution > 20000)
                return AirQuality.BZly;
            else
                return AirQuality.Nieznany;
        }

        //AirQuality IPollution.GetAirQuality()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
