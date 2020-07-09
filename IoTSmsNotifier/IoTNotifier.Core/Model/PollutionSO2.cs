using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Model
{
    public class PollutionSO2 : IPollution
    {
        public PollutionSO2(float valueOfPollution, DateTime lastMeasurement)
        {
            SymbolOfPollution = "SO2";
            ValueOfPollution = valueOfPollution;
            LastMeasurement = lastMeasurement;
        }

        public string SymbolOfPollution { get; }
        public float ValueOfPollution { get; }
        public DateTime LastMeasurement { get; }

        public AirQuality GetAirQuality()
        {
            if (ValueOfPollution <= 50)
                return AirQuality.BDobry;
            else if (ValueOfPollution <= 100)
                return AirQuality.Dobry;
            else if (ValueOfPollution <= 200)
                return AirQuality.Umiarkowany;
            else if (ValueOfPollution <= 350)
                return AirQuality.Dostateczny;
            else if (ValueOfPollution <= 500)
                return AirQuality.Zly;
            else if (ValueOfPollution > 500)
                return AirQuality.BZly;
            else
                return AirQuality.Nieznany;
        }
    }
}
