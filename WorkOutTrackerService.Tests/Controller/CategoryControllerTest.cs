using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOutDBLayer;
using WorkOutDBModel.Model;

namespace WorkOutTrackerService.Tests.Controller
{
    [TestClass]
    public class CategoryControllerTest
    {
        [TestMethod()]
        public void GetAllCategories()
        {
            CateogryDb obj = new CateogryDb();
            int actualCount = obj.GetAll().Count;
            int expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod()]
        public void GetCategory_By_CategoryId()
        {
            CateogryDb obj = new CateogryDb();
            string actualCategoryName = obj.GetById(1).CategoryName;
            string expectedCategoryName = "Walk";
            Assert.AreEqual(expectedCategoryName, actualCategoryName);
        }

        [TestMethod()]
        public void AddCategory()
        {
            CateogryDb obj = new CateogryDb();
            bool result= obj.Add(
                new Category {  CategoryName="Running Test",CategoryId=0});
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void Delete_Category()
        {
            CateogryDb obj = new CateogryDb();
            bool result = obj.Delete(2);
            Assert.AreEqual(true, result);
        }

    }
}
