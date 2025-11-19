using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAPI.Models;

namespace OrderAPI.Data.Mapping
{
    public class OrderDetailMap : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderHeaderId).IsRequired();
            builder.HasOne(x => x.OrderHeader).WithMany(h => h.OrderDetails)
                .HasForeignKey(x => x.OrderHeaderId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x => x.ProductName).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
