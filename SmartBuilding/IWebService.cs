using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    public interface IWebService
    {
        void LogStateChange(string logDetails);
        void LogEngineerRequired(string logDetails);
        void LogFireAlarm(string logDetails);
    }
}
