using NUnit.Framework;
using Quan_Ly_Ca_Phe.DAO;
using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCaPheTest
{
    public class FoodTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(null, 1, 30000, false)]
        [TestCase("Ga", -1, 30000, false)]
        [TestCase("Ga", 1, -1, false)]
        [TestCase("Ga", 1, 30000, true)]
        [TestCase("Ga", 1, 0, true)]
        public void Test_AddFood(string name, int cateID, float price, bool expectedResult)
        {
            DateTime day = new DateTime(2008, 5, 1, 8, 30, 52);
            bool result = FoodDAO.Instance.Test_InsertFood((DateTime.Now - day).Seconds, name, cateID, price);
            int foodListLength = FoodDAO.FoodList.Count();

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(1, null, 2, 30000, false)]
        [TestCase(1, "Ga ran", -1, 30000, false)]
        [TestCase(1, "", -1, 30000, false)]
        [TestCase(1, "Ga ran", 1, 0, true)]
        [TestCase(1, "Ga ran", 2, -1, false)]
        [TestCase(1, "Ga ran", 2, 30000, true)]
        [TestCase(-1, "Ga ran", 2, 30000, false)]
        public void Test_EditFood(int id, string name, int cateID, float price, bool expectedResult)
        {
            FoodDAO.FoodList.Add(new Food(id, "Ga", 1, 1000));

            Food food = new Food(id, name, cateID, price);
            bool result = FoodDAO.Instance.Test_EditFood(id, name, cateID, price);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
