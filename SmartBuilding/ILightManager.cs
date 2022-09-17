using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    public interface ILightManager
    {
        void SetLight(bool isOn, int lightID);
        void SetAllLights(bool isOn);
    }
}
