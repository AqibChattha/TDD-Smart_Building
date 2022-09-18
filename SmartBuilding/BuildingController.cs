using Castle.Core.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBuilding
{
    /// <summary>
    /// The building controller class controlls all the fuctions of the building.
    /// </summary>
    class BuildingController
    {
        /// <summary>
        /// The building id. It is unique for each building.
        /// </summary>
        private string buildingId;

        /// <summary>
        /// The state building is in. It can be "Open", "Closed", "out of hours", "fire drill" or "fire alarm"..
        /// </summary>
        private string currentState;
        
        /// <summary>
        /// It is used to store which state we will be transitioning to when we exit from "fire drill" or "fire alarm".
        /// </summary>
        private string historyState;

        /// <summary>
        /// The interface attributes of all the manager and services. The will be used in dependancy injection.
        /// </summary>
        private IDoorManager iDoorManager;
        private IFireAlarmManager iFireAlarmManager;
        private ILightManager iLightManager;
        private IWebService iWebService;
        private IEmailService iEmailService;

        /// <summary>
        /// A one parameter constructor. It sets the buildingId to given id and the currentState is set to "out of hours" by default.
        /// </summary>
        /// <param name="id">The given building id</param>
        public BuildingController(string id)
        {
            buildingId = id.ToLower();
            currentState = "out of hours";
        }

        /// <summary>
        /// A two parameter constructor. It sets the buildingId and currentState to given id and startState respectively.
        /// </summary>
        /// <param name="id">The given building id</param>
        /// <param name="startState">The given building state</param>
        /// <exception cref="ArgumentException">When the given building state is not correnct</exception>
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

        /// <summary>
        /// A six parameter constructor. It sets the buildingId to given id and the currentState is set to "out of hours" by default. The other 5 interface parameter are used for dependancy injection and also testing.
        /// </summary>
        /// <param name="id">The given building id</param>
        /// <param name="iLightManager">Can be an actual object or a substitute</param>
        /// <param name="iFireAlarmManager">Can be an actual object or a substitute</param>
        /// <param name="iDoorManager">Can be an actual object or a substitute</param>
        /// <param name="iWebService">Can be an actual object or a substitute</param>
        /// <param name="iEmailService">Can be an actual object or a substitute</param>
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

        /// <summary>
        /// Get the current state of the building.
        /// </summary>
        /// <returns>Current state of the building</returns>
        public string GetCurrentState()
        {
            return currentState;
        }

        /// <summary>
        /// Method to transition from the current state into a new given state.
        /// </summary>
        /// <param name="state">target state</param>
        /// <returns>True if the state is changed or same state is passed. False if there is any error changing the state or the satate is invalid</returns>
        public bool SetCurrentState(string state)
        {
            // return true if the new state is also the current state
            if (currentState.Equals(state))
            {
                return true;
            }
            // else if the current state if "fire drill" or "fire alarm" it can only transition into the previous state and return true otherwise it will be false
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
            // the new state can only be of the given five types "closed", "out of hours", "open", "fire drill" and "fire alarm"
            else
            {
                if (new List<string>() { "closed", "out of hours", "open", "fire drill", "fire alarm" }.Contains(state))
                {
                    if (state.Equals("fire drill") || state.Equals("fire alarm"))
                    {
                        // we will save the current state as we have to transition out into the same state
                        historyState = currentState;

                        if (state.Equals("fire alarm"))
                        {
                            // if the managers and services are online and building is in "fire alarm" state then set the alarm, open all doors and turn on all lights for evacuation
                            if (iFireAlarmManager != null) iFireAlarmManager.SetAlarm(true);
                            if (iDoorManager != null) iDoorManager.OpenAllDoors();
                            if (iLightManager != null) iLightManager.SetAllLights(true);
                            try
                            {
                                // also send the log of fire alarm to the web to take action immediatly
                                if (iWebService != null) iWebService.LogFireAlarm("fire alarm");
                            }
                            catch (Exception ex)
                            {
                                // incase the fire alarm log fails to be send then we send an email to the "smartbuilding@uclan.ac.uk" entailing the reason of "failed to log alarm"
                                iEmailService.SendMail("smartbuilding@uclan.ac.uk", "failed to log alarm", ex.Message);
                            }
                        }
                    }
                    // if the door manager is online and the state is changed to "open"
                    else if (state.Equals("open") && iDoorManager != null)
                    {
                        if (!iDoorManager.OpenAllDoors())
                        {
                            // if there is any error and we are not able to open the doors than we will return false
                            return false;
                        }
                    }
                    // if the light manager is online and the state is changed to "closed"
                    else if (state.Equals("closed") && iLightManager != null && iDoorManager != null)
                    {
                        if (!iDoorManager.LockAllDoors())
                        {
                            // if there is any error and we are not able to lock the doors than we will return false
                            return false;
                        }
                        else
                        {
                            // we will also turn off all the lights in the building
                            iLightManager.SetAllLights(false);
                        }
                    }

                    // change the currentState to the new given state and return true
                    currentState = state;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get the building id.
        /// </summary>
        /// <returns>building id</returns>
        public string GetBuildingID()
        {
            return buildingId;
        }

        /// <summary>
        /// Set the building a new id. The given id is converted to lower case before it is assigned.
        /// </summary>
        /// <param name="id">new building id</param>
        public void SetBuildingID(string id)
        {
            buildingId = id.ToLower();
        }

        /// <summary>
        /// Gets the status of all manager's devices. If there is any faulty device it will send a log to the web service that an engineer in required.
        /// </summary>
        /// <returns>Manager types with their devices status in a concatinated string</returns>
        public string GetStatusReport()
        {
            LightManager lightManager = iLightManager as LightManager;
            DoorManager doorManager = iDoorManager as DoorManager;
            FireAlarmManager fireAlarmManager = iFireAlarmManager as FireAlarmManager;

            bool engineerRequired = false;
            string logOfEngineer = "";

            // if the web service is available
            if (iWebService != null)
            {
                // if there is any fault in any of the manager the engineer will be dispatched and the faulty manager are mentioned in the log
                if (lightManager.GetStatus().Contains("FAULT")) { engineerRequired = true; logOfEngineer += "Lights,"; }
                if (doorManager.GetStatus().Contains("FAULT")) { engineerRequired = true; logOfEngineer += "Doors,"; }
                if (fireAlarmManager.GetStatus().Contains("FAULT")) { engineerRequired = true; logOfEngineer += "FireAlarm,"; }
                if (engineerRequired == true) iWebService.LogEngineerRequired(logOfEngineer);
            }

            return lightManager.GetStatus() + doorManager.GetStatus() + fireAlarmManager.GetStatus();
        }
    }
}
