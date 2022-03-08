using Entities.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Entities.Configuration
{

    // data seed to start the database in memory
    public class DbMemoryConfiguration
    {
        public static void SeedData(MemoryContext context)
        {

            List<Sales> sales = new()
            {
                new Sales
                {
                    DealNumber = 1,
                    CustomerName = "Sam R.",
                    DealershipName = "Sun of Saskatoon",
                    Vehicle = "BMW M760Li Xdrive Sedan",
                    Price = 170000.0,
                    Date =  DateTime.ParseExact("1/1/2001", "M/d/yyyy", CultureInfo.InvariantCulture)
        }
    };

    //var dateTime = DateTime.ParseExact("01/01/2001", formats, new CultureInfo("en-US"), DateTimeStyles.None);


    List<Files> files = new()
    {
        new Files
        {
            id = 1,
            name = "uploaded_file",
            ext = "csv",
            length = 0.1,
            path = "D://FilesStorage//uploaded_file.csv"
        }
    };

    context.Sales.AddRange(sales);
            context.Files.AddRange(files);

            context.SaveChanges();
        }
    }
}
