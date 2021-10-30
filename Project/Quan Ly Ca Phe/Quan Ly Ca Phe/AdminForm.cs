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
    public partial class AdminForm : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource catergoryList = new BindingSource();
        BindingSource accountList = new BindingSource();


        public Account loginAccount;


        public AdminForm()
        {
            InitializeComponent();
            Load();
        }

        #region Methods

        public void Load()
        {
            LoadDateTimePicker();
            GetBillListByDate(dtpFromDate.Value, dtpToDate.Value);
            ViewFoodList();
            LoadTableList();
            LoadCatergoryList();
            LoadAccountList();

            dgvFood.DataSource = foodList;
            dgvTable.DataSource = tableList;
            dgvCate.DataSource = catergoryList;
            dgvAcc.DataSource = accountList;

            BindingData();
        }

        public void BindingData()
        {
            BindingFoodData();
            BindingTableData();
            BindingCatergoryData();
            BindingAccountData();
        }

        public void LoadAccountList()
        {
            accountList.DataSource = AccountDAO.Instance.LoadAccountList();
        }

        public void LoadTableList()
        {
            tableList.DataSource = TableDAO.Instance.GetTableList();
        }

        public void LoadCatergoryList()
        {
            catergoryList.DataSource = CatergoryDAO.Instance.GetCatergory();
        }

        public void BindingFoodData()
        {
            txtIdFood.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameFood.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "Tên món", true, DataSourceUpdateMode.Never));
            nudFoodPrice.DataBindings.Add(new Binding("Value", dgvFood.DataSource, "Giá", true, DataSourceUpdateMode.Never));

            LoadCateComboBox(cbCateFood);
        }

        public void BindingAccountData()
        {
            txtIDAcc.DataBindings.Add(new Binding("Text", dgvAcc.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtUsernameAcc.DataBindings.Add(new Binding("Text", dgvAcc.DataSource, "Tên đăng nhập", true, DataSourceUpdateMode.Never));
            txtAccountType.DataBindings.Add(new Binding("Text", dgvAcc.DataSource, "Loại tài khoản", true, DataSourceUpdateMode.Never));
        }

        public void BindingTableData()
        {
            txtIDTable.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameTable.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "TÊN BÀN", true, DataSourceUpdateMode.Never));

            LoadTableStateComboBox();
        }

        public void BindingCatergoryData()
        {
            txtIDCate.DataBindings.Add(new Binding("Text", dgvCate.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtNameCate.DataBindings.Add(new Binding("Text", dgvCate.DataSource, "Tên thể loại", true, DataSourceUpdateMode.Never));
        }

        public void LoadCateComboBox(ComboBox cb)
        {
            cb.DataSource = CatergoryDAO.Instance.LoadCatergory();
            cb.DisplayMember = "namefood";
        }

        public void LoadTableStateComboBox()
        {
            cbStateTable.Items.Add("Trống");
            cbStateTable.Items.Add("Có người");
        }

        public void GetBillListByDate(DateTime dateIn, DateTime dateOut)
        {
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDate(dateIn, dateOut);
        }

        public void LoadDateTimePicker()
        {
            DateTime today = DateTime.Now;

            dtpFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1);
        }

        public void ViewFoodList()
        {
            foodList.DataSource = FoodDAO.Instance.GetFoodList();
        }

        #endregion

        #region Events
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dateIn = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
            DateTime dateOut = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
            txtPageNum.Text = "1";

            GetBillListByDate(dateIn, dateOut);
        }

        private void btnWatchFood_Click(object sender, EventArgs e)
        {
            ViewFoodList();
        }

        private void txtIdFood_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string nameCate;
                if (dgvFood.SelectedRows[0].Cells["Thể loại"].Value != null)
                {
                    nameCate = dgvFood.SelectedRows[0].Cells["Thể loại"].Value.ToString();

                    int index = -1;
                    foreach (Catergory item in cbCateFood.Items)
                    {
                        if (item.NameFood == nameCate)
                        {
                            index = cbCateFood.Items.IndexOf(item);
                            break;
                        }
                    }

                    cbCateFood.SelectedIndex = index;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtNameFood.Text;
            int cate = (cbCateFood.SelectedItem as Catergory).IdCate;
            float price = (float)nudFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, cate, price))
            {
                MessageBox.Show("Thêm món thành công", "Thông báo");
                ViewFoodList();
                if (insertFood != null)
                    insertFood(this, new EventArgs()); 
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm thức ăn", "Thông báo");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txtNameFood.Text;
            int cate = (cbCateFood.SelectedItem as Catergory).IdCate;
            float price = (float)nudFoodPrice.Value;
            int id = Convert.ToInt32(txtIdFood.Text);

            if (FoodDAO.Instance.EditFood(id, name, cate, price))
            {
                MessageBox.Show("Sửa món thành công", "Thông báo");
                ViewFoodList();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Lỗi khi sửa thức ăn", "Thông báo");
            }
        }

        private void btnDeleteFood_Click_1(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIdFood.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công", "Thông báo");
                ViewFoodList();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Lỗi khi xóa thức ăn", "Thông báo");
            }
        }

        private event  EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value;}
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private void txtIDTable_TextChanged(object sender, EventArgs e)
        {
            int state = (int)dgvTable.SelectedRows[0].Cells["Trạng thái"].Value;

            if (state == 1)
                cbStateTable.SelectedItem = cbStateTable.Items[1];
            else
                cbStateTable.SelectedItem = cbStateTable.Items[0];
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txtNameTable.Text;
            int state = -1;
            string stateWord = cbStateTable.Text;

            if (stateWord.Equals("Trống"))
                state = 0;
            else
                state = 1;

            if(TableDAO.Instance.InsertTable(state, name))
            {
                MessageBox.Show("Thêm thành công", "Thông báo");
                LoadTableList();
                if (insertTable != null)
                    insertTable(this, new EventArgs());
            }    
            else
            {
                MessageBox.Show("Có lỗi khi thêm bàn");
            }    
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txtNameTable.Text;
            int id = Convert.ToInt32(txtIDTable.Text);
            int state = -1;
            string stateWord = cbStateTable.Text;

            if (stateWord.Equals("Trống"))
                state = 0;
            else
                state = 1;

            if (TableDAO.Instance.UpdateTable(state, name, id))
            {
                MessageBox.Show("Sửa bàn thành công", "Thông báo");
                LoadTableList();
                if (updateTable != null)
                    updateTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa bàn", "Thông báo");
            }
        }

        private void btnDelTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDTable.Text);
            string name = txtNameTable.Text;
            int state = -1;
            string stateWord = cbStateTable.Text;

            if (stateWord.Equals("Trống"))
                state = 0;
            else
                state = 1;

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa bàn thành công", "Thông báo");
                LoadTableList();
                if (deleteTable != null)
                    deleteTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa bàn", "Thông báo");
            }
        }

        private void btnWatchTable_Click(object sender, EventArgs e)
        {
            LoadTableList();
        }

        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }

        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }

        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }

        private void btnAddCate_Click(object sender, EventArgs e)
        {
            string name = txtNameCate.Text;

            if (CatergoryDAO.Instance.InsertCatergory(name))
            {
                MessageBox.Show("Thêm thể loại thành công", "Thông báo");
                LoadCatergoryList();
                ViewFoodList();
                LoadCateComboBox(cbCateFood); 

                if (insertCatergory != null)
                    insertCatergory(this, new EventArgs());
            }    
            else
            {
                MessageBox.Show("Có lỗi khi thêm thể loại", "Thông báo");
            }    
        }

        private void btnEditCate_Click(object sender, EventArgs e)
        {
            string name = txtNameCate.Text;
            int id = Convert.ToInt32(txtIDCate.Text);

            if (CatergoryDAO.Instance.UpdateCatergory(name, id))
            {
                MessageBox.Show("Sửa thể loại thành công", "Thông báo");
                LoadCatergoryList();
                LoadCateComboBox(cbCateFood); 
                ViewFoodList();

                if (updateCatergory != null)
                    updateCatergory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thể loại", "Thông báo");
            }
        }

        private void btnDeleteCate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtIDCate.Text);

            if (CatergoryDAO.Instance.DeleteCatergory(id))
            {
                MessageBox.Show("Xóa thể loại thành công", "Thông báo");
                LoadCatergoryList();
                LoadCateComboBox(cbCateFood); 
                ViewFoodList();

                if (deleteCatergory != null)
                    deleteCatergory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thể loại", "Thông báo");
            }
        }

        private void btnWatchCate_Click(object sender, EventArgs e)
        {
            LoadCatergoryList();
        }

        private event EventHandler insertCatergory;
        public event EventHandler InsertCatergory
        {
            add { insertCatergory += value; }
            remove { insertCatergory -= value; }
        }

        private event EventHandler deleteCatergory;
        public event EventHandler DeleteCatergory
        {
            add { deleteCatergory += value; }
            remove { deleteCatergory -= value; }
        }

        private event EventHandler updateCatergory;
        public event EventHandler UpdateCatergory
        {
            add { updateCatergory += value; }
            remove { updateCatergory -= value; }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            string name = txtSearchFood.Text;

            foodList.DataSource = FoodDAO.Instance.FindFoodByName(name);

        }

        private void btnWatchAcc_Click(object sender, EventArgs e)
        {
            LoadAccountList();
        }

        private void btnDelAcc_Click(object sender, EventArgs e)
        {
            string userName = txtUsernameAcc.Text;

            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Vui lòng đừng xóa chính mình chứ!", "Thông báo");
                return;
            }    

            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công", "Thông báo");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Lỗi khi xóa tài khoản", "Thông báo");
            }
        }

        private void btnEditAcc_Click(object sender, EventArgs e)
        {
            string userName = txtUsernameAcc.Text;
            int type = Convert.ToInt32(txtAccountType.Text);

            if (AccountDAO.Instance.UpdateAccount(userName, type))
            {
                MessageBox.Show("Sửa tài khoản thành công", "Thông báo");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Lỗi khi sửa tài khoản", "Thông báo");
            }
        }

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            string userName = txtUsernameAcc.Text;
            int type = Convert.ToInt32(txtAccountType.Text);
            string pass = txtPassAcc.Text;

            if (AccountDAO.Instance.InsertAccount(userName, pass,  type))
            {
                MessageBox.Show("Thêm tài khoản thành công", "Thông báo");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm tài khoản", "Thông báo");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string userName = txtUsernameAcc.Text;

            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Reset password tài khoản thành công", "Thông báo");
                LoadAccountList();
            }
            else
            {
                MessageBox.Show("Lỗi khi Reset password tài khoản", "Thông báo");
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            txtPageNum.Text = "1";
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpFromDate.Value, dtpToDate.Value, Convert.ToInt32(txtPageNum.Text));
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPageNum.Text);
            if (page >= 2)
                page--;
            txtPageNum.Text = page.ToString();
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpFromDate.Value, dtpToDate.Value, page);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPageNum.Text);
            if (page < BillDAO.Instance.GetTheTotalBillRow(dtpFromDate.Value, dtpToDate.Value) / 5)
                page++;
            txtPageNum.Text = page.ToString();
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpFromDate.Value, dtpToDate.Value, page);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int totalRow = BillDAO.Instance.GetTheTotalBillRow(dtpFromDate.Value, dtpToDate.Value);
            int page = totalRow / 5;
            txtPageNum.Text = page.ToString();
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpFromDate.Value, dtpToDate.Value, Convert.ToInt32(txtPageNum.Text));
        }



        #endregion

        private void cbCateFood_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNameFood_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
