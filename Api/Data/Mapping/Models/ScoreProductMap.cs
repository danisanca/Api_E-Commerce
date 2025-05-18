using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class ScoreProductMap : IEntityTypeConfiguration<ScoreProductModel>
    {
        public void Configure(EntityTypeBuilder<ScoreProductModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.amountStars).IsRequired().HasMaxLength(45);
            builder.Property(x => x.productId).IsRequired();
            builder.HasOne(x => x.product).WithMany(x => x.scoreProducts);
            builder.Property(x => x.userId).IsRequired();
            builder.HasOne(x => x.user).WithMany(x => x.scoreProducts);
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
