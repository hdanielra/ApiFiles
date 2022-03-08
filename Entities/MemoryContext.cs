using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Entities
{
    public class MemoryContext : DbContext
    {
        public MemoryContext(DbContextOptions<MemoryContext> options) : base(options)
        {

        }


        public virtual DbSet<Sales> Sales { get; set; }
        public DbSet<Files> Files { get; set; }

    }

}
