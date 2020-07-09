using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Model
{
    public class PollutionO3 : IPollution
    {
        public PollutionO3(float valueOfPollution, DateTime lastMeasurement)
        {
            SymbolOfPollution = "O3";
            ValueOfPollution = valueOfPollution;
            LastMeasurement = lastMeasurement;
        }

        public string SymbolOfPollution { get; }
        public float ValueOfPollution { get; }
        public DateTime LastMeasurement { get; }

        public AirQuality GetAirQuality()
        {
            if (ValueOfPollution <= 24)
                return AirQuality.BDobry;
            else if (ValueOfPollution <= 70)
                return AirQuality.Dobry;
            else if (ValueOfPollution <= 120)
                return AirQuality.Umiarkowany;
            else if (ValueOfPollution <= 160)
                return AirQuality.Dostateczny;
            else if (ValueOfPollution <= 240)
                return AirQuality.Zly;
            else if (ValueOfPollution > 240)
                return AirQuality.BZly;
            else
                return AirQuality.Nieznany;
        }
    }
}
