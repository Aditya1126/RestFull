using Microsoft.EntityFrameworkCore;
using RestFull.Entities;

namespace RestFull.Data
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public object ServiceProvider { get; internal set; }
    }
}
