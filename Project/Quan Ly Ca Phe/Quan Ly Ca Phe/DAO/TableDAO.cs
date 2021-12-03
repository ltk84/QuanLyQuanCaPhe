using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance { get { if (instance == null) instance = new TableDAO(); return instance; } private set => instance = value; }
        private static List<Table> tableList;
        public static List<Table> TableList { get => tableList; set => tableList = value; }


        private TableDAO() {
            TableList = new List<Table>();
        }

        public static int Height = 80;
        public static int Weight = 80;

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            string query = "EXEC dbo.USP_GetTableList";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }

        public DataTable GetTableList()
        {
            string query = "SELECT TABLES_ID AS 'ID', name AS 'Tên bàn', TABLES_STATUS AS 'Trạng thái' FROM dbo.TABLES";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            return data;
        }

        public void UpdateStatus(int status, int idTable)
        {
            string query  = String.Format("UPDATE dbo.TABLES SET TABLES_STATUS = {0} WHERE TABLES_ID = {1} ", status, idTable); 
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
        }

        public void SwitchTable(int idOld, int idNew)
        {
            string query = "EXEC USP_SwitchTable @idOld , @idNew";
            DataProvider.Instance.ExecuteQuery(query, new object[] { idOld, idNew});
        }

        public bool InsertTable(int state, string name)
        {
            string query = String.Format("INSERT dbo.TABLES(TABLES_STATUS,name) VALUES({0}, N'{1}')", state, name);
            int res = DataProvider.Instance.ExecuteNonQuery(query);

            return res > 0;
        }

        public bool DeleteTable(int id)
        {
            string query = String.Format("exec USP_DeleteTableByID @idTable = {0}", id);
            int res = DataProvider.Instance.ExecuteNonQuery(query);

            return res > 0;
        }

        public bool UpdateTable(int state, string name, int id)
        {
            string query = String.Format("UPDATE dbo.TABLES SET name = N'{0}', TABLES_STATUS = {1} WHERE TABLES_ID = {2}", name, state, id);
            int res = DataProvider.Instance.ExecuteNonQuery(query);

            return res > 0;
        }

        public bool Test_InsertTable(int id, int state, string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && (state == 0 || state == 1))
            {
                Table newTable = new Table(id,state,name);
                TableList.Add(newTable);
                return true;
            }
            return false;
        }
        public bool Test_EditTable(int state, string name, int? id)
        {
            if (id == null)
                return false;
            if (id >= 0 && !string.IsNullOrWhiteSpace(name) && (state == 0 || state == 1))
            {
                Table existTable = TableList.Where(x => x.ID == id).FirstOrDefault();
                if (existTable == null) return false;
                existTable.Name = name;
                existTable.Status = state;
                return true;
            }
            return false;
        }
        public bool Test_DeleteTable(int id)
        {

            if (id >= 0)
            {
                Table choosenTable = TableList.Where(x => x.ID == id).FirstOrDefault();
                if (choosenTable == null) return false;
                TableList.Remove(choosenTable);
                return true;
            }
            return false;
        }
    }
}
