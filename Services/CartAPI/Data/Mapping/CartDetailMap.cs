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
            builder.HasOne(x => x.CartHeader).WithMany(h => h.CartDetails).HasForeignKey(x => x.CartHeaderId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
        }
    }
}
