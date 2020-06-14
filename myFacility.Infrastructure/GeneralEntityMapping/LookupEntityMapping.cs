using myFacility.Models.Domains.Lookups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace myFacility.Infrastructure.EntityBuilder
{
    public class BtSysConfigurationEntityMapping
    {
        public BtSysConfigurationEntityMapping(EntityTypeBuilder<BtSysConfiguration> entity)
        {
            entity.HasKey(e => e.ConfigId)
                    .HasName("pk_sys_configuration_gnsys");

            entity.ToTable("bt_sys_configuration");

            entity.Property(e => e.ConfigId).HasColumnName("config_id");

            entity.Property(e => e.ConfigValue)
                .IsRequired()
                .HasColumnName("config_value")
                .HasMaxLength(255)
                .HasDefaultValueSql("(N'')");

            entity.Property(e => e.ConfigValueDesc)
                .IsRequired()
                .HasColumnName("config_value_desc")
                .HasMaxLength(1000);

            entity.Property(e => e.DefaultValue)
                .IsRequired()
                .HasColumnName("default_value")
                .HasMaxLength(255)
                .HasDefaultValueSql("(N'')");

            entity.Property(e => e.Enabled)
                .HasColumnName("enabled")
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.LastModified)
                .HasColumnName("last_modified")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasColumnName("modified_by")
                .HasMaxLength(255);
        }
    }
}

