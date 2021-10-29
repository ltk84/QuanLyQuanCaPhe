using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DTO
{
    public class Bill
    {
        private int id;
        private int tableID;
        private int status;
        private DateTime checkIn;
        private DateTime checkOut;
        private int discount;

        public int Id { get => id; set => id = value; }
        public int TableID { get => tableID; set => tableID = value; }
        public int Status { get => status; set => status = value; }
        public DateTime CheckIn { get => checkIn; set => checkIn = value; }
        public DateTime CheckOut { get => checkOut; set => checkOut = value; }
        public int Discount { get => discount; set => discount = value; }

        public Bill(int id, int tableID, int status, DateTime checkIn, DateTime checkOut, int discount)
        {
            this.Id = id;
            this.TableID = tableID;
            this.Status = status;
            this.CheckIn = checkIn;
            this.CheckOut = checkOut;
            this.discount = discount;
        }

        public Bill(DataRow row)
        {
            this.Id = (int)row["bill_id"];
            this.TableID = (int)row["table_ID"];
            this.Status = (int)row["bill_status"];
            this.CheckIn = Convert.ToDateTime(row["datecheckIn"]);
            object dateTimeTempt = row["datecheckOut"];
            if (dateTimeTempt.ToString() != "")
                this.CheckOut = Convert.ToDateTime(row["datecheckOut"]);
            this.discount = (int)row["discount"];
        }
    }
}
