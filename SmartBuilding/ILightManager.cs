using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// Interface for the Light Manager. This interface is used to allow the Building Controller to interact with the Light Manager class without having to know the implementation details of the Light Manager class.
    /// </summary>
    public interface ILightManager
    {
        /// <summary>
        /// Method to turn all the lights on or off in the building. 
        /// </summary>
        /// <param name="isOn">true for on and false for off</param>
        void SetLight(bool isOn, int lightID);

        /// <summary>
        /// Method to turn the light with given id on or off in the building.
        /// </summary>
        /// <param name="isOn">true for on and false for off</param>
        /// <param name="lightID">The light's unique id</param>
        void SetAllLights(bool isOn);
    }
}
