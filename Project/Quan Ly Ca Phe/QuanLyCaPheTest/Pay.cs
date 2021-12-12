using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Quan_Ly_Ca_Phe.DAO;
using Quan_Ly_Ca_Phe.DTO;

namespace QuanLyCaPheTest
{
    class Pay
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        [TestCase(1, 0, true)]
        [TestCase(null,0,false)]
        
        public void Test_CheckOut(int idBill, int discount, bool expectedResult)
        {
            bool result = BillDAO.Instance.Test_CheckOut(idBill,discount);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
