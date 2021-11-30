using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DTO
{
    public class Account
    {
        private int id;
        private string userName;
        private string password;
        private int type;

        public Account(string userName, int type, string password = null)
        {
            this.UserName = userName;
            this.Password = password;
            this.Type = type;
        }

        public Account(int id, string username, string password, int type)
        {
            this.id = id;
            this.userName = username;
            this.password = password;
            this.type = type;
        }

        public int ID { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public int Type { get => type; set => type = value; }

        public Account(DataRow row)
        {
            this.UserName = row["userName"].ToString();
            this.Password = row["pass"].ToString();
            this.Type = (int)row["acc_type"];
        }
    }
}
