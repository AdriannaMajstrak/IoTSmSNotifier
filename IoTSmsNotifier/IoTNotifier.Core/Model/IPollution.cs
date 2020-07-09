using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNotifier.Core.Model
{
    public interface IPollution
    {
        string SymbolOfPollution { get; }
        float ValueOfPollution { get; }
        DateTime LastMeasurement { get; }

        AirQuality GetAirQuality();
    }
}
