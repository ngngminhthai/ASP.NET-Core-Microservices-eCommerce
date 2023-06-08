using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Product.Infrastructure.Persistence;

public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
{
    ProductDbContext IDesignTimeDbContextFactory<ProductDbContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; database=MSProductDB; Integrated security=true; TrustServerCertificate=true");

        return new ProductDbContext(optionsBuilder.Options);
    }
}
