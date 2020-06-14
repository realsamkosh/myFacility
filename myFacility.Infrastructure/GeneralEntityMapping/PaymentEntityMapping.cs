using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using myFacility.Domain.General.Payment;

namespace myFacility.Infrastructure.GeneralEntityMapping
{
    public class PaymentEntityMapping
    {
        public PaymentEntityMapping(EntityTypeBuilder<TTransactionLog> entity)
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("t_transaction_log");

            entity.HasComment("holds all form of transactions i.e verified and non verified");

            entity.Property(e => e.TransactionId)
                .HasColumnName("transaction_id");

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by")
                .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasColumnName("is_active");

            entity.Property(e => e.Iscanceled).HasColumnName("iscanceled");

            entity.Property(e => e.LastModified)
                .HasColumnName("last_modified")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                .HasColumnName("modified_by")
                .HasMaxLength(255);

            entity.Property(e => e.PytConfirmed).HasColumnName("pyt_confirmed");

            entity.Property(e => e.StagingId).HasColumnName("staging_id");

            entity.Property(e => e.TrxRef)
                .IsRequired()
                .HasColumnName("trx_ref");

            entity.Property(e => e.TrxDesc)
                .IsRequired()
                .HasColumnName("trx_desc")
                .HasColumnType("text");

            entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .HasMaxLength(450);
        }
    }
}
