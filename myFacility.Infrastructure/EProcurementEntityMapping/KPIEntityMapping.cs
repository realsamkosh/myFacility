using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Models.Domains.KPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class TKpiProfileEntityMapping
    {
        public TKpiProfileEntityMapping(EntityTypeBuilder<TKpiProfile> entity)
        {
            entity.HasKey(e => e.ProfileId);

            entity.ToTable("t_kpi_profile");

            entity.HasComment("This table holds the KPI Profile Setup");

            entity.Property(e => e.ProfileId).HasColumnName("profile_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.JobCategoryId).HasColumnName("job_category_id");

            entity.Property(e => e.KpiTypeId).HasColumnName("kpi_type_id");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.Name)
                            .IsRequired()
                            .HasColumnName("name")
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.HasOne(d => d.JobCategory)
                            .WithMany(p => p.TKpiProfile)
                            .HasForeignKey(d => d.JobCategoryId)
                            .HasConstraintName("fk1_kpi_profile");
        }
    }
    public class TKpiTypeEntityMapping
    {
        public TKpiTypeEntityMapping(EntityTypeBuilder<TKpiType> entity)
        {
            entity.HasKey(e => e.KpiTypeId);

            entity.ToTable("t_kpi_type");

            entity.HasComment("This table holds KPI type on the system");

            entity.Property(e => e.KpiTypeId).HasColumnName("kpi_type_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

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

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.Property(e => e.Title)
                            .IsRequired()
                            .HasColumnName("title")
                            .HasMaxLength(150)
                            .IsUnicode(false);
        }
    }
}
