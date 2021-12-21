using NUnit.Framework;
using Quan_Ly_Ca_Phe.DAO;

namespace QuanLyCaPheTest
{
    public class AuthenticationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("admin", "admin", true)]
        [TestCase(null, "admin", false)]
        [TestCase("abcdef1234561234657", "abcd", true)]
        [TestCase("abc", null, false)]
        [TestCase("abcdef1234", "abcdef12345678912345", true)]
        [TestCase("a", "a", true)]
        [TestCase("abcdef1234abcdef1234", "abcdef1234abcdef1234", true)]
        public void Test_CheckValidateUserNamePassword(string inputUsername, string inputPassword, bool expectResult)
        {
            string username = inputUsername;
            string password = inputPassword;

            bool result = AccountDAO.Instance.CheckValidateUserNamePassword(username, password);

            Assert.AreEqual(expectResult, result);
        }

        [Test]
        [TestCase("admin", "admin", true)]
        [TestCase("admi1", "admi1", false)]
        public void Test_Login(string inputUsername, string inputPassword, bool expectResult)
        {
            string username = inputUsername;
            string password = inputPassword;

            bool result = AccountDAO.Instance.Test_Login(username, password);

            Assert.AreEqual(expectResult, result);
        }

        [Test]
        [TestCase(null, "admin", false)]
        [TestCase("admin", null, false)]
        [TestCase("admin", "01234567890123456789", true)]
        [TestCase("admin", "admin", true)]
        public void Test_ChangePassword(string inputUsername, string inputPassword, bool expectResult)
        {
            string username = inputUsername;
            string password = inputPassword;

            bool result = AccountDAO.Instance.Test_ChangePassword(username, password);

            Assert.AreEqual(expectResult, result);
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(-1, false)]
        public void Test_ResetPassword(int id, bool expectedResult)
        {
            var newPass = AccountDAO.Instance.Test_ResetPassword(id);

            bool result;
            if (newPass == null || newPass != "1") result = false;
            else result = true;

            Assert.AreEqual(expectedResult, result);
        }
    }
}