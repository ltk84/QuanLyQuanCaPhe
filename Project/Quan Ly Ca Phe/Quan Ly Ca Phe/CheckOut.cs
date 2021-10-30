using Quan_Ly_Ca_Phe.DAO;
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
    public partial class CheckOut : Form
    {
        public int idBill;
        public double totalPrice;
        public int tableID;
        public int discount;
        double finalTotalPrice;
        public bool isCheckOut;
        public CheckOut(int id, double total, int tbID)
        {
            InitializeComponent();
            this.idBill = id;
            this.totalPrice = total;
            this.tableID = tbID;
            this.txtTotalPrice.Text = total.ToString();
            finalTotalPrice = total;
            this.labelCost.Text = total.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.isCheckOut = false;
            this.Hide();
        }

        private void vbButton21_Click(object sender, EventArgs e)
        {
            if((int)nudDiscount.Value != 0 && discount == 0)
            {
                MessageBox.Show("Tính tiền cho khách hàng với phần trăm đã giảm");
                return;
            }    
            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Thanh toán thành công", tableID.ToString(), totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillDAO.Instance.CheckOutBill(idBill, discount, (float)finalTotalPrice);
                    //lvBill.Items.Clear();
                }
            }
            this.isCheckOut = true;
            this.Hide();
        }

        private void vbButton21_Click_1(object sender, EventArgs e)
        {
            discount = (int)nudDiscount.Value;
            finalTotalPrice = totalPrice - totalPrice / 100 * discount;
            this.labelCost.Text = finalTotalPrice.ToString();
        }
    }
}
