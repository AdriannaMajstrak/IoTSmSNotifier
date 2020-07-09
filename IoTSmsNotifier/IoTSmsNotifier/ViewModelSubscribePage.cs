using IoTNotifier.Core.Services;
using IoTSmsNotifier.Settings;
using IoTSmsNotifier.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IoTSmsNotifier
{
    public class ViewModelSubscribePage : INotifyPropertyChanged
    {
        private IPollutionService pollutionService;
        private ObservableCollection<string> hours;
        private string phoneNumber;
        private string chosenHour = "12:00";
        private bool dailySms;
        private bool alertSms;
        private bool _saveEnabled;
        private bool _phoneError;
        private bool _phoneTouched;

        public event EventHandler CloseMe;

        public ViewModelSubscribePage()
        {
            pollutionService = new PollutionServices();
            hours = new ObservableCollection<string>();
            FillHoursList();
            DailySms = true;
        }

        public bool DailySms
        {
            get
            {
                return dailySms;
            }
            set
            {
                dailySms = value;
                Notify();
            }
        }

        public bool PhoneError
        {
            get
            {
                return _phoneError && _phoneTouched;
            }
            set
            {
                _phoneError = value;
                Notify();
            }
        }

        public bool AlertSms
        {
            get
            {
                return alertSms;
            }
            set
            {
                alertSms = value;
                Notify();
            }
        }
       

        public ObservableCollection<string> Hours
        {
            get
            {
                return hours;
            }
            set
            {
                hours = value;
                Notify();
            }
        }

        public string ChosenHour
        {
            get
            {
                return chosenHour;
            }
            set
            {
                chosenHour = value;
                Notify();
                Notify("SaveEnabled");
            }
        }

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                _phoneTouched = true;
                Regex regex = new Regex("^\\d{9}$");
                if (regex.Match(value).Success)
                {
                    phoneNumber = value;
                    PhoneError = false;
                }
                else
                {
                    PhoneError = true;
                }

                Notify();
                Notify("SaveEnabled");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void FillHoursList()
        {
            for (int i = 6; i < 22; i++)
            {
                hours.Add(i.ToString() + ":00");
            }
        }

        public bool SaveEnabled
        {
            get
            {
                _phoneTouched = true;
                return !PhoneError && !string.IsNullOrWhiteSpace(PhoneNumber) && (AlertSms || (!string.IsNullOrWhiteSpace(ChosenHour) && DailySms));
            }
            set
            {
                _saveEnabled = value;
                Notify();
            }
        }

        public ICommand SaveSubscriptionInfo
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (this.DailySms)
                    {
                        DateTime hour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(this.ChosenHour.Remove(2)), 0, 0);
                        pollutionService.SubscribeCycleNotification(GlobalSettings.Town, this.phoneNumber, hour);
                    }
                    else if (this.AlertSms)
                    {
                        DateTime hour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(this.ChosenHour.Remove(2)), 0, 0);
                        pollutionService.SubscribeWarnings(GlobalSettings.Town, this.phoneNumber);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    this.CloseMe?.Invoke(this, null);

                });
            }
        }

        public ICommand RadioButtonAlertChosed
        {
            get
            {
                return new RelayCommand(() =>
                {
                    AlertSms = true;
                    DailySms = false;
                    Notify("SaveEnabled");
                });
            }
        }

        public ICommand RadioButtonDailyChosed
        {
            get
            {
                return new RelayCommand(() =>
                {
                    DailySms = true;
                    AlertSms = false;
                    Notify("SaveEnabled");
                }
               );
            }
        }
    }
}
