using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiEstoque.Data.Mapping.Models
{
    public class ShopMap : IEntityTypeConfiguration<ShopModel>
    {
        public void Configure(EntityTypeBuilder<ShopModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.name).IsRequired().HasMaxLength(45);
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.userId).IsUnicode(true);
            builder.HasOne(x => x.user).WithOne().HasForeignKey<ShopModel>(x => x.userId);
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
