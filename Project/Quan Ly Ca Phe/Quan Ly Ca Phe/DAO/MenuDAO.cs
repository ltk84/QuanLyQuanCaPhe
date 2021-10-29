using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Ca_Phe.DTO;

namespace Quan_Ly_Ca_Phe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance { get { if (instance == null) instance = new MenuDAO(); return instance; } private set => instance = value; }

        private MenuDAO() { }

        public List<DTO.Menu> GetBillInfoByTableID(int idTable)
        {
            List<DTO.Menu> menu = new List<DTO.Menu>();

            string query = "SELECT f.FOOD_NAME, bi.count, f.PRICE, f.PRICE * bi.count as totalPrice FROM dbo.BILL b, dbo.BILL_INFO bi, dbo.FOOD f WHERE b.BILL_ID = bi.BILL_ID AND bi.FOOD_ID = f.FOOD_ID AND bill_status = 0 and b.TABLE_ID =" + idTable;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                DTO.Menu m = new DTO.Menu(item);
                menu.Add(m);
            }

            return menu;
        }
    }
}
