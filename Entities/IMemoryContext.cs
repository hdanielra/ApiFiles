using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IMemoryContext
    {
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Files> Files { get; set; }

    }
}
