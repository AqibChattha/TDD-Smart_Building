using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// Interface for web service. This interface is used to allow the Building Controller to interact with the web service class without having to know the implementation details of the web service class.
    /// </summary>
    public interface IWebService
    {
        /// <summary>
        /// Method to log the details whenever the building state changes.
        /// </summary>
        /// <param name="logDetails">Details of the log</param>
        void LogStateChange(string logDetails);

        /// <summary>
        /// Method to log the details whenever a engineer is requierd.
        /// </summary>
        /// <param name="logDetails">Details of the log</param>
        void LogEngineerRequired(string logDetails);

        /// <summary>
        /// Method to log the details whenever a fire alarm is rung.
        /// </summary>
        /// <param name="logDetails">Details of the log</param>
        void LogFireAlarm(string logDetails);
    }
}
