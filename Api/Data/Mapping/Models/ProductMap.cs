using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class ProductMap : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.name).IsRequired().HasMaxLength(45).IsUnicode(true);
            builder.Property(x => x.price).IsRequired();
            builder.Property(x => x.description).IsRequired().HasMaxLength(255);
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
            builder.Property(x => x.categoriesId).IsRequired();
            builder.HasOne(x => x.categories);
            builder.Property(x => x.shopId).IsRequired();
            builder.HasOne(x => x.shop)
            .WithMany(s => s.products)
            .HasForeignKey(x => x.shopId)
            .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
