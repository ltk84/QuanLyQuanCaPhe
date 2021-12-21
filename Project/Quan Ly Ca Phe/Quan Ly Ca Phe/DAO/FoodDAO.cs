using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return instance; }
            private set { instance = value; }
        }

        private static List<Food> foodList;
        public static List<Food> FoodList { get => foodList; set => foodList = value; }

        private FoodDAO()
        {
            FoodList = new List<Food>();
        }

        public List<Food> GetFoodListByCateID(int idCate)
        {
            List<Food> list = new List<Food>();

            string query = "SELECT * FROM dbo.FOOD WHERE FCATE_ID = " + idCate;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food f = new Food(item);

                list.Add(f);
            }

            return list;
        }

        public DataTable GetFoodList()
        {
            List<Food> list = new List<Food>();

            string query = "SELECT f.FOOD_ID AS 'ID' , FOOD_NAME AS 'Tên món', fc.FCATE_NAME AS 'Thể loại', PRICE AS 'Giá' FROM dbo.FOOD f, dbo.FOOD_CATE fc WHERE f.FCATE_ID = fc.FCATE_ID";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            return data;
        }

        public List<Food> Test_GetFoodList()
        {
            return FoodList;
        }

        public bool InsertFood(string name, int cate, float price)
        {
            string query = string.Format("INSERT INTO dbo.FOOD (FOOD_NAME , FCATE_ID , PRICE) VALUES(N'{0}', {1},  {2})", name, cate, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }


        public bool EditFood(int idFood, string name, int cate, float price)
        {
            string query = string.Format("UPDATE dbo.FOOD	SET FOOD_NAME = N'{0}', FCATE_ID = {1}, PRICE = {2} WHERE FOOD_ID = {3}", name, cate, price, idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }



        public bool DeleteFood(int idFood)
        {
            int result = -1;
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);

            string query = "DELETE FROM dbo.FOOD WHERE FOOD_ID = " + idFood;
            result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public DataTable FindFoodByName(string name)
        {
            string query = String.Format("SELECT f.FOOD_ID AS 'ID' , FOOD_NAME AS 'Tên món', fc.FCATE_NAME AS 'Thể loại', PRICE AS 'Giá' FROM dbo.FOOD f, dbo.FOOD_CATE fc WHERE f.FCATE_ID = fc.FCATE_ID AND dbo.fuConvertToUnsign1(FOOD_NAME) like dbo.fuConvertToUnsign1(N'%{0}%')", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            return data;
        }

        #region test function
        public bool Test_InsertFood(int id, string name, int cate, float price)
        {
            
            if (cate >= 0)
            {
                DateTime day = new DateTime(2008, 5, 1, 8, 30, 52);
                Food newFood = new Food(id, name, cate, price);
                FoodList.Add(newFood);
                return true;
            }
            return false;
        }

        public bool Test_EditFood(int id, string newName, int newCate, float newPrice)
        {
            if(newCate == null)
            {
                return false;
            }
            if (id >= 0 && newCate >= 0)
            {
                Food existFood = FoodList.Where(x => x.ID == id).FirstOrDefault();
                if (existFood == null) return false;
                existFood.Name = newName;
                existFood.CatergoryID = (int)newCate;
                existFood.Price = (float)newPrice;
                return true;
            }
            return false;
        }

        public bool Test_DeleteFood(int id)
        {
            FoodList.Clear();
            FoodList.Add(new Food(1, "Gà rán", 1, 30000));
            FoodList.Add(new Food(2, "Cà phê", 2, 10000));

            if (id >= 0)
            {
                Food choosenFood = FoodList.Where(x => x.ID == id).FirstOrDefault();
                if (choosenFood == null) return false;
                FoodList.Remove(choosenFood);
                return true;
            }
            return false;
        }

        public List<Food> Test_SearchFood(string keyword)
        {
            List<Food> list = new List<Food>()
            {
                new Food(1, "Gà rán", 1, 30000),
                new Food(2, "Gà luộc", 2, 10000)
            };

            FoodList.Clear();
            FoodList.AddRange(list);
            if (keyword == null) return new List<Food>();
            return FoodDAO.FoodList.Where(x => x.Name.Contains(keyword)).ToList();
        }
        #endregion

    }
}
