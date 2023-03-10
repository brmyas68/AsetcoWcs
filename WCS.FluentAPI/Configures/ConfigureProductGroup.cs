using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureProductGroup : IEntityTypeConfiguration<ProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.HasKey(P => P.ProductGroup_ID);
            builder.Property(P => P.ProductGroup_Name).HasMaxLength(300).IsRequired();
            builder.Property(P => P.ProductGroup_Type).IsRequired();

        }
    }
}
