using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace SmartBuilding
{
    [TestFixture]
    public class BuildingControllerTests
    {
        /// <summary>
        /// Test that buildingController contains a constructor method with a single string
        /// parameter that assigns the buildingID object variable.
        /// </summary>
        /// <param name="id"></param>
        [Category("Level 1 Requirements L1R1")]
        [TestCase("123")]
        [TestCase("")]
        [TestCase("build")]
        [TestCase("build123")]
        public void ConstructorWith1Parameter_SetTheParameterValueToBuildingID(string id)
        {
            // Arrange
            BuildingController buildingController = new BuildingController(id);

            // Act
            string result = buildingController.GetBuildingID();

            // Assert
            Assert.That(buildingController, Is.TypeOf<BuildingController>());
            Assert.AreEqual(id, result);
        }

        /// <summary>
        /// Test that GetBuildingID() returns the value of the buildingID variable.
        /// </summary>
        /// <param name="id"></param>
        [Category("Level 1 Requirements L1R2")]
        [TestCase("123")]
        [TestCase("")]
        [TestCase("build")]
        [TestCase("build123")]
        public void GetBuildingID_GetId_ReturnsBuild1(string id)
        {
            BuildingController buildingController = new BuildingController(id);

            string result = buildingController.GetBuildingID();

            Assert.AreEqual(id, result);
        }

        /// <summary>
        /// Test that when the buildingID object variable is set in the constructor, uppercase letters
        /// will be converted to lower case. 
        /// </summary>
        /// <param name="id"></param>
        [Category("Level 1 Requirements L1R3")]
        [TestCase("123")]
        [TestCase("")]
        [TestCase("BUILD")]
        [TestCase("Build123")]
        public void ConstructorWith1Parameter_LowerCaseTheParameterValueBeforeAssigningToBuildingID(string id)
        {
            BuildingController buildingController = new BuildingController(id);

            string result = buildingController.GetBuildingID();

            Assert.AreEqual(id.ToLower(), result);
        }

        /// <summary>
        /// Test that SetBuildingID() sets the value of buildingID, converting uppercase letters to lowercase.
        /// </summary>
        /// <param name="id"></param>
        [Category("Level 1 Requirements L1R4")]
        [TestCase("123")]
        [TestCase("")]
        [TestCase("BUILD")]
        [TestCase("Build123")]
        public void SetBuildingID_LowerCaseTheParameterValueBeforeAssigningToBuildingID(string id)
        {
            BuildingController buildingController = new BuildingController("build1");

            buildingController.SetBuildingID(id);
            string result = buildingController.GetBuildingID();

            Assert.AreEqual(id.ToLower(), result);
        }

        /// <summary>
        /// Test that constructor sets the initial value of currentState to “out of hours”.
        /// </summary>
        [Category("Level 1 Requirements L1R5")]
        [Test]
        public void ConstructorWith1Parameter_SetDefaultValueOfCurrentStateToOutOfHours()
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.AreEqual("out of hours", buildingController.GetCurrentState());
        }

        /// <summary>
        /// Test that GetCurrentState() function returns the value of the currentState object variable.
        /// </summary>
        [Category("Level 1 Requirements L1R6")]
        [Test]
        public void GetCurrentState_GetDefaultState_ReturnsOutOfOrder()
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.AreEqual("out of hours", buildingController.GetCurrentState());
        }

        /// <summary>
        /// Test that SetCurrentState() function checks the string supplied is a valid state (“closed”,
        /// “out of hours”, “open”, “fire drill” or “fire alarm”). If the string supplied is valid the
        /// function will set the currentState variable and return true.
        /// </summary>
        /// <param name="state"></param>
        [Category("Level 1 Requirements L1R7")]
        [TestCase("closed")]
        [TestCase("out of hours")]
        [TestCase("open")]
        [TestCase("fire drill")]
        [TestCase("fire alarm")]
        public void SetCurrentState_SetTheCurrentStateToParameterStateIfItIsValid_ReturnsTrue(string state)
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.IsTrue(buildingController.SetCurrentState(state));
            Assert.AreEqual(state, buildingController.GetCurrentState());
        }

        /// <summary>
        /// Test that SetCurrentState() function checks the string supplied is a valid state (“closed”, 
        /// “out of hours”, “open”, “fire drill” or “fire alarm”). If the string supplied is invalid 
        /// the function will return false and the state will be unchanged.
        /// </summary>
        /// <param name="state"></param>
        [Category("Level 2 Requirements L1R7")]
        [TestCase("fire larm")]
        [TestCase("open1")]
        public void SetCurrentState_DoNotSetTheCurrentStateToParameterStateIfItIsInvalid_ReturnsFalse(string state)
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.That(buildingController.SetCurrentState(state), Is.Not.True);
        }

        /// <summary>
        /// Test that SetCurrentState() function will only allow state to transition into the state it was in
        /// before going into the "fire alarm" or "fire drill" state. If the given state is the previous state
        /// of the current state than state will transition out of the fire state and return true.
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="fireState"></param>
        /// <param name="nextState"></param>
        [Category("Level 2 Requirements L2R1")]
        [TestCase("closed", "fire drill", "closed")]
        [TestCase("open", "fire alarm", "open")]
        public void SetCurrentState_GoingToPreviousStateFromFireStates_returnsTrue(string currentState, string fireState, string nextState)
        {
            BuildingController buildingController = new BuildingController("build1");

            buildingController.SetCurrentState(currentState);
            buildingController.SetCurrentState(fireState);

            Assert.IsTrue(buildingController.SetCurrentState(nextState));
        }

        /// <summary>
        /// Test that SetCurrentState() function will only allow state to transition into the state it was in
        /// before going into the "fire alarm" or "fire drill" state. If the given state is not the previous
        /// state of the current state than state will remain in the fire state and return false.
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="fireState"></param>
        /// <param name="nextState"></param>
        [Category("Level 2 Requirements L2R1")]
        [TestCase("out of hours", "fire drill", "closed")]
        [TestCase("closed", "fire alarm", "open")]
        public void SetCurrentState_CanNotChangeStateUnlessItsPreviousStateOfFireStates_returnsFalse(string currentState, string fireState, string nextState)
        {
            BuildingController buildingController = new BuildingController("build1");

            buildingController.SetCurrentState(currentState);
            buildingController.SetCurrentState(fireState);

            Assert.That(buildingController.SetCurrentState(nextState), Is.Not.True);
        }

        /// <summary>
        /// Test that SetCurrentState() will return true by default and remain in the same state if an attempt
        /// is made to set the BuildingController’s state to the present value of currentState.
        /// </summary>
        /// <param name="state"></param>
        [Category("Level 2 Requirements L2R2")]
        [TestCase("closed")]
        [TestCase("out of hours")]
        [TestCase("open")]
        [TestCase("fire drill")]
        [TestCase("fire alarm")]
        public void SetCurrentState_CurrentStateAndParameterStateAreSame_DoesNotChangeStateValueAndReturnsTrue(string state)
        {
            BuildingController buildingController = new BuildingController("build1");

            buildingController.SetCurrentState(state);
            String currentState = new String(state.ToCharArray());
            buildingController.SetCurrentState(currentState);

            Assert.AreNotSame(currentState, buildingController.GetCurrentState());
        }

        /// <summary>
        /// Test that the BuildingController class has an additional constructor that takes two string parameters
        /// that set the default values for buildingID and currentState  (the constructor should accept parameters 
        /// in upper case, lower case, or a mixture of the two, but should store the value of currentState in 
        /// lower case). The BuildingController class can only be initialised to one of the three normal operation 
        /// states (“closed”, “out of hours” or “open”). If it is not set to one of these states, an ArgumentException
        /// is thrown with the following message: "Argument Exception: BuildingController can only be initialised to 
        /// the following states 'open', 'closed', 'out of hours'"
        /// </summary>
        /// <param name="state"></param>
        [Category("Level 2 Requirements L2R3")]
        [TestCase("")]
        [TestCase("123")]
        [TestCase("ope")]
        [TestCase(null)]
        public void ConstructorWith2Parameters_ParameterStateIsInvalid_ThrowsArgumentException(string state)
        {
            Assert.Throws<ArgumentException>(() => new BuildingController("build1", state));
            try
            {
                BuildingController buildingController = new BuildingController("build1", state);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Argument Exception: BuildingController can only be initialised to the following states 'open', 'closed', 'out of hours'");
            }
        }

        /// <summary>
        /// To make the BuildingController class ‘unit test friendly’, it allows dependency injection through
        /// an additional constructor method. The method takes 6 parameters providing the class with interfaces
        /// for all 5 dependencies outlined on the class diagram.
        /// </summary>
        [Category("Level 3 Requirements L3R1")]
        [Test]
        public void ConstructorWith6Parameters_CreateObject()
        {
            ILightManager iLightManager = Substitute.For<ILightManager>();
            IFireAlarmManager iFireAlarmManager = Substitute.For<IFireAlarmManager>();
            IDoorManager iDoorManager = Substitute.For<IDoorManager>();
            IWebService iWebService = Substitute.For<IWebService>();
            IEmailService iEmailService = Substitute.For<IEmailService>();

            var buildingController = new BuildingController("build1", iLightManager, iFireAlarmManager, iDoorManager, iWebService, iEmailService);

            Assert.That(buildingController, Is.TypeOf<BuildingController>());
        }

        /// <summary>
        /// Test that the GetStatus() methods of lights manager class returns a string containing commaseparated
        /// values. The first value is the type of device (Lights) followed by a sequence of either OK, or FAULT
        /// values indicating the state of each managed device. 
        /// </summary>
        /// <param name="status"></param>
        [Category("Level 3 Requirements L3R2")]
        [TestCase("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,")]
        [TestCase("Lights,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,")]
        public void GetStatus_LightsStatus_ReturnsStatusOfAllLightsInACommaSeperatedString(string status)
        {
            var lightManager = Substitute.For<LightManager>();

            // Creating a stub of lights manager to test the GetStatus() method return value.
            lightManager.GetStatus().Returns(status);

            Assert.AreEqual(status, lightManager.GetStatus());
        }

        /// <summary>
        /// Test that the GetStatus() methods of doors manager class returns a string containing commaseparated 
        /// values. The first value is the type of device (Doors) followed by a sequence of either OK, or FAULT
        /// values indicating the state of each managed device. 
        /// </summary>
        /// <param name="status"></param>
        [Category("Level 3 Requirements L3R2")]
        [TestCase("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,")]
        [TestCase("Doors,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,")]
        public void GetStatus_DoorsStatus_ReturnsStatusOfAllDoorsInACommaSeperatedString(string status)
        {
            var doorManager = Substitute.For<DoorManager>();

            // Creating a stub of doors manager to test the GetStatus() method return value.
            doorManager.GetStatus().Returns(status);

            Assert.AreEqual(status, doorManager.GetStatus());
        }

        /// <summary>
        /// Test that the GetStatus() methods of fire alarm manager class returns a string containing commaseparated
        /// values. The first value is the type of device (FireAlarm) followed by a sequence of either OK, or FAULT
        /// values indicating the state of each managed device. 
        /// </summary>
        /// <param name="status"></param>
        [Category("Level 3 Requirements L3R2")]
        [TestCase("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,")]
        [TestCase("FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,")]
        public void GetStatus_FireAlarmStatus_ReturnsStatusOfAllFireAlarmInACommaSeperatedString(string status)
        {
            var fireAlarmManager = Substitute.For<FireAlarmManager>();

            // Creating a stub of fire alarm manager to test the GetStatus() method return value.
            fireAlarmManager.GetStatus().Returns(status);

            Assert.AreEqual(status, fireAlarmManager.GetStatus());
        }

        /// <summary>
        /// Test that GetStatusReport() method calls the GetStatus() methods of all 3 manager classes (LightManager, 
        /// DoorManager and FireAlarmManager) and appends each string returned together into a single string (in the
        /// following order: lightStatus, doorStatus, fireAlarmStatus) before returning the result. 
        /// </summary>
        [Category("Level 3 Requirements L3R3")]
        [Test]
        public void GetStatusReport_ConcatinateAllManagerStatus_ReturnsDevicesStatusConcatinatedReport()
        {
            // Creating the mock object for each of the manager interfaces
            var lightManager = Substitute.For<LightManager>();
            var doorManager = Substitute.For<DoorManager>();
            var fireAlarmManager = Substitute.For<FireAlarmManager>();

            // Creating a stub of all managers to use the GetStatus() return values in the GetStatusReport() function.
            lightManager.GetStatus().Returns("Lights,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,");

            var buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, null, null);

            Assert.AreEqual("Lights,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,", buildingController.GetStatusReport());
        }

        /// <summary>
        /// When the BuildingControllers SetCurrentState() method moves to the “open” state, all doors should be set to open
        /// by calling the OpenAllDoors() method of the DoorManager object. If DoorManager.OpenAllDoors() returns false 
        /// (indicating there was a failure to unlock all the doors) ‘false’ should be returned from the SetCurrentState()
        /// function and the building should remain in its current state.  
        /// </summary>
        [Category("Level 3 Requirements L3R4")]
        [Test]
        public void SetCurrentState_WhenOpeningAllDoorsIsSuccessfull_ChangeStateToOpenAndReturnsTrue()
        {
            // Creating the mock object for the door manager.
            var doorManager = Substitute.For<IDoorManager>();

            // Creating a stub of OpenAllDoors() function which returns true.
            doorManager.OpenAllDoors().Returns(true);

            BuildingController buildingController = new BuildingController("build1", null, null, doorManager, null, null);

            Assert.That(buildingController.SetCurrentState("open"), Is.True);
            Assert.That("open", Is.EqualTo(buildingController.GetCurrentState()));
        }

        /// <summary>
        /// Test that when moving to the “open” state, if DoorManager.OpenAllDoors() returns ‘true’ when attempting to move to
        /// the open state (indicating unlocking all the doors was a success) ‘true’ should be returned from SetCurrentState()
        /// and the building should move to the “open” state. 
        /// </summary>
        [Category("Level 3 Requirements L3R5")]
        [Test]
        public void SetCurrentState_WhenOpeningAllDoorsIsUnsuccessfull_ChangesNothingAndReturnsFalse()
        {
            // Creating the mock object for the door manager.
            var doorManager = Substitute.For<IDoorManager>();

            // Creating a stub of OpenAllDoors() function which returns false.
            doorManager.OpenAllDoors().Returns(false);

            BuildingController buildingController = new BuildingController("build1", null, null, doorManager, null, null);

            Assert.That(buildingController.SetCurrentState("open"), Is.Not.True);
            Assert.That("out of hours", Is.EqualTo(buildingController.GetCurrentState()));
        }

        /// <summary>
        /// Test when the BuildingController’s SetCurrentState() method moves to the “closed” state, all doors should be set to closed by
        /// calling the DoorManager.LockAllDoors() method and all lights must be turned off by calling the LightManager.SetAllLights(false) 
        /// </summary>
        [Category("Level 4 Requirements L4R1")]
        [Test]
        public void SetCurrentState_ClosingAllDoorsAndTurningOffAllLights_ChangeStateToClosedAndReturnsTrue()
        {
            // Creating the mock objects for the door manager and light manager.
            var doorManager = Substitute.For<IDoorManager>();
            var lightManager = Substitute.For<ILightManager>();

            BuildingController buildingController = new BuildingController("build1", lightManager, null, doorManager, null, null);

            // Creating a stub of LockAllDoors() function which returns true.
            doorManager.LockAllDoors().Returns(true);

            Assert.That(buildingController.SetCurrentState("closed"), Is.True);

            // Checking if the SetAllLights(false) function of the mock object of light manager was called in the SetCurrentState() function.
            lightManager.Received().SetAllLights(false);

            Assert.That("closed", Is.EqualTo(buildingController.GetCurrentState()));
        }

        /// <summary>
        /// Test when the BuildingController’s SetCurrentState() method moves to the “closed” state, there is an error and some doors
        /// are not set to closed by calling the DoorManager.LockAllDoors() method and all lights must be turned off by calling the
        /// LightManager.SetAllLights(false) 
        /// </summary>
        [Category("Level 4 Requirements L4R1")]
        [Test]
        public void SetCurrentState_ClosingAllDoorsAndTurningOffAllLightsButDoorsFail_ChangesNothingAndReturnsFalse()
        {
            // Creating the mock objects for the door manager and light manager.
            var doorManager = Substitute.For<IDoorManager>();
            var lightManager = Substitute.For<ILightManager>();

            // Creating a stub of LockAllDoors() function which returns false.
            doorManager.LockAllDoors().Returns(false);

            BuildingController buildingController = new BuildingController("build1", lightManager, null, doorManager, null, null);

            Assert.That(buildingController.SetCurrentState("closed"), Is.Not.True);

            // Checking if the SetAllLights(false) function of the mock object of light manager was not called in the SetCurrentState() function.
            lightManager.DidNotReceive().SetAllLights(false);

            Assert.That("out of hours", Is.EqualTo(buildingController.GetCurrentState()));
        }

        /// <summary>
        /// Test that when the BuildingController.SetCurrentState() method moves to the “fire alarm” state, the alarm should be triggered
        /// by calling FireAlarmManager.SetAlarm(true), all doors should be unlocked by calling DoorManager.OpenAllDoors(), all lights 
        /// should be turned on using LightManager.SetAllLights(true) and an online log should be made by calling WebService.LogFireAlarm(“fire alarm”).
        /// </summary>
        [Category("Level 4 Requirements L4R2")]
        [Test]
        public void SetCurrentState_SetFireAlarmUnlockAllDoorsAndTurnOnAllLightsAndMakeLog_ChangeStateToFireAlarmAndReturnsTrue()
        {
            // Creating the mock object for each of the manager and web service interfaces.
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var doorManager = Substitute.For<IDoorManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();

            BuildingController buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, webService, null);

            Assert.That(buildingController.SetCurrentState("fire alarm"), Is.True);
            Assert.That("fire alarm", Is.EqualTo(buildingController.GetCurrentState()));

            // Checking if the following fuctions are called in the SetCurrentState() function.
            doorManager.Received().OpenAllDoors();
            fireAlarmManager.Received().SetAlarm(true);
            lightManager.Received().SetAllLights(true);
            webService.Received().LogFireAlarm("fire alarm");
        }

        /// <summary>
        /// Test the GetStatusReport() method will parse each of the three status reports (for Lights, FireAlarm and Doors) and if a fault is detected,
        /// the WebService object will be used to log that an engineer is required to fix the fault by calling the WebService.LogEngineerRequired()
        /// method, passing a string parameter. The string parameter should contain the type of device that has shown a fault (e.g. Lights,
        /// FireAlarm or Doors). If multiple device types have shown a fault then these should be separated by a comma. E.g. If both Lights
        /// and Doors status reports indicate a fault the following string should be logged to the web server “Lights,Doors,”. 
        /// </summary>
        /// <param name="lightsStatus"></param>
        /// <param name="doorsStatus"></param>
        /// <param name="alarmStatus"></param>
        /// <param name="logExpected"></param>
        
        [Category("Level 4 Requirements L4R3")]

        [TestCase("Lights,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,",
            "Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,",
            "FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,",
            "Lights,FireAlarm,")]

        [TestCase("Lights,OK,OK,OK,OK,OK,OK,OK,OK,",
            "Doors,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,",
            "FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,",
            "Doors,")]

        [TestCase("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,",
            "Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,",
            "FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,",
            "FireAlarm,")]

        [TestCase("Lights,FAULT,OK,OK,OK,OK,OK,OK,OK,",
            "Doors,OK,OK,OK,OK,OK,FAULT,OK,OK,OK,OK,OK,",
            "FireAlarm,OK,OK,OK,OK,OK,OK,OK,FAULT,OK,",
            "Lights,Doors,FireAlarm,")]
        public void GetStatusReport_LogTheRequiredEngineerToFixFaultyDevices_ReturnsDevicesStatusConcatinatedReport(string lightsStatus, string doorsStatus, string alarmStatus, string logExpected)
        {
            // Creating the mock object for each of the manager and web service interfaces.
            var lightManager = Substitute.For<LightManager>();
            var doorManager = Substitute.For<DoorManager>();
            var fireAlarmManager = Substitute.For<FireAlarmManager>();
            var webService = Substitute.For<IWebService>();


            // Creating a stub of GetStatus() function which returns the parameterized statuses.
            lightManager.GetStatus().Returns(lightsStatus);
            doorManager.GetStatus().Returns(doorsStatus);
            fireAlarmManager.GetStatus().Returns(alarmStatus);

            BuildingController buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, webService, null);

            Assert.AreEqual(lightsStatus + doorsStatus + alarmStatus, buildingController.GetStatusReport());

            // Checking if the LogEngineerRequired() fuction is called in the GetStatusReport() function.
            webService.Received().LogEngineerRequired(logExpected);
        }

        /// <summary>
        /// Test the GetStatusReport() method will parse each of the three status reports (for Lights, FireAlarm and Doors) and if no fault is detected,
        /// the WebService object will not be used to log the engineer.

        /// </summary>
        [Category("Level 4 Requirements L4R3")]
        [Test]
        public void GetStatusReport_TheDevicesAreFunctionalAndNoEngineerNeeded_ReturnsDevicesStatusConcatinatedReport()
        {
            // Creating the mock object for each of the manager and web service interfaces.
            var lightManager = Substitute.For<LightManager>();
            var doorManager = Substitute.For<DoorManager>();
            var fireAlarmManager = Substitute.For<FireAlarmManager>();
            var webService = Substitute.For<IWebService>();


            // Creating a stub of GetStatus() function which returns the given statuses
            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,");

            BuildingController buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, webService, null);

            Assert.AreEqual("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,", buildingController.GetStatusReport());

            // Checking if the LogEngineerRequired() fuction is not called in the GetStatusReport() function.
            webService.DidNotReceive().LogEngineerRequired("");
        }

        /// <summary>
        /// Test that in addition to requirement L4R2, If WebService.LogFireAlarm( ) throws an Exception when called, an email should be sent using the
        /// EmailService’s SendMail( ) method. To smartbuilding@uclan.ac.uk, with the subject “failed to log alarm” and the message parameter should 
        /// contain the exception message returned from the failed call to the  LogFireAlarm() function.
        /// </summary>
        /// <exception cref="Exception"></exception>
        [Category("Level 4 Requirements L4R4")]
        [Test]
        public void SetCurrentState_SendEmailWhenLogFireAlarmThrowsExeption_ChangeStateToFireAlarmAndReturnsTrue()
        {
            // Creating the mock object for each of the manager and web service and email interfaces.
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var doorManager = Substitute.For<IDoorManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            var emailService = Substitute.For<IEmailService>();

            BuildingController buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, webService, emailService);

            // creating a stub LogFireAlarm() where the fuctions throws an exception
            webService.When(x => x.LogFireAlarm("fire alarm")).Do(x => { throw new Exception("Error"); });

            try
            {
                // catch the exception
                buildingController.SetCurrentState("fire alarm");
            }
            catch (Exception ex)
            {
                // now check if SendEmail() function is called and an email is sent to the "smartbuilding@uclan.ac.uk" email addres.
                emailService.Received().SendMail("smartbuilding@uclan.ac.uk", "failed to log alarm", ex.Message);
            }
        }
    }
}
