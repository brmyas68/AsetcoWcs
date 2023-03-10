using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureProduct : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(P => P.Product_ID);
            builder.Property(P => P.Product_Name).HasMaxLength(300).IsRequired();
            builder.Property(P => P.Product_NameEn).HasMaxLength(300);
            builder.Property(P => P.Product_Model).HasMaxLength(300);
            builder.Property(P => P.Product_Series).HasMaxLength(300);
            builder.Property(P => P.Product_Description);
            builder.Property(P => P.Product_Type).IsRequired();
            builder.Property(P => P.Product_Group).IsRequired();

        }
    }
}
