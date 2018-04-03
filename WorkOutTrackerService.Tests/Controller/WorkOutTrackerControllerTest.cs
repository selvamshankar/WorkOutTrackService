using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOutDBLayer;
using WorkOutDBModel.Model;
using System.Linq;
using System;

namespace WorkOutTrackerService.Tests.Controller
{
    public class WorkOutTrackerControllerTest
    {
        [TestMethod()]
        public void GetAllWorkoutTracker()
        {
            WorkOutDb obj = new WorkOutDb();
            int actualCount = obj.GetAll().Count;
            int expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod()]
        public void GetWorkouttracker_By_Id()
        {
            WorkOutDb obj = new WorkOutDb();
            string actualworkOutTitle = obj.GetById(1).WorkOutTitle;
            string expectedWorkOutTitle = "test Workout Tracker";
            Assert.AreEqual(actualworkOutTitle, expectedWorkOutTitle);
        }

        [TestMethod()]
        public void AddWorkOut_Tracker()
        {
            WorkOutDb obj = new WorkOutDb();
            bool result = obj.Add(
                new WorkOut { Created=DateTime.Now });
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void Delete_workoutTracker()
        {
            WorkOutDb obj = new WorkOutDb();
            bool result = obj.Delete(2);
            Assert.AreEqual(true, result);
        }
    }
}
