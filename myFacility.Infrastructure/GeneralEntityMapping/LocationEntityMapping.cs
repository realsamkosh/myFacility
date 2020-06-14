using myFacility.Models.Domains.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace myFacility.Infrastructure.EntityBuilder
{
    public class TCountryEntityMapping
    {
        public TCountryEntityMapping(EntityTypeBuilder<BtCountry> entity)
        {
            entity.HasKey(e => e.CountryId)
                    .HasName("pk_country");

            entity.ToTable("bt_country");

            entity.HasComment("The various countries of the world registered on the system");

            entity.HasIndex(e => e.CountryCode)
                .HasName("uq1_country")
                .IsUnique();

            entity.HasIndex(e => e.Name)
                .HasName("uq2_country")
                .IsUnique();

            entity.Property(e => e.CountryId).HasColumnName("country_id");

            entity.Property(e => e.CountryCode)
                .IsRequired()
                .HasColumnName("country_code")
                .HasMaxLength(3)
                .HasComment("The official code of registered country");

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by")
                .HasMaxLength(255)
                .HasComment("The creator of the country into the database");

            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("The date country was created into the database");

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
                .HasColumnName("name")
                .HasMaxLength(128)
                .HasComment("The country unique name");

            entity.Property(e => e.NationalCurrency)
                .HasColumnName("national_currency")
                .HasMaxLength(3)
                .HasComment("The country official currency");

            entity.Property(e => e.Nationality)
                .HasColumnName("nationality")
                .HasMaxLength(150);
        }
    }
    public class TStateEntityMapping
    {
        public TStateEntityMapping(EntityTypeBuilder<BtState> entity)
        {
            entity.HasKey(e => e.StateId)
                        .HasName("pk_state");

            entity.ToTable("bt_state");

            entity.HasIndex(e => e.Name)
                .HasName("uq2_state")
                .IsUnique();

            entity.HasIndex(e => e.StateCode)
                .HasName("uq1_state")
                .IsUnique();

            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.Property(e => e.CountryId).HasColumnName("country_id");

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by")
                .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

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
                .HasMaxLength(128);

            entity.Property(e => e.StateCode)
                .IsRequired()
                .HasColumnName("state_code")
                .HasMaxLength(10);

            entity.HasOne(d => d.Country)
                .WithMany(p => p.BtState)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk1_state");
        }
    }
    public class TLgaEntityMapping
    {
        public TLgaEntityMapping(EntityTypeBuilder<BtLga> entity)
        {
            entity.HasKey(e => e.LgaId)
                      .HasName("pk_lga_code");

            entity.ToTable("bt_lga");

            entity.HasIndex(e => e.LgaCode)
                .HasName("uq1_lga")
                .IsUnique();

            entity.Property(e => e.LgaId).HasColumnName("lga_id");

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by")
                .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasColumnName("is_active")
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.LastModified)
                .HasColumnName("last_modified")
                .HasColumnType("datetime");

            entity.Property(e => e.LgaCode)
                .IsRequired()
                .HasColumnName("lga_code")
                .HasMaxLength(10);

            entity.Property(e => e.ModifiedBy)
                .HasColumnName("modified_by")
                .HasMaxLength(255);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(128);

            entity.Property(e => e.StateCode)
                .IsRequired()
                .HasColumnName("state_code")
                .HasMaxLength(10);

            entity.HasOne(d => d.StateCodeNavigation)
                .WithMany(p => p.BtLga)
                .HasPrincipalKey(p => p.StateCode)
                .HasForeignKey(d => d.StateCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk1_lga");
        }
    }
}
