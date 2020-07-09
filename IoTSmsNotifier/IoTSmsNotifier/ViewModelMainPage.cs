using IoTNotifier.Core.Model;
using IoTNotifier.Core.Services;
using IoTSmsNotifier.DTO;
using IoTSmsNotifier.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace IoTSmsNotifier
{
    public class ViewModelMainPage : INotifyPropertyChanged
    {
        private readonly string town = GlobalSettings.Town;
        private ObservableCollection<PollutionDTO> pollutions;
        private PollutionServices pollutionServices;

        public ViewModelMainPage()
        {
            pollutionServices = new PollutionServices();
            //pierwszy raz przy uruchomieniu bierzemy ręcznie zanieszczyszenia, potem metoda cykliczna            
            ListOfPollutions = new ObservableCollection<PollutionDTO>(MaperToPollutionDTO(pollutionServices.GetPollution(town)));


            Task.Factory.StartNew(()=>
            {
                UpdateUI();
            });
            
            ///////////////do testow
            //pollutions = new ObservableCollection<PollutionDTO>
            //{
            //    new PollutionDTO(new PollutionCO(15, DateTime.Now)),
            //    new PollutionDTO(new PollutionNO2(20, DateTime.Now)),
            //    new PollutionDTO(new PollutionO3(25, DateTime.Now)),
            //    new PollutionDTO(new PollutionPM10(15, DateTime.Now)),
            //    new PollutionDTO(new PollutionPM25(20, DateTime.Now)),
            //    new PollutionDTO(new PollutionSO2(25, DateTime.Now))  
            //};
            ///////////////
        }

        private async void UpdateUI()
        {
            while (true)
            {
                try
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () =>
                    {
                        var pollutions = MaperToPollutionDTO(pollutionServices.GetPollution(town));

                        foreach (var pollution in pollutions)
                        {
                            var polutionToUpdate = ListOfPollutions.FirstOrDefault(x => x.SymbolOfPollution == pollution.SymbolOfPollution);

                            if(polutionToUpdate.ValueOfPollution != pollution.ValueOfPollution)
                            {
                                polutionToUpdate.ValueOfPollution = pollution.ValueOfPollution;
                            }

                            if(polutionToUpdate.EmoticonPath != pollution.EmoticonPath)
                            {
                                polutionToUpdate.EmoticonPath = pollution.EmoticonPath;
                            }

                        }
                    });
                    Task.Delay(10000).Wait();
                }
                catch (Exception ex)
                {
                    //// do nothing
                }

            }
        }

        public ObservableCollection <PollutionDTO> ListOfPollutions
        {
            get
            {
                return pollutions;
            }
            set
            {
                pollutions = value; 
                Notify();
            }

        }

        private IList<PollutionDTO> MaperToPollutionDTO(IList<IPollution> pollutionsList)
        {
            IList<PollutionDTO> pollutionDTOList = new List<PollutionDTO>();

            foreach (var pollution in pollutionsList)
            {
                pollutionDTOList.Add(new PollutionDTO(pollution));
            }

            return pollutionDTOList;
        }

        //Implementacja INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
