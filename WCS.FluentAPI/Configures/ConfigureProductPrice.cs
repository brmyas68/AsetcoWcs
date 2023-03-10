using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureProductPrice : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.HasKey(P => P.ProductPrice_ID);
            builder.Property(P => P.ProductPrice_ID).IsRequired();
            builder.Property(P => P.ProductPrice_Price).IsRequired();
            builder.Property(P => P.ProductPrice_ProductId).IsRequired();
        }
    }
}
