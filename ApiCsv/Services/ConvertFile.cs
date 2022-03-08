using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Requests;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ApiCsv.Services
{
    public class ConvertFile : IConvertFile
    {
        private readonly IMapper _mapper;

        public ConvertFile(IMapper mapper)
        {
            _mapper = mapper;
        }


        public List<Sales> ToSales(string path)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            List<Sales> sales = new();
            int count = 0;
            var enc1252 = CodePagesEncodingProvider.Instance.GetEncoding(1252);
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //var enc1252 = Encoding.GetEncoding(1252);

            // to avoid doing a split between double quotes
            Regex CSVParser = new(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            using (var reader = new StreamReader(path, enc1252))
                while (!reader.EndOfStream)
                {
                    count++;

                    var line = reader.ReadLine();

                    if (count == 1) continue;

                    List<string> values = CSVParser
                                            .Split(line)
                                            .Select(v => v.Replace("\"", ""))
                                            .ToList();

                    // mapping
                    Sales sale = _mapper.Map<Sales>(values);

                    sales.Add(sale);
                }

            return sales;
        }


    }
}
