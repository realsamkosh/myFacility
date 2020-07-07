using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Models.Domains.Quote;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class TQuoteEntityMapping
    {
        public TQuoteEntityMapping(EntityTypeBuilder<TQuote> entity)
        {
            entity.HasKey(e => e.QuoteId);

            entity.ToTable("t_quote");

            entity.Property(e => e.QuoteId).HasColumnName("quote_id");

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

            entity.Property(e => e.IsRated).HasColumnName("is_rated");

            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.QuoteNo)
                            .IsRequired()
                            .HasColumnName("quote_no")
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.Property(e => e.RatedBy)
                            .HasColumnName("rated_by")
                            .HasMaxLength(255);

            entity.Property(e => e.Score)
                            .HasColumnName("score")
                            .HasColumnType("decimal(20, 4)");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Job)
                            .WithMany(p => p.TQuote)
                            .HasForeignKey(d => d.JobId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_quote");

            entity.HasOne(d => d.Vendor)
                            .WithMany(p => p.TQuote)
                            .HasForeignKey(d => d.VendorId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk2_quote");
        }
    }
    public class TQuoteKpiLogEntityMapping
    {
        public TQuoteKpiLogEntityMapping(EntityTypeBuilder<TQuoteKpiLog> entity)
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("t_quote_kpi_log");

            entity.Property(e => e.LogId).HasColumnName("log_id");

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

            entity.Property(e => e.Kpi)
                            .IsRequired()
                            .HasColumnName("kpi")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.Property(e => e.KpiSettingId).HasColumnName("kpi_setting_id");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.QuoteId).HasColumnName("quote_id");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.Quote)
                            .WithMany(p => p.TQuoteKpiLog)
                            .HasForeignKey(d => d.QuoteId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_quote_kpi_log");
        }
    }
}
