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
        
        [Category("Level 1 Requirements L1R1")]
        [TestCase("123")]
        [TestCase("")]
        [TestCase("build")]
        [TestCase("build123")]
        public void ConstructorWith1Parameter_SetTheParameterValueToBuildingID(string id)
        {
            BuildingController buildingController = new BuildingController(id);

            string result = buildingController.GetBuildingID();

            Assert.That(buildingController, Is.TypeOf<BuildingController>());
            Assert.AreEqual(id, result);
        }

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

        [Category("Level 1 Requirements L1R5")]
        [Test]
        public void ConstructorWith1Parameter_SetDefaultValueOfCurrentStateToOutOfHours()
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.AreEqual("out of hours", buildingController.GetCurrentState());
        }

        [Category("Level 1 Requirements L1R6")]
        [Test]
        public void GetCurrentState_GetDefaultState_ReturnsOutOfOrder()
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.AreEqual("out of hours", buildingController.GetCurrentState());
        }

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

        [Category("Level 2 Requirements L2R1")]
        [TestCase("fire larm")]
        [TestCase("open1")]
        public void SetCurrentState_DoNotSetTheCurrentStateToParameterStateIfItIsInvalid_ReturnsFalse(string state)
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.That(buildingController.SetCurrentState(state), Is.Not.True);
        }

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

        [Category("Level 3 Requirements L3R2")]
        [TestCase("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,")]
        [TestCase("Lights,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,")]
        public void GetStatus_LightsStatus_ReturnsStatusOfAllLightsInACommaSeperatedString(string status)
        {
            var lightManager = Substitute.For<LightManager>();

            lightManager.GetStatus().Returns(status);

            Assert.AreEqual(status, lightManager.GetStatus());
        }

        [Category("Level 3 Requirements L3R2")]
        [TestCase("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,")]
        [TestCase("Doors,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,")]
        public void GetStatus_DoorsStatus_ReturnsStatusOfAllDoorsInACommaSeperatedString(string status)
        {
            var doorManager = Substitute.For<DoorManager>();

            doorManager.GetStatus().Returns(status);

            Assert.AreEqual(status, doorManager.GetStatus());
        }

        [Category("Level 3 Requirements L3R2")]
        [TestCase("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,")]
        [TestCase("FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,")]
        public void GetStatus_FireAlarmStatus_ReturnsStatusOfAllFireAlarmInACommaSeperatedString(string status)
        {
            var fireAlarmManager = Substitute.For<FireAlarmManager>();

            fireAlarmManager.GetStatus().Returns(status);

            Assert.AreEqual(status, fireAlarmManager.GetStatus());
        }

        [Category("Level 3 Requirements L3R3")]
        [Test]
        public void GetStatusReport_ConcatinateAllManagerStatus_ReturnsDevicesStatusConcatinatedReport()
        {
            var lightManager = Substitute.For<LightManager>();
            var doorManager = Substitute.For<DoorManager>();
            var fireAlarmManager = Substitute.For<FireAlarmManager>();

            lightManager.GetStatus().Returns("Lights,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,");

            var buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, null, null);

            Assert.AreEqual("Lights,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,FireAlarm,OK,OK,FAULT,OK,OK,OK,OK,FAULT,OK,OK,", buildingController.GetStatusReport());
        }

        [Category("Level 3 Requirements L3R4")]
        [Test]
        public void SetCurrentState_WhenOpeningAllDoorsIsSuccessfull_ChangeStateToOpenAndReturnsTrue()
        {
            var doorManager = Substitute.For<IDoorManager>();

            doorManager.OpenAllDoors().Returns(true);
            BuildingController buildingController = new BuildingController("build1", null, null, doorManager, null, null);

            Assert.That(buildingController.SetCurrentState("open"), Is.True);
            Assert.That("open", Is.EqualTo(buildingController.GetCurrentState()));
        }

        [Category("Level 3 Requirements L3R5")]
        [Test]
        public void SetCurrentState_WhenOpeningAllDoorsIsUnsuccessfull_ChangesNothingAndReturnsFalse()
        {
            var doorManager = Substitute.For<IDoorManager>();

            doorManager.OpenAllDoors().Returns(false);
            BuildingController buildingController = new BuildingController("build1", null, null, doorManager, null, null);

            Assert.That(buildingController.SetCurrentState("open"), Is.Not.True);
            Assert.That("out of hours", Is.EqualTo(buildingController.GetCurrentState()));
        }

        [Category("Level 4 Requirements L4R1")]
        [Test]
        public void SetCurrentState_ClosingAllDoorsAndTurningOffAllLights_ChangeStateToClosedAndReturnsTrue()
        {
            var doorManager = Substitute.For<IDoorManager>();
            var lightManager = Substitute.For<ILightManager>();
            BuildingController buildingController = new BuildingController("build1", lightManager, null, doorManager, null, null);

            doorManager.LockAllDoors().Returns(true);

            Assert.That(buildingController.SetCurrentState("closed"), Is.True);

            lightManager.Received().SetAllLights(false);

            Assert.That("closed", Is.EqualTo(buildingController.GetCurrentState()));
        }

        [Category("Level 4 Requirements L4R1")]
        [Test]
        public void SetCurrentState_ClosingAllDoorsAndTurningOffAllLightsButDoorsFail_ChangesNothingAndReturnsFalse()
        {
            var doorManager = Substitute.For<IDoorManager>();
            var lightManager = Substitute.For<ILightManager>();

            doorManager.LockAllDoors().Returns(false);

            BuildingController buildingController = new BuildingController("build1", lightManager, null, doorManager, null, null);

            Assert.That(buildingController.SetCurrentState("closed"), Is.Not.True);

            lightManager.DidNotReceive().SetAllLights(false);

            Assert.That("out of hours", Is.EqualTo(buildingController.GetCurrentState()));
        }

        [Category("Level 4 Requirements L4R2")]
        [Test]
        public void SetCurrentState_SetFireAlarmUnlockAllDoorsAndTurnOnAllLightsAndMakeLog_ChangeStateToFireAlarmAndReturnsTrue()
        {
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var doorManager = Substitute.For<IDoorManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();

            BuildingController buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, webService, null);

            Assert.That(buildingController.SetCurrentState("fire alarm"), Is.True);
            Assert.That("fire alarm", Is.EqualTo(buildingController.GetCurrentState()));

            doorManager.Received().OpenAllDoors();
            fireAlarmManager.Received().SetAlarm(true);
            lightManager.Received().SetAllLights(true);
            webService.Received().LogFireAlarm("fire alarm");
        }

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
            var lightManager = Substitute.For<LightManager>();
            var doorManager = Substitute.For<DoorManager>();
            var fireAlarmManager = Substitute.For<FireAlarmManager>();
            var webService = Substitute.For<IWebService>();


            lightManager.GetStatus().Returns(lightsStatus);
            doorManager.GetStatus().Returns(doorsStatus);
            fireAlarmManager.GetStatus().Returns(alarmStatus);

            BuildingController buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, webService, null);

            Assert.AreEqual(lightsStatus + doorsStatus + alarmStatus, buildingController.GetStatusReport());

            webService.Received().LogEngineerRequired(logExpected);
        }

        [Category("Level 4 Requirements L4R3")]
        [Test]
        public void GetStatusReport_TheDevicesAreFunctionalAndNoEngineerNeeded_ReturnsDevicesStatusConcatinatedReport()
        {
            var lightManager = Substitute.For<LightManager>();
            var doorManager = Substitute.For<DoorManager>();
            var fireAlarmManager = Substitute.For<FireAlarmManager>();
            var webService = Substitute.For<IWebService>();


            lightManager.GetStatus().Returns("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            doorManager.GetStatus().Returns("Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,");
            fireAlarmManager.GetStatus().Returns("FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,");

            BuildingController buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, webService, null);

            Assert.AreEqual("Lights,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,Doors,OK,OK,OK,OK,OK,OK,OK,OK,OK,OK,FireAlarm,OK,OK,OK,OK,OK,OK,OK,OK,OK,", buildingController.GetStatusReport());

            webService.DidNotReceive().LogEngineerRequired("");
        }

        [Category("Level 4 Requirements L4R4")]
        [Test]
        public void SetCurrentState_SendEmailWhenLogFireAlarmThrowsExeption_ChangeStateToFireAlarmAndReturnsTrue()
        {
            var fireAlarmManager = Substitute.For<IFireAlarmManager>();
            var doorManager = Substitute.For<IDoorManager>();
            var lightManager = Substitute.For<ILightManager>();
            var webService = Substitute.For<IWebService>();
            var emailService = Substitute.For<IEmailService>();

            BuildingController buildingController = new BuildingController("build1", lightManager, fireAlarmManager, doorManager, webService, emailService);

            webService.When(x => x.LogFireAlarm("fire alarm")).Do(x => { throw new Exception("Error"); });

            try
            {
                buildingController.SetCurrentState("fire alarm");
            }
            catch(Exception ex)
            {
                emailService.Received().SendMail("smartbuilding@uclan.ac.uk", "failed to log alarm", ex.Message);
            }
        }
    }
}
