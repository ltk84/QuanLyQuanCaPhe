using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static Account UserAccount { get; set; }

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO()
        {
            UserAccount = new Account("admin", 0, "admin");
        }

        public bool Login(string userName, string passWord)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(passWord);
            byte[] hassData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hassPass = "";
            foreach (byte item in hassData)
            {
                hassPass += item;
            }

            string query = "EXEC USP_LOGIN @USERNAME , @PASS";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, hassPass });

            return result.Rows.Count > 0;
        }

        public bool Test_Login(string username, string password)
        {
            return username.Equals(UserAccount.UserName) && password.Equals(UserAccount.Password);
        }

        public bool CheckValidateUserNamePassword(string username, string password)
        {
            return !string.IsNullOrWhiteSpace(username) && username.Length <= 20 && !string.IsNullOrWhiteSpace(password) && password.Length <= 20;
        }

        public Account GetAccountByUserName(string username)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.ACCOUNT WHERE USERNAME = '" + username + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        public bool UpdateAccount(string userName, string password, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("exec USP_UpdateAccount @username , @passWord , @newPass", new object[] { userName, password, newPass });

            return result > 0;
        }

        public DataTable LoadAccountList()
        {
            string query = "select ACC_ID as 'ID', USERNAME as 'Tên đăng nhập', ACC_TYPE as 'Loại tài khoản' from ACCOUNT";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool InsertAccount(string userName, string password, int type)
        {
            string query = String.Format("INSERT dbo.ACCOUNT(USERNAME, pass, ACC_TYPE) VALUES(   N'{0}', N'{1}', {2} )", userName, password, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteAccount(string username)
        {
            string query = String.Format("DELETE FROM dbo.ACCOUNT WHERE username = '{0}'", username);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateAccount(string userName, int type)
        {
            string query = String.Format("UPDATE dbo.ACCOUNT SET ACC_TYPE  = {0} WHERE USERNAME = N'{1}'", type, userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool ResetPassword(string username)
        {
            string query = String.Format("UPDATE dbo.ACCOUNT SET pass = {0} WHERE USERNAME = N'{1}'", 1, username);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool Test_ChangePassword(string username, string newPassword)
        {
            if (CheckValidateUserNamePassword(username, newPassword))
            {
                UserAccount.Password = newPassword;
                return true;
            }
            return false;
        }

        public string Test_ResetPassword(int id)
        {
            List<Account> listAcc = new List<Account>()
            {
                new Account(1, "1", "abc", 0),
                new Account(2, "2", "abc", 0),
            };

            var acc = listAcc.Where(x => x.ID == id).FirstOrDefault();
            if (acc == null) return null;
            acc.Password = "1";

            return acc.Password;
        }
    }
}
