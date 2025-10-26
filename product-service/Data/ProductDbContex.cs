using Microsoft.EntityFrameworkCore;
using product_service.Models;

namespace product_service.Data
{
    public class ProductDbContex : DbContext
    {

        public ProductDbContex(DbContextOptions<ProductDbContex> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
