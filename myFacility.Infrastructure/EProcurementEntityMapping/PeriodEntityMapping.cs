using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Models.Domains.Period;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class TPeriodScheduleEntityMapping
    {
        public TPeriodScheduleEntityMapping(EntityTypeBuilder<TPeriodSchedule> entity)
        {
            entity.HasKey(e => e.PeriodId);

            entity.ToTable("t_period_schedule");

            entity.Property(e => e.PeriodId).HasColumnName("period_id");

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

            entity.Property(e => e.Name)
                            .IsRequired()
                            .HasColumnName("name")
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.Property(e => e.PeriodTypeId).HasColumnName("period_type_id");

            entity.Property(e => e.StartDate)
                            .HasColumnName("start_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.StopDate)
                            .HasColumnName("stop_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.PeriodType)
                            .WithMany(p => p.TPeriodSchedule)
                            .HasForeignKey(d => d.PeriodTypeId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_period_schedule");
        }
    }
    public class TPeriodTypeEntityMapping
    {
        public TPeriodTypeEntityMapping(EntityTypeBuilder<TPeriodType> entity)
        {
            entity.HasKey(e => e.PreriodTypeId);

            entity.ToTable("t_period_type");

            entity.Property(e => e.PreriodTypeId).HasColumnName("preriod_type_id");

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

            entity.Property(e => e.Name)
                            .IsRequired()
                            .HasColumnName("name")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
        }
    }
}
