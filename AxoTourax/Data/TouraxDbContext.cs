using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AxoTourax.Data
{
    public class TouraxDbContext : IdentityDbContext
    {
        public TouraxDbContext(DbContextOptions<TouraxDbContext> options)
           : base(options) { }

        //public virtual DbSet<ItemData> Items { get; set; }
    }
}
