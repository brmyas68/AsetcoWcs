

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;


namespace WCS.FluentAPI.Configures
{
 
    public class ConfigureOrder : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(B => B.Order_ID);
            builder.Property(B => B.Order_ID).IsRequired();
            builder.Property(B => B.Order_UserID).IsRequired();
            builder.Property(B => B.Order_ProductID).IsRequired();
            builder.Property(B => B.Order_DateRegister).IsRequired();
            builder.Property(B => B.Order_Desc).HasMaxLength(500);
            builder.Property(B => B.Order_Count).IsRequired();
            builder.Property(B => B.Order_Price).IsRequired();
        }
    }
}
