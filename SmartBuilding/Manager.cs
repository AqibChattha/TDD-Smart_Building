using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// The 'Manager' class is the base class for all managers in the system. It provides the basic functionality for all managers.
    /// </summary>
    public abstract class Manager
    {
        /// <summary>
        /// Attribute to keep track if the engineer in required by the managers to fix any faulty devices.
        /// </summary>
        private bool engineerRequired;
        
        /// <summary>
        /// This method will give the type of manager and status of all the devices its managing. Its virtual so we can make substitutes of it during the tests.
        /// </summary>
        /// <returns>Manager type and every device status in a comma seperated string</returns>
        public virtual string GetStatus()
        {
            return "Status";
        }

        /// <summary>
        /// Set whether the engineer is required or not for the given manager.
        /// </summary>
        /// <param name="isEngineerNeeded">target value</param>
        /// <returns>true if the value is changed otherwise false</returns>
        public bool SetEngineerRequired(bool isEngineerNeeded)
        {
            if (engineerRequired != isEngineerNeeded)
            {
                engineerRequired = isEngineerNeeded;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
