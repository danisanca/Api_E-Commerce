using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class OrderHeaderMap : IEntityTypeConfiguration<OrderHeaderModel>
    {
        public void Configure(EntityTypeBuilder<OrderHeaderModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.addressId).IsRequired();
            builder.HasOne(x => x.address).WithOne().HasForeignKey<OrderHeaderModel>(x => x.addressId).OnDelete(DeleteBehavior.Restrict) ;
            builder.Property(x => x.userId).IsRequired();
            builder.HasOne(x => x.user).WithOne().HasForeignKey<OrderHeaderModel>(x => x.userId); ;
            builder.Property(x => x.quantityItem).IsRequired();
            builder.Property(x => x.finalPrice).IsRequired();
            builder.Property(x => x.documentTpe).IsRequired();
            builder.Property(x => x.document).IsRequired();
            builder.Property(x => x.preferenceID).IsRequired();
            builder.Property(x => x.InitPoint).IsRequired();
            builder.Property(x => x.status).IsRequired();
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
