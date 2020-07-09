using IoTNotifier.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTSmsNotifier.DTO
{
    public class PollutionDTO : INotifyPropertyChanged
    {
        private float _valueOfPollution;
        private string _emoticonPath;

        public event PropertyChangedEventHandler PropertyChanged;

        public PollutionDTO(IPollution pollution)
        {
            this.SymbolOfPollution = pollution.SymbolOfPollution;
            this.ValueOfPollution = pollution.ValueOfPollution;
            this.LastMeasurement = pollution.LastMeasurement;
            this.AirQuality = pollution.GetAirQuality();
            this.GetIconPath();
            this.GetEmoticonPath();
        }

        public float ValueOfPollution
        {
            get
            {
                return _valueOfPollution;
            }
            set
            {
                _valueOfPollution = value;
                Notify("ValueOfPollution");
            }
        }        

        public string EmoticonPath
        {
            get
            {
                return _emoticonPath;
            }
            set
            {
                _emoticonPath = value;
                Notify("EmoticonPath");
            }
        }

        public string SymbolOfPollution { get; set; }
        public DateTime LastMeasurement { get; set; }
        public AirQuality AirQuality { get; set; }
        public string IconPath { get; set; }

        private void GetIconPath()
        {
            switch (SymbolOfPollution)
            {
                case "CO":
                    IconPath = "Resources/CO.png";
                    break;
                case "NO2":
                    IconPath = "Resources/NO2.png";
                    break;
                case "O3":
                    IconPath = "Resources/O3.png";
                    break;
                case "PM10":
                    IconPath = "Resources/PM10.png";
                    break;
                case "PM2,5":
                    IconPath = "Resources/PM25.png";
                    break;
                case "SO2":
                    IconPath = "Resources/SO2.png";
                    break;
                default:
                    IconPath = "";
                    break;
            }
        }

        private void GetEmoticonPath()
        {
            switch (AirQuality)
            {
                case AirQuality.BDobry:
                    EmoticonPath = "Resources/1. Bardzo dobra.png";
                    break;
                case AirQuality.Dobry:
                    EmoticonPath = "Resources/2. Dobra.png";
                    break;
                case AirQuality.Umiarkowany:
                    EmoticonPath = "Resources/3. Umiarkowana.png";
                    break;
                case AirQuality.Dostateczny:
                    EmoticonPath = "Resources/4. Dostateczna.png";
                    break;
                case AirQuality.Zly:
                    EmoticonPath = "Resources/5. Zla.png";
                    break;
                case AirQuality.BZly:
                    EmoticonPath = "Resources/6. Bardzo zla.png";
                    break;
                default:
                    EmoticonPath = "";
                    break;
            }
        }

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}