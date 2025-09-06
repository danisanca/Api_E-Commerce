using CartAPI.Data.Mapping;
using CartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CartAPI.Data
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<CartHeader> CartHeader { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartDetailMap());
            modelBuilder.ApplyConfiguration(new CartHeaderMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
