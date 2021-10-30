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
                        btn.Text = btn.Text + "Trống";
                        btn.BackColor = Color.White;
                        break;
                    default:
                        btn.Text = btn.Text + "Đã có khách";
                        btn.BackColor = Color.Blue;
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
            txtTotalPrice.Text = totalPrice.ToString("c", culture);
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
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(idTable);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxBillID(), idFood, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }
            ShowBill(idTable);
            LoadTableList();
            CultureInfo culture = new CultureInfo("vi-VN");
            totalPrice += ad.totalPrice;
            txtTotalPrice.Text = this.totalPrice.ToString("c", culture);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            ad.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table;

            if (table == null)
                return;

            int idBill = BillDAO.Instance.GetBillIDByIDTable(table.ID);
            int discount = (int)nudDiscount.Value;

            double totalPrice = Decimal.ToDouble(Convert.ToDecimal(txtTotalPrice.Text.Split(',')[0])) * 1000;
            double finalTotalPrice = totalPrice - totalPrice / 100 * discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Thanh toán hóa đơn bàn {0}\n Tổng tiền - (Tổng tiền / 100) x Giảm giá \n => {1} - ({1} / 100) x {2} = {3}", table.ID.ToString(), totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillDAO.Instance.CheckOutBill(idBill, discount, (float)finalTotalPrice);
                    lvBill.Items.Clear();
                }
            }

            LoadTableList();
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void lvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.loginAccount = loginAccount;


            af.ShowDialog();
        }
    }
}
