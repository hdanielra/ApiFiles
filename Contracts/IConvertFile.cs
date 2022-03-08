using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IConvertFile
    {
        public List<Sales> ToSales(string path);
    }
}
