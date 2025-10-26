using Microsoft.EntityFrameworkCore;
using order_service.Models;

namespace order_service.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options) { }

        public DbSet<Orders> Orders => Set<Orders>();
    }
}
