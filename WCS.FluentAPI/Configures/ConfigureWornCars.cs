

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureWornCars : IEntityTypeConfiguration<WornCars>
    {
        public void Configure(EntityTypeBuilder<WornCars> builder)
        {
            builder.HasKey(W => W.WornCars_ID);
            builder.Property(W => W.WornCars_ID).IsRequired();
           // builder.Property(W => W.WornCars_BrandID).IsRequired();
            builder.Property(W => W.WornCars_ModelID).IsRequired();
            builder.Property(W => W.WornCars_DocumentType);
            builder.Property(W => W.WornCars_Weight);
            builder.Property(W => W.WornCars_Desc).HasMaxLength(1000);
        }
    }
}
