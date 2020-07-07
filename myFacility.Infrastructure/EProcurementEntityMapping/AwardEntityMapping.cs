using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Models.Domains.Award;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class TAwardedJobStatusEntityMapping
    {
        public TAwardedJobStatusEntityMapping(EntityTypeBuilder<TAwardedJobStatus> entity)
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("t_awarded_job_status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");

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

            entity.Property(e => e.Status)
                            .IsRequired()
                            .HasColumnName("status")
                            .HasMaxLength(50)
                            .IsUnicode(false);
        }
    }
    public class TAwardedJobsEntityMapping
    {
        public TAwardedJobsEntityMapping(EntityTypeBuilder<TAwardedJobs> entity)
        {
            entity.HasKey(e => e.AwardedJobId);

            entity.ToTable("t_awarded_jobs");

            entity.Property(e => e.AwardedJobId).HasColumnName("awarded_job_id");

            entity.Property(e => e.AwardStatusId).HasColumnName("award_status_id");

            entity.Property(e => e.AwardedDate)
                            .HasColumnName("awarded_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.EnableChangeForm).HasColumnName("enable_change_form");

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.PoNumber)
                            .IsRequired()
                            .HasColumnName("po_number")
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.Property(e => e.ProjectEndDate)
                            .HasColumnName("project_end_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.ProjectStartDate)
                            .HasColumnName("project_start_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.QuoteId).HasColumnName("quote_id");

            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.AwardStatus)
                            .WithMany(p => p.TAwardedJobs)
                            .HasForeignKey(d => d.AwardStatusId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk4_awarded_jobs");

            entity.HasOne(d => d.Job)
                            .WithMany(p => p.TAwardedJobs)
                            .HasForeignKey(d => d.JobId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_awarded_jobs");

            entity.HasOne(d => d.Quote)
                            .WithMany(p => p.TAwardedJobs)
                            .HasForeignKey(d => d.QuoteId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk3_awarded_jobs");

            entity.HasOne(d => d.Vendor)
                            .WithMany(p => p.TAwardedJobs)
                            .HasForeignKey(d => d.VendorId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk2_awarded_jobs");
        }
    }
}
