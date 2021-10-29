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

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set { instance = value; }
        }

        private BillInfoDAO() { }

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

    }
}
