using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class OrderTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        [TestCase(1, 2, 5, true)]
        [TestCase(null,2,5, false)]
        [TestCase(1, null, 5, false)]
        [TestCase(1, 2, -5, false)]
        [TestCase(1, 2, 0, false)]
        public void Test_AddOrder(int idTable, int idFood, int count, bool expectedResult)
        {
            bool result = BillInfoDAO.Instance.Test_AddOrder(idTable,idFood,count);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
