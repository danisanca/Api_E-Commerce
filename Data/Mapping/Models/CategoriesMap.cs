using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class CategoriesMap : IEntityTypeConfiguration<CategoriesModel>
    {
        public void Configure(EntityTypeBuilder<CategoriesModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.name).IsRequired().HasMaxLength(45).IsUnicode(true);
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.imageUrl).HasMaxLength(255); 
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
