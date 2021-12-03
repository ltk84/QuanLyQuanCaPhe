using NUnit.Framework;
using Quan_Ly_Ca_Phe.DAO;
using Quan_Ly_Ca_Phe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCaPheTest
{
    public class TableTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(0, null, false)]
        [TestCase(-1, "Bàn 1", false)]
        [TestCase(0, "Bàn 1", true)]
        [TestCase(1, "Bàn 1", true)]
        public void Test_AddTable(int state, string name, bool expectedResult)
        {
            DateTime day = new DateTime(2008, 5, 1, 8, 30, 52);
            bool result = TableDAO.Instance.Test_InsertTable((DateTime.Now - day).Seconds, state, name);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(1, 0, null, false)]
        [TestCase(1, -1, "Bàn 3", false)]
        [TestCase(1, 0, "Bàn 3", true)]
        [TestCase(null, 0, "Bàn 3", false)]
        public void Test_EditTable(int id, int state, string name, bool expectedResult)
        {
            DateTime day = new DateTime(2008, 5, 1, 8, 30, 52);
            TableDAO.TableList.Add(new Table(1, 0, "Bàn 1"));
            TableDAO.TableList.Add(new Table(2, 1, "Bàn 2"));

            Table table = new Table(id, state, name);
            bool result = TableDAO.Instance.Test_EditTable(state, name, id);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(-1, false)]
        public void Test_DeleteTable(int id, bool expectedResult)
        {
            TableDAO.TableList.Clear();
            TableDAO.TableList.Add(new Table(1, 0, "Bàn 1"));
            TableDAO.TableList.Add(new Table(2, 1, "Bàn 2"));
            bool result = TableDAO.Instance.Test_DeleteTable(id);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
