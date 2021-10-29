using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DTO
{
    public class Table
    {
        private int iD;
        private int status;
        private string name;

        public Table(int iD, int status, string name)
        {
            ID = iD;
            Status = status;
            Name = name;
        }

        public Table(DataRow dataRow)
        {
            ID = (int)dataRow["tables_id"];
            Status = (int)dataRow["tables_status"];
            Name = dataRow["Name"].ToString();
        }

        public int ID { get => iD; set => iD = value; }
        public int Status { get => status; set => status = value; }
        public string Name { get => name; set => name = value; }
    }
}
