using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Data.Mapping.Models
{
    public class EvidenceMap : IEntityTypeConfiguration<EvidenceModel>
    {
        public void Configure(EntityTypeBuilder<EvidenceModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.description).IsRequired().HasMaxLength(255);
            builder.Property(x => x.productId).IsRequired();
            builder.HasOne(x => x.product).WithMany(x => x.evidences);
            builder.Property(x => x.userId).IsRequired();
            builder.HasOne(x => x.user).WithMany(x => x.evidences);
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
