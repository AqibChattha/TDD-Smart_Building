using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace SmartBuilding
{
    [TestFixture]
    public class BuildingControllerTests
    {
        [SetUp]
        public void Init()
        { /* ... */ }

        [TearDown]
        public void Cleanup()
        { /* ... */ }


        /*
         * para,
         */
        [Test]
        public void Constructor_CreateObject_SetIdToBuild1()
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.AreEqual("build1", buildingController.GetBuildingID());
        }

        [Test]
        public void GetBuildingID_GetId_ReturnsBuild1()
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.AreEqual("build1", buildingController.GetBuildingID());
        }

        [Test]
        public void Constructor_LowerCaseTheId_ReturnsBuild1()
        {
            BuildingController buildingController = new BuildingController("BUILD1");

            Assert.AreEqual("build1", buildingController.GetBuildingID());
        }

        [Test]
        public void SetBuildingID_SetNewIdToLowerCase_ReturnsBuild1()
        {
            BuildingController buildingController = new BuildingController("build1");
            buildingController.SetBuildingID("NewBuild2");

            Assert.AreEqual("newbuild2", buildingController.GetBuildingID());
        }

        [Test]
        public void Constructor_CreateObject_SetStateToOutOfHoursByDefault()
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.AreEqual("out of hours", buildingController.GetCurrentState());
        }

        [Test]
        public void GetCurrentState_Get_State()
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.AreEqual("out of hours", buildingController.GetCurrentState());
        }

        [Test]
        public void SetCurrentState_CheckFromStatesAvailable_SetStateFromListOrDoNothing()
        {
            BuildingController buildingController = new BuildingController("build1");

            Assert.AreEqual("out of hours", buildingController.GetCurrentState());
            Assert.AreEqual(false, buildingController.SetCurrentState("open1"));
            Assert.AreEqual("out of hours", buildingController.GetCurrentState());
            Assert.AreEqual(true, buildingController.SetCurrentState("open"));
            Assert.AreEqual("open", buildingController.GetCurrentState());
            Assert.AreEqual(true, buildingController.SetCurrentState("closed"));
            Assert.AreEqual(true, buildingController.SetCurrentState("out of hours"));
            Assert.AreEqual(true, buildingController.SetCurrentState("fire drill"));
            Assert.AreEqual(true, buildingController.SetCurrentState("fire alarm"));


        }
    }
}
