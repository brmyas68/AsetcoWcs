

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureWornCenter : IEntityTypeConfiguration<WornCenter>
    {
        public void Configure(EntityTypeBuilder<WornCenter> builder)
        {
            builder.HasKey(W => W.WornCenter_ID);
            builder.Property(W => W.WornCenter_ID).IsRequired();
            builder.Property(W => W.WornCenter_Name).HasMaxLength(200).IsRequired();
            builder.Property(W => W.WornCenter_ProvinceID).IsRequired();
            builder.Property(W => W.WornCenter_CityID).IsRequired();
            builder.Property(W => W.WornCenter_Fax).HasMaxLength(50);
            builder.Property(W => W.WornCenter_Email).HasMaxLength(200);
            builder.Property(W => W.WornCenter_Tell).HasMaxLength(50);
            builder.Property(W => W.WornCenter_Address).HasMaxLength(500);
            builder.Property(W => W.WornCenter_ManagerFullName).HasMaxLength(150);
        }
    }
}
