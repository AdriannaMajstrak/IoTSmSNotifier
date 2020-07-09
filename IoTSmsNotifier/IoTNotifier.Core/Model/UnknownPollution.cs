using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Model
{
    public class UnknownPollution : IPollution
    {
        public string SymbolOfPollution => "Unknown";

        public float ValueOfPollution => 0;

        public DateTime LastMeasurement => throw new NotImplementedException();

        public AirQuality GetAirQuality()
        {
            throw new NotImplementedException();
        }
    }
}
