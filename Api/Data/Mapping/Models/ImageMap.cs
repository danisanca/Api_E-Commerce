using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class ImageMap : IEntityTypeConfiguration<ImageModel>
    {
        public void Configure(EntityTypeBuilder<ImageModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.url).IsRequired().HasMaxLength(255).IsUnicode(true);
            builder.Property(x => x.size).IsRequired();
            builder.Property(x => x.productId).IsRequired();
            builder.HasOne(x => x.Product);
            builder.Property(x => x.shopId).IsRequired();
            builder.HasOne(x => x.Shop);
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
