using ApiEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiEstoque.Data.Mapping.Models
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.name).IsRequired().HasMaxLength(45);
            builder.Property(x => x.username).IsRequired().HasMaxLength(45).IsUnicode(true);
            builder.Property(x => x.email).IsRequired().HasMaxLength(100).IsUnicode(true);
            builder.Property(x => x.password).IsRequired().HasMaxLength(255);
            builder.Property(x => x.status).IsRequired().HasMaxLength(24);
            builder.Property(x => x.typeAccount).IsRequired();
            builder.Property(x => x.createdAt).IsRequired();
            builder.Property(x => x.updatedAt).IsRequired();
        }
    }
}
