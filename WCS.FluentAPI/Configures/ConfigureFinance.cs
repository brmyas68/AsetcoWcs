

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureFinance : IEntityTypeConfiguration<Finance>
    {
        public void Configure(EntityTypeBuilder<Finance> builder)
        {
            builder.HasKey(F => F.Finance_ID);
            builder.Property(F => F.Finance_ID).IsRequired();
            builder.Property(F => F.Finance_WornMasterID).IsRequired();
            builder.Property(F => F.Finance_ModifirID).IsRequired();
            builder.Property(F => F.Finance_Desc).HasMaxLength(1000);
        }
    }
}
