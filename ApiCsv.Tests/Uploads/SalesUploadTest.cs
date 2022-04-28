using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApiCsv.Tests.Uploads
{
    [TestClass]
    public class SalesUploadTest
    {
        private Mock<IWebHostEnvironment> _webEnvironment;

        [TestInitialize]
        public void Initialize()
        {
            _webEnvironment = new Mock<IWebHostEnvironment>();
        }



        [TestMethod]
        public void UploadFile_ShouldCreateFileInStorage()
        {
            // arrange
            var httpContextMock = new Mock<MemoryContext>();
            _webEnvironment = new Mock<IWebHostEnvironment>();
            _webEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            _webEnvironment.Setup(m => m.WebRootPath).Returns(AppDomain.CurrentDomain.BaseDirectory);
            _webEnvironment.Setup(m => m.ContentRootPath).Returns(AppDomain.CurrentDomain.BaseDirectory);
            Services.Uploads _upload = new Services.Uploads(_webEnvironment.Object);

            string path = AppDomain.CurrentDomain.BaseDirectory + @"FileStorage/";
            if (!Directory.Exists(path))
            {
                // creating the directory in bin/Debug/net5.0/FileStorage/
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            //Setup mock file using a memory stream
            var content = "5469,Milli Fulton,Sun of Saskatoon,2017 Ferrari 488 Spider,429987,6/19/2018";
            var fileName = "test.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            
            //create FormFile with desired data
            IFormFile file = new FormFile(ms, 0, ms.Length, "id_from_form", fileName);


            //Act
            var result = _upload.UploadFile(file);
            //var result = _upload.Object.UploadFile(file);


            // assert            
            Assert.IsTrue(result.Result.Contains(fileName));

        }






    }
}
