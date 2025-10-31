using OrderAPI.Data.Mapping;
using OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Data
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderDetailMap());
            modelBuilder.ApplyConfiguration(new OrderHeaderMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
