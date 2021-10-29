using Quan_Ly_Ca_Phe.DAO;
using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Ca_Phe
{
    public partial class AccountProfileForm : Form
    {
        private Account loginAccount;
        public Account LoginAccount { get => loginAccount; set { loginAccount = value; ChangeAccount(loginAccount); } }
        public AccountProfileForm(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeAccount(Account acc)
        {
            this.txtSignIn.Text = acc.UserName;
        }

        private void UpdateAccount()
        {
            string userName = txtSignIn.Text;
            string passWord = txtPassword.Text;
            string newPass = txtNewPass.Text;
            string rePass = txtRePass.Text;

            if (newPass != rePass)
            {
                MessageBox.Show("Vui lòng nhập lại đúng mật khẩu!", "Thông báo");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(userName, passWord, newPass))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    if (afterUpdateAccount != null)
                        afterUpdateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                }
                else
                    MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo");
            }    
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }

        private event EventHandler<AccountEvent> afterUpdateAccount;
        public event EventHandler<AccountEvent> AfterUpdateAccount
        {
            add { afterUpdateAccount += value; }
            remove { afterUpdateAccount -= value; }
        }
    }

    public class AccountEvent:EventArgs
    {
        private Account acc;

        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        public AccountEvent(Account acc)
        {
            this.acc = acc;
        }
    }
}
