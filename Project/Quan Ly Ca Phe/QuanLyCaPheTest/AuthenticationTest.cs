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
        [TestCase("a", "a", true)]
        [TestCase("abcdef1234abcdef1234", "abcdef1234abcdef1234", true)]
        [TestCase("abcdef1234", "abcdef123456789", true)]
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
    }
}