using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// The Fire Alarm Manager class is a manager that manages the fire alarms in the building. It is a subclass of the Manager class. It provides the functionality to turn the alarms on and off.
    /// </summary>
    public class FireAlarmManager : Manager, IFireAlarmManager
    {
        /// <summary>
        /// Default constructor, to be used in testing.
        /// </summary>
        public FireAlarmManager()
        {

        }

        /// <summary>
        /// Set the fire alarm in the building on or off. The method is not implemented as it is only used for testing purposes.
        /// </summary>
        /// <param name="isActive">target value</param>
        /// <returns>True if the state changes otherwise false</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool SetAlarm(bool isActive)
        {
            throw new NotImplementedException();
        }
    }
}
