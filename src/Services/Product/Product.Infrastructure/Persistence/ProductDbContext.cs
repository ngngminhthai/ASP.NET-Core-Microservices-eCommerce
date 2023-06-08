using Microsoft.EntityFrameworkCore;
using Product.Domain.AggregateModels.ProductAggregate;

namespace Product.Infrastructure.Persistence
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ProductItem> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
