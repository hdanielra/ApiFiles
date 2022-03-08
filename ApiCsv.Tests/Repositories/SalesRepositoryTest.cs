using Entities;
using Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiCsv.Tests.Repository
{
    [TestClass]
    public class SalesRepositoryTest
    {

        private List<Sales> _listSales;
        private MemoryContext _memoryContext;
        private RepositoryManager _manager;


        [TestInitialize]
        public void Initialize()
        {
            _listSales = FakeDbContext.GetListSales();
            _memoryContext = FakeDbContext.GetDatabaseContext(_listSales);
            _manager = new RepositoryManager(_memoryContext);
        }



        [TestMethod]
        public void FindAll_ShouldReturnAllSales()
        {
            var sales = _manager.Sales.FindAll(false);

            Assert.AreEqual(11, sales.ToList().Count);
            Assert.AreEqual("Customer 1", sales.ToList()[0].CustomerName);
            Assert.AreEqual("Customer 2", sales.ToList()[1].CustomerName);
            Assert.AreEqual("Customer 3", sales.ToList()[2].CustomerName);
        }



        [TestMethod]
        public void FindByCondition_ShouldReturnOneRecord()
        {
            int index = 2;
            var sale = _manager.Sales.FindByCondition(s => s.DealNumber.Equals(index), false).FirstOrDefault();

            Assert.AreEqual("Customer 2", sale.CustomerName);
        }


        [TestMethod]
        public void GetBest_ShouldReturnOneRecord()
        {
            var best = _manager.Sales.GetBest(false).Result;

            var expected = new { Vehicle = "Vehicle 5", Count = 2 };

            Assert.AreEqual(best.ToString(), expected.ToString());
        }




        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void FindByCondition_ShouldThowNullReferenceException()
        {
            int index = -2;
            var sale = _manager.Sales.FindByCondition(s => s.DealNumber.Equals(index), false).FirstOrDefault();

            Assert.AreEqual("Customer 2", sale.CustomerName);
        }


        [TestMethod]
        public void Create_ShouldCreateSale()
        {
            var sales = _manager.Sales.FindAll(false);

            int len = sales.ToList().Count;

            Sales sale = new Sales()
            {
                DealNumber = len + 1,
                CustomerName = $"Customer {len + 1}",
                DealershipName = $"Dealership {len + 1}",
                Vehicle = $"Vehicle {len + 1}",
                Price = 200000,
                Date = DateTime.Now
            };

            _manager.Sales.Create(sale);
            _manager.Save().Wait();
            var s = _manager.Sales.FindByCondition(s => s.DealNumber.Equals(len + 1), false).FirstOrDefault();

            //sales = _manager.Sales.FindAll(false);

            Assert.AreEqual($"Customer {len + 1}", s.CustomerName);
        }




    }
}
