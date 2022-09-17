using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    public class LightManager : Manager, ILightManager
    {
        public LightManager()
        {

        }
        
        public void SetAllLights(bool isOn)
        {
            throw new NotImplementedException();
        }

        public void SetLight(bool isOn, int lightID)
        {
            throw new NotImplementedException();
        }
        
    }
}
