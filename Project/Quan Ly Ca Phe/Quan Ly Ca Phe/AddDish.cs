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
    public partial class AddDish : Form
    {
        private Account loginAccount;

        public Account LoginAccount { get => loginAccount; set { loginAccount = value;  } }
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
            //txtTotalPrice.Text = totalPrice.ToString("c", culture);
        }
       
        public AddDish()
        {
            InitializeComponent();
            LoadCatergory();
        }
        private void LoadCatergory()
        {
            List<Catergory> list = CatergoryDAO.Instance.LoadCatergory();

            cbCater.DataSource = list;

            cbCater.DisplayMember = "nameFood";
        }

        private void LoadFoodByCateID(int idCate)
        {
            List<Food> foodList = FoodDAO.Instance.GetFoodListByCateID(idCate);

            cbFood.DataSource = foodList;

            cbFood.DisplayMember = "name";
        }
        private void Af_UpdateCatergory(object sender, EventArgs e)
        {
            LoadCatergory();
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        private void Af_DeleteCatergory(object sender, EventArgs e)
        {
            LoadCatergory();
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        private void Af_InsertCatergory(object sender, EventArgs e)
        {
            LoadCatergory();
        }
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            af.loginAccount = loginAccount;

            af.InsertFood += Af_InsertFood;
            af.DeleteFood += Af_DeleteFood;
            af.UpdateFood += Af_UpdateFood;

            af.InsertTable += Af_InsertTable;
            af.DeleteTable += Af_DeleteTable;
            af.UpdateTable += Af_UpdateTable;

            af.InsertCatergory += Af_InsertCatergory;
            af.DeleteCatergory += Af_DeleteCatergory;
            af.UpdateCatergory += Af_UpdateCatergory;

            af.ShowDialog();
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
        private void cbCater_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Catergory cater = cb.SelectedItem as Catergory;

            int idCate = cater.IdCate;
            if (idCate > 0)
                LoadFoodByCateID(idCate);
        }
        private void LoadTableList()
        {
            //flpTable.Controls.Clear();

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

                //flpTable.Controls.Add(btn);
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            int idTable = (int)((sender as Button).Tag as Table).ID;
            lvBill.Tag = (sender as Button).Tag;
            ShowBill(idTable);
        }
        private void Af_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodByCateID((cbCater.SelectedItem as Catergory).IdCate);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        private void Af_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodByCateID((cbCater.SelectedItem as Catergory).IdCate);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            LoadTableList();
        }

        private void Af_InsertFood(object sender, EventArgs e)
        {
            LoadFoodByCateID((cbCater.SelectedItem as Catergory).IdCate);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }
    }
}
