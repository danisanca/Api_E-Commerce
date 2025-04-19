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
            builder.Property(x => x.amount).IsRequired();
            builder.Property(x => x.price).IsRequired();
            builder.Property(x => x.productId).IsRequired().IsUnicode(true);
            builder.HasOne(x => x.product);
            builder.Property(x => x.userId).IsRequired();
            builder.HasOne(x => x.user).WithMany(h => h.historyPurchases);
            builder.Property(x => x.createdAt).IsRequired();
        }
    }
}
