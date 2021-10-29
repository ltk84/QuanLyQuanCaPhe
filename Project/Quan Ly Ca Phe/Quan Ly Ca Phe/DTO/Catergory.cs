using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DTO
{
    public class Catergory
    {
        private string nameFood;
        private int idCate;

        public Catergory(string nameFood, int idCate)
        {
            this.nameFood = nameFood;
            this.idCate = idCate;
        }

        public Catergory(DataRow row)
        {
            this.nameFood = row["Fcate_name"].ToString();
            this.idCate = (int)row["fcate_id"];
        }

        public string NameFood { get => nameFood; set => nameFood = value; }
        public int IdCate { get => idCate; set => idCate = value; }
    }
}
