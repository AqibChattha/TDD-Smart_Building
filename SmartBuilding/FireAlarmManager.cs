using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    public class FireAlarmManager : Manager, IFireAlarmManager
    {
        public FireAlarmManager()
        {

        }

        public bool SetAlarm(bool isActive)
        {
            throw new NotImplementedException();
        }
    }
}
