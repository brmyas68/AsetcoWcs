

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;


namespace WCS.FluentAPI.Configures
{
 
    public class ConfigureOrderTransaction : IEntityTypeConfiguration<OrderTransaction>
    {
        public void Configure(EntityTypeBuilder<OrderTransaction> builder)
        {
            builder.HasKey(O => O.OrderTransaction_ID);
            builder.Property(O => O.OrderTransaction_UserID).IsRequired();
            builder.Property(O => O.OrderTransaction_DateTrans).IsRequired();
            builder.Property(O => O.OrderTransaction_TrackingCode).IsRequired().HasMaxLength(100);
        }
    }
}
