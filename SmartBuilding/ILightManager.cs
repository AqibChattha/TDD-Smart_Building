using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    interface ILightManager
    {
        bool SetLight(bool isOn, int lightID);
        bool SetAllLights(bool isOn);
    }
}
