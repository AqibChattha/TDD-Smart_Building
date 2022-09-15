using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    class BuildingController
    {
        private string buildingId;
        private string CurrentState;

        public BuildingController(string id)
        {
            this.buildingId = id;
        }

        public BuildingController()
        {
            
        }
    }
}
