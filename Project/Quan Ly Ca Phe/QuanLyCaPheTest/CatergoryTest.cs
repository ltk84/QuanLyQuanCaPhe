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
    public class CatergoryTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(null, false)]
        [TestCase("Đồ ăn", true)]
        public void Test_AddCatergory(string name, bool expectedResult)
        {
            DateTime day = new DateTime(2008, 5, 1, 8, 30, 52);
            bool result = CatergoryDAO.Instance.Test_InsertCatergory((DateTime.Now - day).Seconds, name);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(1, null, false)]
        [TestCase(1, "Đồ ăn", true)]
        [TestCase(null, "Đồ ăn", false)]
        public void Test_EditCategory(int id, string name, bool expectedResult)
        {
            CatergoryDAO.CatergoryList.Add(new Catergory("Đồ ăn", 1));
            CatergoryDAO.CatergoryList.Add(new Catergory("Đồ uống", 2));

            Catergory cate = new Catergory(name, id);
            bool result = CatergoryDAO.Instance.Test_EditCategory(id, name);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(null, false)]
        public void Test_DeleteCatergory(int id, bool expectedResult)
        {
            CatergoryDAO.CatergoryList.Clear();
            CatergoryDAO.CatergoryList.Add(new Catergory("Đồ ăn", 1));
            CatergoryDAO.CatergoryList.Add(new Catergory("Đồ uống", 2));
            bool result = CatergoryDAO.Instance.Test_DeleteCatergory(id);

            Assert.AreEqual(expectedResult, result);
        }
    }
}

