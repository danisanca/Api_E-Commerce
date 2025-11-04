using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class HistoryMovimentMap : IEntityTypeConfiguration<HistoryMovimentModel>
    {
        public void Configure(EntityTypeBuilder<HistoryMovimentModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.amount).IsRequired().HasMaxLength(45);
            builder.Property(x => x.action).IsRequired().HasMaxLength(24);
            builder.Property(x => x.productId).IsRequired();
            builder.HasOne(x => x.product);
            builder.Property(x => x.userId).IsRequired();
            builder.HasOne(x => x.user).WithMany(h => h.historyMoviments).HasForeignKey(x => x.userId);
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
