using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// The Light Manager class is a manager that manages the lights in the building. It is a subclass of the Manager class. It provides the functionality to turn the lights on and off.
    /// </summary>
    public class LightManager : Manager, ILightManager
    {
        /// <summary>
        /// Default constructor, to be used in testing.
        /// </summary>
        public LightManager()
        {

        }

        /// <summary>
        /// Method to turn all the lights on or off in the building. The method is not implemented as it is only used for testing purposes.
        /// </summary>
        /// <param name="isOn">true for on and false for off</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetAllLights(bool isOn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method to turn the light with given id on or off in the building. The method is not implemented as it is only used for testing purposes.
        /// </summary>
        /// <param name="isOn">true for on and false for off</param>
        /// <param name="lightID">The light's unique id</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetLight(bool isOn, int lightID)
        {
            throw new NotImplementedException();
        }
        
    }
}
