using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiCsv.Tests
{
    public class FakeDbContext
    {
        public static MemoryContext GetDatabaseContext(List<Sales> listSales = null)
        {
            var options = new DbContextOptionsBuilder<MemoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new MemoryContext(options);
            databaseContext.Database.EnsureCreated();

            if (databaseContext.Sales.Count() <= 0)
            {
                databaseContext.Sales.AddRange(listSales ?? GetListSales());
                databaseContext.Sales.Add(new Sales()
                {
                    DealNumber = 11,
                    CustomerName = $"Customer 5",
                    DealershipName = $"Dealership 5",
                    Vehicle = $"Vehicle 5",
                    Price = 200000,
                    Date = DateTime.Now
                });

                databaseContext.SaveChanges();
            }
            return databaseContext;
        }



        public static List<Sales> GetListSales()
        {
            List<Sales> sales = new List<Sales>();
            for (int i = 1; i <= 10; i++)
            {
                sales.Add(new Sales()
                {
                    DealNumber = i,
                    CustomerName = $"Customer {i}",
                    DealershipName = $"Dealership {i}",
                    Vehicle = $"Vehicle {i}",
                    Price = 150000 + (i * 10000),
                    Date = DateTime.Now
                });
            }
            return sales;
        }
    }
}
