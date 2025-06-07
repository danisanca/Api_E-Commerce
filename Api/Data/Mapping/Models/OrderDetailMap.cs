using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class OrderDetailMap : IEntityTypeConfiguration<OrderDetailModel>
    {
        public void Configure(EntityTypeBuilder<OrderDetailModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.orderHeaderId).IsRequired();
            builder.HasOne(x => x.orderHeader).WithMany(o=>o.orderDetails).OnDelete(DeleteBehavior.Restrict); 
           
            builder.Property(x => x.productId).IsRequired();
            builder.HasOne(x => x.products).WithMany(p => p.orderDetails).HasForeignKey(x => x.productId); ;

            builder.Property(x => x.amount).IsRequired();
            builder.Property(x => x.price).IsRequired();
            builder.Property(x => x.status).IsRequired();
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
