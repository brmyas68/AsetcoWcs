

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureBusiness : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.HasKey(B => B.Business_ID);
            builder.Property(B => B.Business_ID).IsRequired();
            builder.Property(B => B.Business_SplitDesc).HasMaxLength(1000);
            builder.Property(B => B.Business_PriceDesc).HasMaxLength(1000);
            builder.Property(B => B.Business_ParkingDesc).HasMaxLength(1000);
            builder.Property(B => B.Business_AgreementDesc).HasMaxLength(1000);
        }
    }
}
