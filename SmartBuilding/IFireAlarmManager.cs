using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// Interface for the Fire Alarm. This interface is used to allow the Building Controller to interact with the Fire Alarm Manager class without having to know the implementation details of the Fire Alarm Manager class.
    /// </summary>
    public interface IFireAlarmManager
    {
        /// <summary>
        /// Set the fire alarm in the building on or off.
        /// </summary>
        /// <param name="isActive">target value</param>
        /// <returns>True if the state changes otherwise false</returns>
        bool SetAlarm(bool isActive);
    }
}
