using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Basket.Infrastructure.Persistence
{
    public class BasketDbContextFactory : IDesignTimeDbContextFactory<BasketDbContext>
    {
        public BasketDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BasketDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; database=MSBasketDB; Integrated security=true; TrustServerCertificate=true");

            return new BasketDbContext(optionsBuilder.Options);
        }
    }
}
