using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Model
{
    public class PollutionNO2 : IPollution
    {
        public PollutionNO2(float valueOfPollution, DateTime lastMeasurement)
        {
            SymbolOfPollution = "NO2";
            ValueOfPollution = valueOfPollution;
            LastMeasurement = lastMeasurement;
        }

        public string SymbolOfPollution { get; }
        public float ValueOfPollution { get; }
        public DateTime LastMeasurement { get; }

        public AirQuality GetAirQuality()
        {
            if (ValueOfPollution <= 40)
                return AirQuality.BDobry;
            else if (ValueOfPollution <= 100)
                return AirQuality.Dobry;
            else if (ValueOfPollution <= 150)
                return AirQuality.Umiarkowany;
            else if (ValueOfPollution <= 200)
                return AirQuality.Dostateczny;
            else if (ValueOfPollution <= 400)
                return AirQuality.Zly;
            else if (ValueOfPollution > 400)
                return AirQuality.BZly;
            else
                return AirQuality.Nieznany;
        }
    }
}
