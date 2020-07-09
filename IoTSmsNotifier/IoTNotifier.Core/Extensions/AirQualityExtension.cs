using IoTNotifier.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Extensions
{
    public static class AirQualityExtension
    {
        public static string ToAirQualityString(this AirQuality airQuaity)
        {
            switch (airQuaity)
            {
                case AirQuality.BDobry:
                    return "Bardzo dobry";                    
                case AirQuality.Dobry:
                    return "Dobry"; 
                case AirQuality.Umiarkowany:
                    return "Umiarkowany";
                case AirQuality.Dostateczny:
                    return "Dostateczny";
                case AirQuality.Zly:
                    return "Zły";
                case AirQuality.BZly:
                    return "Bardzo zły";
                case AirQuality.Nieznany:
                    return "?";
                default:
                    return "?";
            }
        }
    }
}
