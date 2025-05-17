using ApiEstoque.Data.Mapping.Models;
using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<CategoriesModel> Categories { get; set; }
        public DbSet<EvidenceModel> Evidence { get; set; }
        public DbSet<HistoryMovimentModel> HistoryMoviment { get; set; }
        public DbSet<HistoryPurchaseModel> HistoryPurchase { get; set; }
        public DbSet<ImageModel> Image { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<ShopModel> Shop { get; set; }
        public DbSet<ScoreProductModel> ScoreProduct { get; set; }
        public DbSet<StockModel> Stock { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<DiscountModel> Discount { get; set; }
        public DbSet<AddressModel> Adress { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CategoriesMap());
            modelBuilder.ApplyConfiguration(new EvidenceMap());
            modelBuilder.ApplyConfiguration(new HistoryMovimentMap());
            modelBuilder.ApplyConfiguration(new HistoryPurchaseMap());
            modelBuilder.ApplyConfiguration(new ImageMap());
            modelBuilder.ApplyConfiguration(new ShopMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ScoreProductMap());
            modelBuilder.ApplyConfiguration(new StockMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new DiscountMap());
            modelBuilder.ApplyConfiguration(new AddressMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
