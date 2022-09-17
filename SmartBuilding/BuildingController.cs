using Castle.Core.Internal;
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
        private string historyState;

        private IDoorManager iDoorManager;
        private IFireAlarmManager iFireAlarmManager;
        private ILightManager iLightManager;
        private IWebService iWebService;
        private IEmailService iEmailService;

        public BuildingController(string id)
        {
            buildingId = id.ToLower();
            currentState = "out of hours";
        }

        public BuildingController(string id, string startState)
        {
            buildingId = id.ToLower();
            if (!startState.IsNullOrEmpty() && new List<string>() { "closed", "out of hours", "open" }.Contains(startState.ToLower()))
            {
                currentState = startState.ToLower();
            }
            else
            {
                throw new ArgumentException("Argument Exception: BuildingController can only be initialised to the following states 'open', 'closed', 'out of hours'");
            }
        }

        public BuildingController(string id, ILightManager iLightManager, IFireAlarmManager iFireAlarmManager, IDoorManager iDoorManager, IWebService iWebService, IEmailService iEmailService)
        {
            buildingId = id.ToLower();
            currentState = "out of hours";
            this.iDoorManager = iDoorManager;
            this.iFireAlarmManager = iFireAlarmManager;
            this.iLightManager = iLightManager;
            this.iWebService = iWebService;
            this.iEmailService = iEmailService;
        }

        public string GetCurrentState()
        {
            return currentState;
        }

        public bool SetCurrentState(string state)
        {
            if (currentState.Equals(state))
            {
                return true;
            }
            else if (currentState.Equals("fire drill") || currentState.Equals("fire alarm"))
            {
                if (historyState.Equals(state))
                {
                    currentState = state;
                    return true;
                }
                else
                {
                    return false;
                }
            } 
            else
            {
                if (new List<string>() { "closed", "out of hours", "open", "fire drill", "fire alarm" }.Contains(state))
                {
                    if (state.Equals("fire drill") || state.Equals("fire alarm")){
                        historyState = currentState;
                        
                        if (state.Equals("fire alarm"))
                        {
                            if (iFireAlarmManager != null) iFireAlarmManager.SetAlarm(true);
                            if (iDoorManager != null) iDoorManager.OpenAllDoors();
                            if (iLightManager != null) iLightManager.SetAllLights(true);
                            if (iWebService != null) iWebService.LogFireAlarm("fire alarm");
                        }
                    }
                    else if (state.Equals("open") && iDoorManager != null)
                    {
                        if (!iDoorManager.OpenAllDoors())
                        {
                            return false;
                        }
                    }
                    else if (state.Equals("closed") && iLightManager != null)
                    {
                        if (!iDoorManager.LockAllDoors())
                        {
                            return false;
                        }
                        else
                        {
                            iLightManager.SetAllLights(false);
                        }
                    }
                    currentState = state;
                    return true;
                }
                else
                {
                    return false;
                }
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
            LightManager lightManager = iLightManager as LightManager;
            DoorManager doorManager = iDoorManager as DoorManager;
            FireAlarmManager fireAlarmManager = iFireAlarmManager as FireAlarmManager;
            return lightManager.GetStatus() + doorManager.GetStatus() + fireAlarmManager.GetStatus();
        }
    }
}
