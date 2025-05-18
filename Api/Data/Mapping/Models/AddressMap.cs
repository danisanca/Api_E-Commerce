using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
   
    public class AddressMap : IEntityTypeConfiguration<AddressModel>
    {
        public void Configure(EntityTypeBuilder<AddressModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.street).IsRequired().HasMaxLength(255);
            builder.Property(x => x.complement).IsRequired().HasMaxLength(255);
            builder.Property(x => x.neighborhood).HasMaxLength(255);
            builder.Property(x => x.city).IsRequired().HasMaxLength(45);
            builder.Property(x => x.state).IsRequired().HasMaxLength(2);
            builder.Property(x => x.zipcode).IsRequired().HasMaxLength(8);
            builder.Property(x => x.cellPhone).IsRequired().HasMaxLength(11);
            builder.Property(x => x.userId).IsUnicode(true);
            builder.HasOne(x => x.user);
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
