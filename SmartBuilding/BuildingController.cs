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
        private string currentState;

        public BuildingController(string id)
        {
            this.buildingId = id.ToLower();
            this.currentState = "out of hours";
        }

        public string GetCurrentState()
        {
            return currentState;
        }

        public bool SetCurrentState(string state)
        {
            if (new List<string>() { "closed", "out of hours", "open", "fire drill", "fire alarm" }.Contains(state))
            {
                currentState = state;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetBuildingID()
        {
            return buildingId;
        }

        public void SetBuildingID(string id)
        {
            buildingId = id.ToLower();
        }

        public string GetStatusReport()
        {
            return null;
        }
    }
}
