using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Models.Domains.Job;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class TJobEntityMapping
    {
        public TJobEntityMapping(EntityTypeBuilder<TJob> entity)
        {
            entity.HasKey(e => e.JobId);

            entity.ToTable("t_job");

            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.Details)
                            .IsRequired()
                            .HasColumnName("details")
                            .IsUnicode(false);

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.JobAttachmentFileName)
                            .HasColumnName("job_attachment_file_name")
                            .IsUnicode(false);

            entity.Property(e => e.JobNo)
                            .IsRequired()
                            .HasColumnName("job_no")
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.Property(e => e.JobTypeId).HasColumnName("job_type_id");

            entity.Property(e => e.KpiProfileId).HasColumnName("kpi_profile_id");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.RequestingDeptId).HasColumnName("requesting_dept_id");

            entity.Property(e => e.SapGroupNo)
                            .HasColumnName("sap_group_no")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.Property(e => e.SapNo)
                            .HasColumnName("sap_no")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.Property(e => e.StartDate)
                            .HasColumnName("start_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.Status).HasColumnName("status");

            entity.Property(e => e.StopDate)
                            .HasColumnName("stop_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.Property(e => e.Title)
                            .IsRequired()
                            .HasColumnName("title")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.HasOne(d => d.JobType)
                            .WithMany(p => p.TJob)
                            .HasForeignKey(d => d.JobTypeId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_job");
        }
    }
    public class TJobCategoryEntityMapping
    {
        public TJobCategoryEntityMapping(EntityTypeBuilder<TJobCategory> entity)
        {

            entity.HasKey(e => e.CategoryId);

            entity.ToTable("t_job_category");

            entity.HasComment("This table holds various job published by organizations");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.Code)
                            .HasColumnName("code")
                            .HasMaxLength(50)
                            .IsUnicode(false);

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.Description)
                            .HasColumnName("description")
                            .IsUnicode(false);

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.JobTypeId).HasColumnName("job_type_id");

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
                            .HasMaxLength(50)
                            .IsUnicode(false);

            entity.HasOne(d => d.JobType)
                            .WithMany(p => p.TJobCategory)
                            .HasForeignKey(d => d.JobTypeId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_t_job_category");
        }
    }
    public class TJobCategoryDocumentEntityMapping
    {
        public TJobCategoryDocumentEntityMapping(EntityTypeBuilder<TJobCategoryDocument> entity)
        {
            entity.HasKey(e => e.MappingId);

            entity.ToTable("t_job_category_document");

            entity.HasComment("This table holds the job ctageory document mapping");

            entity.Property(e => e.MappingId).HasColumnName("mapping_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.DocumentId).HasColumnName("document_id");

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

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.HasOne(d => d.Document)
                            .WithMany(p => p.TJobCategoryDocument)
                            .HasForeignKey(d => d.DocumentId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk2_job_category_document");

            entity.HasOne(d => d.JobCategory)
                            .WithMany(p => p.TJobCategoryDocument)
                            .HasForeignKey(d => d.JobCategoryId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_job_category_document");
        }
    }
    public class TJobJobcategoryMappingEntityMapping
    {
        public TJobJobcategoryMappingEntityMapping(EntityTypeBuilder<TJobJobcategoryMapping> entity)
        {
            entity.HasKey(e => e.MappingId);

            entity.ToTable("t_job_jobcategory_mapping");

            entity.Property(e => e.MappingId).HasColumnName("mapping_id");

            entity.Property(e => e.JobCategoryId).HasColumnName("job_category_id");

            entity.Property(e => e.JobId).HasColumnName("job_id");

            entity.HasOne(d => d.JobCategory)
                            .WithMany(p => p.TJobJobcategoryMapping)
                            .HasForeignKey(d => d.JobCategoryId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk2_job_jobcategory_mapping");

            entity.HasOne(d => d.Job)
                            .WithMany(p => p.TJobJobcategoryMapping)
                            .HasForeignKey(d => d.JobId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_job_jobcategory_mapping");
        }
    }
    public class TJobTypeEntityMapping
    {
        public TJobTypeEntityMapping(EntityTypeBuilder<TJobType> entity)
        {
            entity.HasKey(e => e.JobtypeId);

            entity.ToTable("t_job_type");

            entity.HasComment("This table holds various job type");

            entity.Property(e => e.JobtypeId).HasColumnName("jobtype_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.Code)
                            .HasColumnName("code")
                            .HasMaxLength(50)
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

            entity.Property(e => e.Name)
                            .IsRequired()
                            .HasColumnName("name")
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
        }
    }
    public class TLowValueJobEntityMapping
    {
        public TLowValueJobEntityMapping(EntityTypeBuilder<TLowValueJob> entity)
        {
            entity.HasKey(e => e.LowvaluejobId);

            entity.ToTable("t_low_value_job");

            entity.Property(e => e.LowvaluejobId).HasColumnName("lowvaluejob_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.Details)
                            .IsRequired()
                            .HasColumnName("details");

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.JobNo)
                            .HasColumnName("job_no")
                            .HasMaxLength(100)
                            .IsUnicode(false);

            entity.Property(e => e.JobTypeId).HasColumnName("job_type_id");

            entity.Property(e => e.KpiProfileId).HasColumnName("kpi_profile_id");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.LowValueCode)
                            .HasColumnName("low_value_code")
                            .HasMaxLength(50)
                            .IsUnicode(false);

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.StartDate)
                            .HasColumnName("start_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.Status).HasColumnName("status");

            entity.Property(e => e.StopDate)
                            .HasColumnName("stop_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.Property(e => e.Title)
                            .IsRequired()
                            .HasColumnName("title")
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            entity.HasOne(d => d.JobType)
                            .WithMany(p => p.TLowValueJob)
                            .HasForeignKey(d => d.JobTypeId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk1_low_value_job");
        }
    }
}
