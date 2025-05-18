using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class HistoryPurchaseMap : IEntityTypeConfiguration<HistoryPurchaseModel>
    {
        public void Configure(EntityTypeBuilder<HistoryPurchaseModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.cartProducts).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(x => x.price).IsRequired();
            builder.Property(x => x.userId).IsRequired();
            builder.HasOne(x => x.user).WithMany(h => h.historyPurchases);
            builder.Property(x => x.externalReference).IsRequired();
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
