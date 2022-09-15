using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    interface IWebService
    {
        void LogStateChange(string logDetails);
        void LogEngineerRequired(string logDetails);
        void LogFireAlarm(string logDetails);
    }
}
