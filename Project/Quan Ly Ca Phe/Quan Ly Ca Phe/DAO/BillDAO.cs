using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            private set { instance = value; }
        }

        private BillDAO() { }

        public void InsertBill(int idTable)
        {
            string query = "EXEC dbo.USP_InsertBill @id_table = " + idTable;

            DataProvider.Instance.ExecuteQuery(query);
        }

        public int GetBillIDByIDTable(int idTable)
        {
            string query = "SELECT * FROM BILL WHERE TABLE_ID = " + idTable.ToString() + " AND Bill_status = 0";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                Bill b = new Bill(data.Rows[0]);
                return b.Id;
            }
            return -1;
        }

        public int GetMaxBillID()
        {
            string query = "SELECT MAX(BILL_ID) FROM BILL";

            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public void CheckOutBill(int idBill, int discount, float totalPrice)
        {
            string query = "UPDATE dbo.BILL SET BILL_STATUS = 1, DATECHECKOUT = GETDATE(), discount = "+ discount + ", totalPrice = " + totalPrice + "WHERE BILL_ID = " + idBill;

            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public DataTable GetBillListByDate(DateTime DateIn, DateTime DateOut)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_GetBillListByDate @DateIn , @DateOut", new object[] { DateIn, DateOut});
        }

        public DataTable GetBillListByDateAndPage(DateTime DateIn, DateTime DateOut, int page)
        {
            return DataProvider.Instance.ExecuteQuery("EXEC USP_GetBillListByDateAndPage @DateIn , @DateOut , @page", new object[] { DateIn, DateOut, page });
        }

        public int GetTheTotalBillRow(DateTime DateIn, DateTime DateOut)
        {
            return (int)DataProvider.Instance.ExecuteScalar("SELECT COUNT(*) FROM dbo.BILL WHERE  DATECHECKIN >= '" + DateIn +"' AND DATECHECKOUT <= '" + DateOut +"' ");
        }
    }
}
 