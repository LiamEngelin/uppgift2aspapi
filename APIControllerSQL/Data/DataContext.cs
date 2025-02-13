using APIControllerSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace APIControllerSQL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        public DbSet<Forceuser> Forceusers { get; set; }
    }
}
