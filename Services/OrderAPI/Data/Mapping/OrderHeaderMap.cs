using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderAPI.Models;

namespace OrderAPI.Data.Mapping
{
    public class OrderHeaderMap : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.TotalDiscont).IsRequired();
            builder.Property(x => x.ListCount).IsRequired();
            builder.Property(x => x.ExternalReference).IsRequired();
            builder.Property(x => x.PreferenceId).IsRequired();
            builder.Property(x => x.InitPoint).IsRequired();
            builder.Property(x => x.ReferenceCreatedAt).IsRequired();
            builder.Property(x => x.ExpireAt).IsRequired();
            builder.Property(x => x.PaymentStatus).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
