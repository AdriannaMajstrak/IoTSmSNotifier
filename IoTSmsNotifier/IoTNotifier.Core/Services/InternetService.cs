using IoTNotifier.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTNotifier.Core.Services
{
    public class InternetService : IInternetService
    {
        IInternetRepository internetRepository = new InternetRepository();

        public bool CheckInternetConnection()
        {
            try
            {
                return internetRepository.CheckInternetConnection();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
