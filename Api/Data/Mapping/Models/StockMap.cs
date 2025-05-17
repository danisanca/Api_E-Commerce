using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class StockMap : IEntityTypeConfiguration<StockModel>
    {
        public void Configure(EntityTypeBuilder<StockModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.amount).IsRequired();
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.productId).IsRequired().IsUnicode(true);
            builder.HasOne(x => x.product).WithMany(s => s.stocks);
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
