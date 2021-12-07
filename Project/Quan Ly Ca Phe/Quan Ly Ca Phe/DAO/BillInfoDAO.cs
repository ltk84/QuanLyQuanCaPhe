using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;
        private List<Table> tables { get; set; }
        private List<Bill> bills { get; set; }
        private List<Food> foods { get; set; } 
        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set { instance = value; }
        }

        private BillInfoDAO() {
            tables = new List<Table>();
            foods = new List<Food>();
            bills = new List<Bill>();
            tables.Add(new Quan_Ly_Ca_Phe.DTO.Table(1,1,"Bàn 1"));
            tables.Add(new Quan_Ly_Ca_Phe.DTO.Table(1,0, "Bàn 2"));
            foods.Add(new Quan_Ly_Ca_Phe.DTO.Food(1,"Cơm sườn",0,30000));
            foods.Add(new Quan_Ly_Ca_Phe.DTO.Food(2,"Cà phê",1, 15000));
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            string query = "EXEC dbo.USP_InsertBillInfo @idBill , @idFood , @count";

            DataProvider.Instance.ExecuteQuery(query, new object[] { idBill, idFood, count});
        }

        public bool DeleteBillInfoByFoodID(int idFood)
        {
            string query = "DELETE FROM dbo.BILL_INFO WHERE FOOD_ID = " + idFood;
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool Test_AddOrder(int idTable, int idFood, int count)
        { 
            if (idTable == null|| idFood == null || count <= 0) return false;
            for (int i = 1; i <= tables.Count; i++)
            {
                if(idTable == tables[i].ID)
                {
                }
            }
            return true;
        }
    }
}
