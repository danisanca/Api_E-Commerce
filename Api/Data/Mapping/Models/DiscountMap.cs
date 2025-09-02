using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiEstoque.Data.Mapping.Models
{
    public class DiscountMap:IEntityTypeConfiguration<DiscountModel>
    {
        public void Configure(EntityTypeBuilder<DiscountModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.percentDiscount).IsRequired();
            builder.Property(x => x.productId).IsRequired();
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
