


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;


namespace WCS.FluentAPI.Configures
{
    public class ConfigureMessage : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(M => M.Message_ID);
            builder.Property(M => M.Message_ID).IsRequired();
            builder.Property(M => M.Message_Mobile).HasMaxLength(11);
            builder.Property(M => M.Message_Text).HasMaxLength(500);
            builder.Property(M => M.Message_FullName).HasMaxLength(100);
            builder.Property(M => M.Message_TenantID).IsRequired();
        }
    }
}
