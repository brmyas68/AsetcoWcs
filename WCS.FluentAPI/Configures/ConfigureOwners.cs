

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureOwners : IEntityTypeConfiguration<Owners>
    {
        public void Configure(EntityTypeBuilder<Owners> builder)
        {
            builder.HasKey(O => O.Owners_ID);
            builder.Property(O => O.Owners_ID).IsRequired();
            builder.Property(O => O.Owners_UserID).IsRequired();
            builder.Property(O => O.Owners_Tell).HasMaxLength(100);
            builder.Property(O => O.Owners_Desc).HasMaxLength(1000);
            builder.Property(O => O.Owners_ShabaNumber).HasMaxLength(350);
        }
    }
}
