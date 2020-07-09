using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Model
{
    public class PollutionPM25 : IPollution
    {
        public PollutionPM25(float valueOfPollution, DateTime lastMeasurement)
        {
            SymbolOfPollution = "PM2,5";
            ValueOfPollution = valueOfPollution;
            LastMeasurement = lastMeasurement;
        }

        public string SymbolOfPollution { get; }
        public float ValueOfPollution { get; }
        public DateTime LastMeasurement { get; }

        public AirQuality GetAirQuality()
        {
            if (ValueOfPollution <= 12)
                return AirQuality.BDobry;
            else if (ValueOfPollution <= 36)
                return AirQuality.Dobry;
            else if (ValueOfPollution <= 60)
                return AirQuality.Umiarkowany;
            else if (ValueOfPollution <= 84)
                return AirQuality.Dostateczny;
            else if (ValueOfPollution <= 120)
                return AirQuality.Zly;
            else if (ValueOfPollution > 120)
                return AirQuality.BZly;
            else
                return AirQuality.Nieznany;
        }
    }
}
