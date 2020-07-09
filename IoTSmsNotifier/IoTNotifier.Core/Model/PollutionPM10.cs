using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Model
{
    public class PollutionPM10 : IPollution
    {
        public PollutionPM10(float valueOfPollution, DateTime lastMeasurement)
        {
            SymbolOfPollution = "PM10";
            ValueOfPollution = valueOfPollution;
            LastMeasurement = lastMeasurement;
        }

        public string SymbolOfPollution { get; }
        public float ValueOfPollution { get; }
        public DateTime LastMeasurement { get; }

        public AirQuality GetAirQuality()
        {
            if (ValueOfPollution <= 20)
                return AirQuality.BDobry;
            else if (ValueOfPollution <= 60)
                return AirQuality.Dobry;
            else if (ValueOfPollution <= 100)
                return AirQuality.Umiarkowany;
            else if (ValueOfPollution <= 140)
                return AirQuality.Dostateczny;
            else if (ValueOfPollution <= 200)
                return AirQuality.Zly;
            else if (ValueOfPollution > 200)
                return AirQuality.BZly;
            else
                return AirQuality.Nieznany;
        }
    }
}
