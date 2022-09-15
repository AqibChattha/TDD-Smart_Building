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

        public string GetCurrentState()
        {
            return CurrentState;
        }

        public bool SetCurrentState(string state)
        {
            CurrentState = state;
            return true;
        }

        public string GetBuildingID()
        {
            return buildingId;
        }

        public bool SetBuildingID(string id)
        {
            buildingId = id;
            return true;
        }

        public string GetStatusReport()
        {
            return null;
        }
    }
}
