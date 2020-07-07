using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Models.Domains.Vendor;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class TVendorAccountStatusEntityMapping
    {
        public TVendorAccountStatusEntityMapping(EntityTypeBuilder<TVendorAccountStatus> entity)
        {
            entity.HasKey(e => e.StatusCode);

            entity.ToTable("t_vendor_account_status");

            entity.HasComment("This table hold vendors account status");

            entity.Property(e => e.StatusCode)
                .HasColumnName("status_code")
                .HasMaxLength(1);

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.Description)
                            .IsRequired()
                            .HasColumnName("description")
                            .HasMaxLength(50);

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
        }
    }
    public class TVendorAppraisalEntityMapping
    {
        public TVendorAppraisalEntityMapping(EntityTypeBuilder<TVendorAppraisal> entity)
        {
            entity.HasKey(e => e.AppraisalId);

            entity.ToTable("t_vendor_appraisal");

            entity.HasComment("This table hold vendor Appraisal");

            entity.Property(e => e.AppraisalId).HasColumnName("appraisal_id");

            entity.Property(e => e.AvgJobCompletionScore)
                            .HasColumnName("avg_job_completion_score")
                            .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.AvgOtherScore)
                            .HasColumnName("avg_other_score")
                            .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.AvgQuoteScore)
                            .HasColumnName("avg_quote_score")
                            .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.Comment)
                            .IsRequired()
                            .HasColumnName("comment")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.DateSent)
                            .HasColumnName("date_sent")
                            .HasColumnType("datetime");

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.IsSent).HasColumnName("is_sent");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.PeriodId).HasColumnName("period_id");

            entity.Property(e => e.PeriodTypeId).HasColumnName("period_type_id");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Period)
                            .WithMany(p => p.TVendorAppraisal)
                            .HasForeignKey(d => d.PeriodId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk2_vendor_appraisal");

            entity.HasOne(d => d.PeriodType)
                            .WithMany(p => p.TVendorAppraisal)
                            .HasForeignKey(d => d.PeriodTypeId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_vendor_appraisal");
        }
    }
    public class TVendorAppraisalKpiEntityMapping
    {
        public TVendorAppraisalKpiEntityMapping(EntityTypeBuilder<TVendorAppraisalKpi> entity)
        {
            entity.HasKey(e => e.KpiId);

            entity.ToTable("t_vendor_appraisal_kpi");

            entity.HasComment("This table keeps the vendor appraisal KPI Log");

            entity.Property(e => e.KpiId).HasColumnName("kpi_id");

            entity.Property(e => e.AppraisalId).HasColumnName("appraisal_id");

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

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.Score)
                            .HasColumnName("score")
                            .HasColumnType("decimal(20, 2)");

            entity.Property(e => e.Weight)
                            .HasColumnName("weight")
                            .HasColumnType("decimal(20, 2)");

            entity.HasOne(d => d.Appraisal)
                            .WithMany(p => p.TVendorAppraisalKpi)
                            .HasForeignKey(d => d.AppraisalId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_vendor_appraisal_kpi");
        }
    }
    public class TVendorBusinessRegistrationEntityMapping
    {
        public TVendorBusinessRegistrationEntityMapping(EntityTypeBuilder<TVendorBusinessRegistration> entity)
        {
            entity.HasKey(e => e.BvregId);

            entity.ToTable("t_vendor_business_registration");

            entity.HasComment("This table hold the details of Vendor Business Registration");

            entity.Property(e => e.BvregId).HasColumnName("bvreg_id");

            entity.Property(e => e.AccountName)
                            .HasColumnName("account_name")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.Property(e => e.AccountNumber)
                            .HasColumnName("account_number")
                            .HasMaxLength(50)
                            .IsUnicode(false);

            entity.Property(e => e.BankId).HasColumnName("bank_id");

            entity.Property(e => e.CanVisit).HasColumnName("can_visit");

            entity.Property(e => e.CompanyName)
                            .IsRequired()
                            .HasColumnName("company_name")
                            .HasMaxLength(200)
                            .IsUnicode(false);

            entity.Property(e => e.ContactAddress)
                            .IsRequired()
                            .HasColumnName("contact_address")
                            .HasMaxLength(300)
                            .IsUnicode(false);

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

            entity.Property(e => e.NewCompanyName)
                            .HasColumnName("new_company_name")
                            .IsUnicode(false);

            entity.Property(e => e.PhoneNumber)
                            .HasColumnName("phone_number")
                            .HasMaxLength(50)
                            .IsUnicode(false);

            entity.Property(e => e.PvregId).HasColumnName("pvreg_id");

            entity.Property(e => e.RegOfficeAddress)
                            .IsRequired()
                            .HasColumnName("reg_office_address")
                            .HasMaxLength(300)
                            .IsUnicode(false);

            entity.Property(e => e.WebsiteAddress)
                            .HasColumnName("website_address")
                            .HasMaxLength(200)
                            .IsUnicode(false);

            entity.HasOne(d => d.Pvreg)
                            .WithMany(p => p.TVendorBusinessRegistration)
                            .HasForeignKey(d => d.PvregId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_vendor_business_registration");
        }
    }
    public class TVendorJobCategoryDocEntityMapping
    {
        public TVendorJobCategoryDocEntityMapping(EntityTypeBuilder<TVendorJobCategoryDoc> entity)
        {
            entity.HasKey(e => e.MappingId);

            entity.ToTable("t_vendor_job_category_doc");

            entity.Property(e => e.MappingId).HasColumnName("mapping_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.DocumentName)
                            .IsRequired()
                            .HasColumnName("document_name")
                            .HasMaxLength(500)
                            .IsUnicode(false);

            entity.Property(e => e.ExpiryDate)
                            .HasColumnName("expiry_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.JobCategoryDocId).HasColumnName("job_category_doc_id");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.MimeType)
                            .HasColumnName("mime_type")
                            .HasMaxLength(250)
                            .IsUnicode(false);

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.Property(e => e.VendorJobCatId).HasColumnName("vendor_job_cat_id");

            entity.HasOne(d => d.JobCategoryDoc)
                            .WithMany(p => p.TVendorJobCategoryDoc)
                            .HasForeignKey(d => d.JobCategoryDocId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_vendor_job_category_doc");

            entity.HasOne(d => d.VendorJobCat)
                            .WithMany(p => p.TVendorJobCategoryDoc)
                            .HasForeignKey(d => d.VendorJobCatId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk2_vendor_job_category");
        }
    }
    public class TVendorJobCategoryMappingEntityMapping
    {
        public TVendorJobCategoryMappingEntityMapping(EntityTypeBuilder<TVendorJobCategoryMapping> entity)
        {
            entity.HasKey(e => e.MappingId);

            entity.ToTable("t_vendor_job_category_mapping");

            entity.Property(e => e.MappingId).HasColumnName("mapping_id");

            entity.Property(e => e.ApprovalStatus)
                            .IsRequired()
                            .HasColumnName("approval_status")
                            .HasMaxLength(1)
                            .IsUnicode(false);

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

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.PvregId).HasColumnName("pvreg_id");

            entity.HasOne(d => d.JobCategory)
                            .WithMany(p => p.TVendorJobCategoryMapping)
                            .HasForeignKey(d => d.JobCategoryId)
                            .HasConstraintName("fk2_vendor_job_category_mapping");

            entity.HasOne(d => d.Pvreg)
                            .WithMany(p => p.TVendorJobCategoryMapping)
                            .HasForeignKey(d => d.PvregId)
                            .HasConstraintName("fk1_vendor_job_category_mapping");
        }
    }
    public class TVendorPersonalRegistrationEntityMapping
    {
        public TVendorPersonalRegistrationEntityMapping(EntityTypeBuilder<TVendorPersonalRegistration> entity)
        {
            entity.HasKey(e => e.PvregId);

            entity.ToTable("t_vendor_personal_registration");

            entity.HasComment("This table hold the vendor registration record");

            entity.Property(e => e.PvregId).HasColumnName("pvreg_id");

            entity.Property(e => e.AccountStatus)
                            .IsRequired()
                            .HasColumnName("account_status")
                            .HasMaxLength(1);

            entity.Property(e => e.ApplicantName)
                            .IsRequired()
                            .HasColumnName("applicant_name")
                            .HasMaxLength(500)
                            .IsUnicode(false);

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.Comment)
                            .HasColumnName("comment")
                            .IsUnicode(false);

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
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.Property(e => e.FormNumber)
                            .IsRequired()
                            .HasColumnName("form_number")
                            .HasMaxLength(50)
                            .IsUnicode(false);

            entity.Property(e => e.HomeAddress)
                            .IsRequired()
                            .HasColumnName("home_address")
                            .HasMaxLength(500)
                            .IsUnicode(false);

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.IsCreatedByTenant).HasColumnName("is_created_by_tenant");

            entity.Property(e => e.IsProcurement).HasColumnName("is_procurement");

            entity.Property(e => e.IsSupply).HasColumnName("is_supply");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.PhoneNumber1)
                            .IsRequired()
                            .HasColumnName("phone_number_1")
                            .HasMaxLength(50)
                            .IsUnicode(false);

            entity.Property(e => e.PhoneNumber2)
                            .HasColumnName("phone_number_2")
                            .HasMaxLength(50)
                            .IsUnicode(false);

            entity.Property(e => e.PositionInOrganization)
                            .IsRequired()
                            .HasColumnName("position_in_organization")
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
        }
    }
    public class TVendorRejectionHistoryEntityMapping
    {
        public TVendorRejectionHistoryEntityMapping(EntityTypeBuilder<TVendorRejectionHistory> entity)
        {
            entity.HasKey(e => e.RejectionId);

            entity.ToTable("t_vendor_rejection_history");

            entity.HasComment("This table holds the vendor registration rejection history if any");

            entity.Property(e => e.RejectionId).HasColumnName("rejection_id");

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

            entity.Property(e => e.PvregId).HasColumnName("pvreg_id");

            entity.Property(e => e.Reason)
                            .IsRequired()
                            .HasColumnName("reason");

            entity.HasOne(d => d.Pvreg)
                            .WithMany(p => p.TVendorRejectionHistory)
                            .HasForeignKey(d => d.PvregId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_vendor_rejection_history");
        }
    }
}
