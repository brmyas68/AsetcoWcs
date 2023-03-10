

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCS.ClassDomain.Domains;

namespace WCS.FluentAPI.Configures
{
    public class ConfigureWornMaster : IEntityTypeConfiguration<WornMaster>
    {
        public void Configure(EntityTypeBuilder<WornMaster> builder)
        {
            builder.HasKey(W => W.WornMaster_ID);
            builder.Property(W => W.WornMaster_ID).IsRequired();
            builder.Property(W => W.WornMaster_OwnerID).IsRequired();
           // builder.Property(W => W.WornMaster_AgentID);
            builder.Property(W => W.WornMaster_RegisterDate).IsRequired();
            // builder.Property(W => W.WornMaster_BusinessID);
            // builder.Property(W => W.WornMaster_CivilContractID);
            builder.Property(W => W.WornMaster_State).IsRequired();
            builder.Property(W => W.WornMaster_TrackingCode).HasMaxLength(150);
            builder.Property(W => W.WornMaster_StateDesc).HasMaxLength(1000);
        }
    }
}
