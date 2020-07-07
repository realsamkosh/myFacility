using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Models.Domains.Invoice;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class TInvoiceEntityMapping
    {
        public TInvoiceEntityMapping(EntityTypeBuilder<TInvoice> entity)
        {

            entity.HasKey(e => e.InvoiceId);

            entity.ToTable("t_invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

            entity.Property(e => e.AccountName)
                            .HasColumnName("account_name")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.Property(e => e.Amount)
                            .HasColumnName("amount")
                            .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.AwardedJobId).HasColumnName("awarded_job_id");

            entity.Property(e => e.AwardedJobInvoiceStatusId).HasColumnName("awarded_job_invoice_status_id");

            entity.Property(e => e.BankId).HasColumnName("bank_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

            entity.Property(e => e.DeptId).HasColumnName("dept_id");

            entity.Property(e => e.Description)
                            .IsRequired()
                            .HasColumnName("description");

            entity.Property(e => e.InvoiceDate)
                            .HasColumnName("invoice_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.InvoiceNo)
                            .IsRequired()
                            .HasColumnName("invoice_no")
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.Property(e => e.InvoiceTypeId).HasColumnName("invoice_type_id");

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.IsProcessed).HasColumnName("is_processed");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.LowValueJobId).HasColumnName("low_value_job_id");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.ProcessedBy)
                            .HasColumnName("processed_by")
                            .HasMaxLength(255)
                            .IsUnicode(false);

            entity.Property(e => e.SkipJobCompletion).HasColumnName("skip_job_completion");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
        }
    }
    public class TInvoiceDetailsEntityMapping
    {
        public TInvoiceDetailsEntityMapping(EntityTypeBuilder<TInvoiceDetails> entity)
        {
            entity.HasKey(e => e.DetailsId);

            entity.ToTable("t_invoice_details");

            entity.Property(e => e.DetailsId).HasColumnName("details_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

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

            entity.Property(e => e.Note)
                            .IsRequired()
                            .HasColumnName("note")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.Property(e => e.Percentage)
                            .HasColumnName("percentage")
                            .HasColumnType("decimal(18, 1)");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.Property(e => e.Value)
                            .IsRequired()
                            .HasColumnName("value")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.HasOne(d => d.Invoice)
                            .WithMany(p => p.TInvoiceDetails)
                            .HasForeignKey(d => d.InvoiceId)
                            .HasConstraintName("fk1_invoice_details");
        }
    }
    public class TInvoiceStatusEntityMapping
    {
        public TInvoiceStatusEntityMapping(EntityTypeBuilder<TInvoiceStatus> entity)
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("t_invoice_status");

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
    public class TInvoiceTypeEntityMapping
    {
        public TInvoiceTypeEntityMapping(EntityTypeBuilder<TInvoiceType> entity)
        {

            entity.HasKey(e => e.TypeId);

            entity.ToTable("t_invoice_type");

            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.CheckJobCompletion).HasColumnName("check_job_completion");

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

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
        }
    }
}
