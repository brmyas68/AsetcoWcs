

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureCivilContract : IEntityTypeConfiguration<CivilContract>
    {
        public void Configure(EntityTypeBuilder<CivilContract> builder)
        {
            builder.HasKey(C => C.CivilContract_ID);
            builder.Property(C => C.CivilContract_ID).IsRequired();
            builder.Property(C => C.CivilContract_InqDocumentDesc).HasMaxLength(1000);
            builder.Property(C => C.CivilContract_InqPoliceDesc).HasMaxLength(1000);
            builder.Property(C => C.CivilContract_InqValidationDesc).HasMaxLength(1000);
            builder.Property(C => C.CivilContract_Desc).HasMaxLength(1000);
            builder.Property(C => C.CivilContract_DocumentName).HasMaxLength(200);
            builder.Property(C => C.CivilContract_IsOwnerCarDesc).HasMaxLength(500);
        }
    }
}
