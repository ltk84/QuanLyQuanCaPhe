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
    class AccountTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(null, "1", 0, false)]
        [TestCase("adminabcdefghiabcaasa", "1", 0, false)]
        [TestCase("1", null, 0, false)]
        [TestCase("1", "adminabcdefghiabcaasa", 0, false)]
        [TestCase("1", "1", null, false)]
        [TestCase("1", "1", 0, true)]

        public void Test_AddAccount(string userName, string password, int? type, bool expectedResult)
        {
            
            bool result = AccountDAO.Instance.Test_InsertAccount(userName, password, type);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(null, 1, false)]
        [TestCase("adminabcdefghiabcaasa", 1, false)]
        [TestCase("3", 3, false)]
        [TestCase("3", null, false)]
        [TestCase("3", 1, false)]
        [TestCase("2", 1, true)]

        public void Test_EditAccount(string username, int? type, bool expectedResult)
        {
            AccountDAO.Instance.resetListAccount();
            AccountDAO.Instance.AddToAccountList(new Account("2",0, "1"));


            bool result = AccountDAO.Instance.Test_EditAccount(username, type);

            Assert.AreEqual(expectedResult, result);
        }


        

        [Test]
        [TestCase(null, false)]
        [TestCase("sjojo", false)]
        [TestCase("1", true)]
        public void Test_DeleteAccount(string username, bool expectedResult)
        {
            AccountDAO.Instance.resetListAccount();
            AccountDAO.Instance.AddToAccountList(new Account("1", 0, "1"));
            AccountDAO.Instance.AddToAccountList(new Account("2", 0, "2"));


            bool result = AccountDAO.Instance.Test_DeleteAccount(username);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
