using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DTO
{
    public class BillInfo
    {
        private int id;
        private int billID;
        private int foodID;
        private int count;

        public BillInfo(int id, int billID, int foodID, int count)
        {
            this.Id = id;
            this.BillID = billID;
            this.FoodID = foodID;
            this.Count = count;
        }

        public int Id { get => id; set => id = value; }
        public int BillID { get => billID; set => billID = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public int Count { get => count; set => count = value; }

    }
}
