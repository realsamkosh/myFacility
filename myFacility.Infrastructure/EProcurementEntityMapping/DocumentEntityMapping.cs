using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Models.Domains.Document;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class TDocumentEntityMapping
    {
        public TDocumentEntityMapping(EntityTypeBuilder<TDocument> entity)
        {
            entity.HasKey(e => e.DocumentId);

            entity.ToTable("t_document");

            entity.HasComment("This table holds the list of document on the system");

            entity.Property(e => e.DocumentId).HasColumnName("document_id");

            entity.Property(e => e.BranchId).HasColumnName("branch_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.Description)
                            .HasColumnName("description")
                            .HasMaxLength(250)
                            .IsUnicode(false);

            entity.Property(e => e.ExpiringDate)
                            .HasColumnName("expiring_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.HasExpiringDate).HasColumnName("has_expiring_date");

            entity.Property(e => e.IsActive)
                            .IsRequired()
                            .HasColumnName("is_active")
                            .HasDefaultValueSql("((1))");

            entity.Property(e => e.IsProcurement).HasColumnName("is_procurement");

            entity.Property(e => e.IsSupply).HasColumnName("is_supply");

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.MaxFileSize).HasColumnName("max_file_size");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");

            entity.Property(e => e.Title)
                            .IsRequired()
                            .HasColumnName("title")
                            .HasMaxLength(50)
                            .IsUnicode(false);
        }
    }
    public class TDocumentFormatEntityMapping
    {
        public TDocumentFormatEntityMapping(EntityTypeBuilder<TDocumentFormat> entity)
        {

            entity.HasKey(e => e.DocumentId);

            entity.ToTable("t_document_format");

            entity.HasComment("This holds the various document required or to be uploaded");

            entity.Property(e => e.DocumentId).HasColumnName("document_id");

            entity.Property(e => e.CreatedBy)
                            .IsRequired()
                            .HasColumnName("created_by")
                            .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                            .HasColumnName("created_date")
                            .HasColumnType("datetime");

            entity.Property(e => e.Formats)
                            .IsRequired()
                            .HasColumnName("formats")
                            .HasMaxLength(500);

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
    public class TDocumentPurposeEntityMapping
    {
        public TDocumentPurposeEntityMapping(EntityTypeBuilder<TDocumentPurpose> entity)
        {
            entity.HasKey(e => e.PurposeId);

            entity.ToTable("t_document_purpose");

            entity.HasComment("This table hold the purpose of document");

            entity.Property(e => e.PurposeId).HasColumnName("purpose_id");

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

            entity.Property(e => e.Title)
                            .IsRequired()
                            .HasColumnName("title")
                            .HasMaxLength(100);
        }
    }
    public class TDocumentPurposeMappingEntityMapping
    {
        public TDocumentPurposeMappingEntityMapping(EntityTypeBuilder<TDocumentPurposeMapping> entity)
        {

            entity.HasKey(e => e.MappingId);

            entity.ToTable("t_document_purpose_mapping");

            entity.HasComment("This table maps document to its various purpose");

            entity.Property(e => e.MappingId).HasColumnName("mapping_id");

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

            entity.Property(e => e.LastModified)
                            .HasColumnName("last_modified")
                            .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                            .HasColumnName("modified_by")
                            .HasMaxLength(255);

            entity.Property(e => e.PurposeId).HasColumnName("purpose_id");

            entity.HasOne(d => d.Document)
                            .WithMany(p => p.TDocumentPurposeMapping)
                            .HasForeignKey(d => d.DocumentId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_t_document_purpose_mapping_t_document");

            entity.HasOne(d => d.Purpose)
                            .WithMany(p => p.TDocumentPurposeMapping)
                            .HasForeignKey(d => d.PurposeId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_t_document_purpose_mapping_t_document_purpose");
        }
    }
}
