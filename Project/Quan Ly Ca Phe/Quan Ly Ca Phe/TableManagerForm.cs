using Quan_Ly_Ca_Phe.DAO;
using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Ca_Phe
{
    public partial class TableManagerForm : Form
    {
        private Account loginAccount;
        public float totalPrice  = 0;

        public Account LoginAccount { get => loginAccount; set { loginAccount = value; ChangeAccount(loginAccount.Type); } }

        public TableManagerForm(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            LoadTableList();

            LoadComboBoxSwitchTable();
        }

        #region Method

        private void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 0;
            thôngTinTàiKhoảnToolStripMenuItem.Text += String.Format(" ({0})", loginAccount.UserName);
        }

        private void LoadTableList()
        {
            flpTable.Controls.Clear();

            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (var item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.Weight, Height = TableDAO.Height };

                btn.Text = item.Name + Environment.NewLine;

                switch (item.Status)
                {
                    case 0:
                        btn.Text = btn.Text;
                        btn.BackColor = ColorTranslator.FromHtml("#6BFF5F");
                        btn.Image = Properties.Resources.enable;
                        btn.ImageAlign = ContentAlignment.BottomCenter;
                        break;
                    default:
                        btn.Text = btn.Text;
                        btn.BackColor = ColorTranslator.FromHtml("#807B7B");
                        btn.Image = Properties.Resources.disnable;
                        btn.ImageAlign = ContentAlignment.BottomCenter;
                        break;
                }

                btn.Click += Btn_Click;
                btn.Tag = item;

                flpTable.Controls.Add(btn);
            }
        }

        private void ShowBill(int idTable)
        {
            lvBill.Items.Clear();

            List<DTO.Menu> menus = MenuDAO.Instance.GetBillInfoByTableID(idTable);

            float totalPrice = 0;
            foreach (DTO.Menu item in menus)
            {
                ListViewItem lvItem = new ListViewItem(item.FoodName);
                lvItem.SubItems.Add(item.Count.ToString());
                lvItem.SubItems.Add(item.Price.ToString());
                lvItem.SubItems.Add(item.TotalPrice.ToString());

                totalPrice += item.TotalPrice;
                lvBill.Items.Add(lvItem);
            }

            CultureInfo culture = new CultureInfo("vi-VN");
            labelTotal.Text = totalPrice.ToString("c", culture);
            Table t = lvBill.Tag as Table;
            labelNameTable.Text = t.Name;
        }

        private void LoadComboBoxSwitchTable()
        {
            cbSwitchTable.DataSource = TableDAO.Instance.LoadTableList();
            cbSwitchTable.DisplayMember = "Name";
        }


        #endregion

        #region Event

        private void Btn_Click(object sender, EventArgs e)
        {
            int idTable = (int)((sender as Button).Tag as Table).ID;
            lvBill.Tag = (sender as Button).Tag;
            ShowBill(idTable);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountProfileForm apf = new AccountProfileForm(loginAccount);
            apf.AfterUpdateAccount += Apf_AfterUpdateAccount;
            apf.ShowDialog();
        }

        private void Apf_AfterUpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.UserName + ")";
        }

       
        private void Af_UpdateTable(object sender, EventArgs e)
        {
            LoadTableList();
        }

        private void Af_DeleteTable(object sender, EventArgs e)
        {
            LoadTableList();
        }

        private void Af_InsertTable(object sender, EventArgs e)
        {
            LoadTableList();
        }

        private void btnAddDish_Click(object sender, EventArgs e)
        {
            Table t = lvBill.Tag as Table;
            if (t == null)
                return;
            int idTable = t.ID;
            int idBill = BillDAO.Instance.GetBillIDByIDTable(idTable);
            String nameTable = t.Name;
            AddDish ad = new AddDish(id: idTable, name: nameTable);
            ad.ShowDialog();
            int idFood = ad.idFood;
            int count = ad.count;
            if (idBill == -1 && count != 0)
            {
                BillDAO.Instance.InsertBill(idTable);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxBillID(), idFood, count);
                TableDAO.Instance.UpdateStatus(1,idTable);
            }
            else if(count != 0)
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
                TableDAO.Instance.UpdateStatus(1, idTable);
            }
            ShowBill(idTable);
            LoadTableList();
            CultureInfo culture = new CultureInfo("vi-VN");
            totalPrice += ad.totalPrice;
            labelTotal.Text = this.totalPrice.ToString("c", culture);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            ad.Close();
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table;
            if (table == null || table.Status == 0)
                return;
            int idBill = BillDAO.Instance.GetBillIDByIDTable(table.ID);
            double totalPrice = Decimal.ToDouble(Convert.ToDecimal(labelTotal.Text.Split(',')[0])) * 1000;
            CheckOut ck = new CheckOut(id: idBill, total: totalPrice, tbID: table.ID);
            ck.ShowDialog();
            if (ck.isCheckOut == true)
            {
                lvBill.Items.Clear();
                this.labelTotal.Text = "0.00";
                TableDAO.Instance.UpdateStatus(0, table.ID);
            }
            ck.Close();
            LoadTableList();
        }
        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int idOld, idNew;

            idOld = (lvBill.Tag as Table).ID;

            Table t = cbSwitchTable.SelectedItem as Table;
            if (t == null)
                return;

            idNew = Convert.ToInt32(t.ID);

            TableDAO.Instance.SwitchTable(idOld, idNew);

            LoadTableList();
        }


        #endregion


        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.loginAccount = loginAccount;
            af.ShowDialog();
            LoadTableList();
        }

        private void cbSwitchTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
