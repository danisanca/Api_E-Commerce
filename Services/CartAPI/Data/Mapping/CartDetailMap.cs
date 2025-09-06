using CartAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartAPI.Data.Mapping
{
    public class CartDetailMap :IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CartHeaderId).IsRequired();
            builder.HasOne(x => x.CartHeader).WithOne().HasForeignKey<CartDetail>(x => x.CartHeaderId);
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
        }
    }
}
