using IoTNotifier.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTNotifier.Core.Repositories
{
    public interface IPollutionInfoRepository
    {
       IList<IPollution> GetPollutions(string city);
    }
}
