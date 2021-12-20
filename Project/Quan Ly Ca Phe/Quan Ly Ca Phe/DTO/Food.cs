using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DTO
{
    public class Food
    {
        private int iD;
        private string name;
        private int catergoryID;
        float price;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public int CatergoryID { get => catergoryID; set => catergoryID = value; }
        public float Price { get => price; set => price = value; }

        public Food(int id, string name, int catergoryID, float price)
        {
            this.iD = id;
            this.Name = name;
            this.CatergoryID = catergoryID;
            this.Price = price;
        }

        public Food(DataRow row)
        {
            this.ID = (int)row["food_id"];
            this.Name = row["food_name"].ToString();
            this.CatergoryID = (int)row["fcate_ID"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }

        public override bool Equals(object obj)
        {
            var food = obj as Food;
            return food.ID == this.ID && food.name == this.name && food.catergoryID == this.catergoryID && food.Price == this.price;
        }
    }
}
