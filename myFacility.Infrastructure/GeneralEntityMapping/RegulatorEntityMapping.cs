using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Models.Domains.Regulator;

namespace myFacility.Infrastructure.EntityBuilder
{
    public class RegulatorEntityMapping
    {
        public RegulatorEntityMapping(EntityTypeBuilder<TRegulator> entity)
        {
            entity.HasKey(e => e.RegId);

            entity.ToTable("t_regulator");

            entity.Property(e => e.RegId).HasColumnName("reg_id");

            entity.Property(e => e.Address)
                .IsRequired()
                .HasColumnName("address")
                .HasMaxLength(1000);

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by")
                .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(256);

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasColumnName("is_active")
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.LastModified)
                .HasColumnName("last_modified")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                .HasColumnName("modified_by")
                .HasMaxLength(255);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(256);

            entity.Property(e => e.RegCode)
                .IsRequired()
                .HasColumnName("reg_code")
                .HasMaxLength(20);
        }
    }
}
