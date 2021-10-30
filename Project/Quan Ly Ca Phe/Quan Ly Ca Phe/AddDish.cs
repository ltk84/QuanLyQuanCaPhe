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
        public ListViewItem lvItem = new ListViewItem();
        public int idFood;
        public String nameTable;
        public int count = 0;
        public float totalPrice = 0;
        public int tableId;
        public Account LoginAccount { get => loginAccount; set { loginAccount = value;  } }
        private void ShowBill(int idTable)
        {
            
            List<DTO.Menu> menus = MenuDAO.Instance.GetBillInfoByTableID(idTable);
            foreach (DTO.Menu item in menus)
            {
                lvItem = new ListViewItem(item.FoodName);
                lvItem.SubItems.Add(item.Count.ToString());
                lvItem.SubItems.Add(item.Price.ToString());
                lvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
            }
        }
       
        public AddDish(int id, String name)
        {

            InitializeComponent();
            LoadCatergory();
            tableId = id;            
            this.label2.Text = name;
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
            
        }
        private void Af_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodByCateID((cbCater.SelectedItem as Catergory).IdCate);
        }

        private void Af_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodByCateID((cbCater.SelectedItem as Catergory).IdCate);
            LoadTableList();
        }

        private void Af_InsertFood(object sender, EventArgs e)
        {
            LoadFoodByCateID((cbCater.SelectedItem as Catergory).IdCate);
           
        }

        private void cbFood_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbCater_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Catergory cater = cb.SelectedItem as Catergory;

            int idCate = cater.IdCate;
            if (idCate > 0)
                LoadFoodByCateID(idCate);
        }

        private void vbButton21_Click(object sender, EventArgs e)
        {
            idFood = (cbFood.SelectedItem as Food).ID;
            count = (int)nudAddFood.Value;
            this.Hide();
        }
    }
}
