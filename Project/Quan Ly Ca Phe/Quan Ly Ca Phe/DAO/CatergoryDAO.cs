using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ca_Phe.DAO
{
    public class CatergoryDAO
    {
        private static CatergoryDAO instance;
        private static List<Catergory> catergoryList;
        public static List<Catergory> CatergoryList { get => catergoryList; set => catergoryList = value; }

        public static CatergoryDAO Instance
        {
            get { if (instance == null) instance = new CatergoryDAO(); return instance; }
            private set { instance = value; }
        }

        private CatergoryDAO() { CatergoryList = new List<Catergory>();}

        public List<Catergory> LoadCatergory()
        {
            List<Catergory> list = new List<Catergory>();

            string query = "SELECT * FROM FOOD_CATE";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Catergory cate = new Catergory(item);

                list.Add(cate);
            }

            return list;
        }

        public DataTable GetCatergory()
        {
            string query = "SELECT FCATE_ID AS 'ID', FCATE_NAME AS 'Tên thể loại' FROM dbo.FOOD_CATE";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            return data;
        }

        public Catergory LoadCatergoryByID(int id)
        {
            Catergory cate = null;

            string query = "SELECT * FROM FOOD_CATE WHERE FCATE_ID = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                cate = new Catergory(item);
                return cate;
            }

            return null;
        }

        public bool InsertCatergory(string name)
        {
            string query = String.Format("INSERT dbo.FOOD_CATE (FCATE_NAME) VALUES (N'{0}')", name);
            int res = DataProvider.Instance.ExecuteNonQuery(query);

            return res > 0;
        }

        public bool DeleteCatergory(int id)
        {
            int res = -1;

            List<Food> foodList = FoodDAO.Instance.GetFoodListByCateID(id);
            foreach (var item in foodList)
            {
                FoodDAO.Instance.DeleteFood(item.ID);
            }

            string query = String.Format("DELETE FROM dbo.FOOD_CATE WHERE FCATE_ID = {0}", id);
            res = DataProvider.Instance.ExecuteNonQuery(query);

            return res > 0;
        }

        public bool UpdateCatergory(string name, int id)
        {
            string query = String.Format("UPDATE dbo.FOOD_CATE SET FCATE_NAME = N'{0}' WHERE FCATE_ID = {1}", name, id);
            int res = DataProvider.Instance.ExecuteNonQuery(query);

            return res > 0;
        }

        /*----------------------------------------------*/
        public bool Test_InsertCatergory(int id, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Catergory newCate = new Catergory(name, id);
                CatergoryList.Add(newCate);
                return true;
            }
            return false;
        }
        public bool Test_EditCategory(int? id, string name)
        {
            if (id == null)
                return false;
            if (id >= 0 && !string.IsNullOrWhiteSpace(name))
            {
                Catergory existCatergory = CatergoryList.Where(x => x.IdCate == id).FirstOrDefault();
                if (existCatergory == null) return false;
                existCatergory.NameFood = name;
                return true;
            }
            return false;
        }
        public bool Test_DeleteCatergory(int id)
        {

            if (id >= 0)
            {
                Catergory choosenCate = CatergoryList.Where(x => x.IdCate == id).FirstOrDefault();
                if (choosenCate == null) return false;
                CatergoryList.Remove(choosenCate);
                return true;
            }
            return false;
        }
    }
}
