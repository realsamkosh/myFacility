using myFacility.Models.Domains.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myFacility.Model.Domain.General.Messaging;

namespace myFacility.Infrastructure.EntityBuilder
{
    public class EmailTemplateEntityMapping
    {
        public EmailTemplateEntityMapping(EntityTypeBuilder<TEmailtemplate> entity)
        {
            entity.HasKey(e => e.EtemplateId)
                    .HasName("pk_emailtemplate");

            entity.ToTable("t_emailtemplate");

            entity.Property(e => e.EtemplateId).HasColumnName("etemplate_id");

            entity.Property(e => e.Body)
                .IsRequired()
                .HasColumnName("body");

            entity.Property(e => e.Code)
                .IsRequired()
                .HasColumnName("code")
                .HasMaxLength(10);

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
                .HasMaxLength(50);

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasColumnName("subject")
                .HasMaxLength(100);
        }
    }
    public class TSmsTemplateEntityMapping
    {
        public TSmsTemplateEntityMapping(EntityTypeBuilder<TSmstemplate> entity)
        {
            entity.HasKey(e => e.SmstemplateId)
                    .HasName("pk_smstemplate");

            entity.ToTable("t_smstemplate");

            entity.Property(e => e.SmstemplateId).HasColumnName("smstemplate_id");

            entity.Property(e => e.Code)
                .IsRequired()
                .HasColumnName("code")
                .HasMaxLength(10);

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

            entity.Property(e => e.Message)
                .IsRequired()
                .HasColumnName("message")
                .HasMaxLength(200);

            entity.Property(e => e.ModifiedBy)
                .HasColumnName("modified_by")
                .HasMaxLength(255);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(50);

            entity.Property(e => e.Sender)
                .IsRequired()
                .HasColumnName("sender")
                .HasMaxLength(11);
        }
    }
    public class TEmailLogEntityMapping
    {
        public TEmailLogEntityMapping(EntityTypeBuilder<TEmailLog> entity)
        {
            entity.HasKey(e => e.EmaillogId)
                    .HasName("pk_email_log");

            entity.ToTable("t_email_log");

            entity.HasComment("The various email logs on the system");

            entity.Property(e => e.EmaillogId).HasColumnName("emaillog_id");

            entity.Property(e => e.AttachmentLoc).HasColumnName("attachment_loc");

            entity.Property(e => e.Body)
                .IsRequired()
                .HasColumnName("body");

            entity.Property(e => e.CanSend)
                .IsRequired()
                .HasColumnName("can_send")
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Createdby)
                .IsRequired()
                .HasColumnName("createdby")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Createddate)
                .HasColumnName("createddate")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetosend)
                .HasColumnName("datetosend")
                .HasColumnType("datetime");

            entity.Property(e => e.EmailBcc)
                .HasColumnName("email_bcc")
                .IsUnicode(false);

            entity.Property(e => e.EmailCc)
                .HasColumnName("email_cc")
                .IsUnicode(false);

            entity.Property(e => e.FailedSending).HasColumnName("failed_sending");

            entity.Property(e => e.From)
                .IsRequired()
                .HasColumnName("from")
                .IsUnicode(false);

            entity.Property(e => e.HasAttachment).HasColumnName("has_attachment");

            entity.Property(e => e.LastFailed)
                .HasColumnName("last_failed")
                .HasColumnType("datetime");

            entity.Property(e => e.Lastmodified)
                .HasColumnName("lastmodified")
                .HasColumnType("datetime");

            entity.Property(e => e.Modifiedby)
                .HasColumnName("modifiedby")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Sendimmediately).HasColumnName("sendimmediately");

            entity.Property(e => e.Sent).HasColumnName("sent");

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasColumnName("subject")
                .HasMaxLength(300);

            entity.Property(e => e.To)
                .IsRequired()
                .HasColumnName("to")
                .IsUnicode(false);
        }
    }
    public class TSmsLogEntityMapping
    {
        public TSmsLogEntityMapping(EntityTypeBuilder<TSmsLog> entity)
        {
            entity.HasKey(e => e.SmslogId)
                        .HasName("pk_sms_log");

            entity.ToTable("t_sms_log");

            entity.Property(e => e.SmslogId).HasColumnName("smslog_id");

            entity.Property(e => e.CanSend)
                .IsRequired()
                .HasColumnName("can_send")
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Createdby)
                .IsRequired()
                .HasColumnName("createdby")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Createddate)
                .HasColumnName("createddate")
                .HasColumnType("datetime");

            entity.Property(e => e.Datetosend)
                .HasColumnName("datetosend")
                .HasColumnType("datetime");

            entity.Property(e => e.FailedSending).HasColumnName("failed_sending");
            entity.Property(e => e.ErrorMessage).HasColumnName("error_message");

            entity.Property(e => e.From)
                .IsRequired()
                .HasColumnName("from")
                .IsUnicode(false);

            entity.Property(e => e.LastFailed)
                .HasColumnName("last_failed")
                .HasColumnType("datetime");

            entity.Property(e => e.Lastmodified)
                .HasColumnName("lastmodified")
                .HasColumnType("datetime");

            entity.Property(e => e.Message)
                .IsRequired()
                .HasColumnName("message");

            entity.Property(e => e.Modifiedby)
                .HasColumnName("modifiedby")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Sendimmediately).HasColumnName("sendimmediately");

            entity.Property(e => e.Sent).HasColumnName("sent");

            entity.Property(e => e.Subject)
                .IsRequired()
                .HasColumnName("subject")
                .HasMaxLength(300);

            entity.Property(e => e.To)
                .IsRequired()
                .HasColumnName("to")
                .IsUnicode(false);
        }
    }
    public class TEmailTokenEntityMapping
    {
        public TEmailTokenEntityMapping(EntityTypeBuilder<TEmailToken> entity)
        {

            entity.HasKey(e => e.EmailtokenId)
                    .HasName("pk_t_email_token");

            entity.ToTable("t_email_token");

            entity.HasComment("This table holds the tokens that can be used wile composing email");

            entity.Property(e => e.EmailtokenId).HasColumnName("emailtoken_id");

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by")
                .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime");

            entity.Property(e => e.EmailToken)
                .IsRequired()
                .HasColumnName("email_token")
                .HasMaxLength(100);

            entity.Property(e => e.IsActive).HasColumnName("is_active");

            entity.Property(e => e.LastModified)
                .HasColumnName("last_modified")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                .HasColumnName("modified_by")
                .HasMaxLength(255);

            entity.Property(e => e.PreviewName)
                .IsRequired()
                .HasColumnName("preview_name")
                .HasMaxLength(200);
        }
    }
    public class TSmsTokenEntityMapping
    {
        public TSmsTokenEntityMapping(EntityTypeBuilder<TSmsToken> entity)
        {

            entity.HasKey(e => e.SmstokenId);

            entity.ToTable("t_sms_token");

            entity.Property(e => e.SmstokenId).HasColumnName("smstoken_id");

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by")
                .HasMaxLength(255);

            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasColumnName("is_active");

            entity.Property(e => e.LastModified)
                .HasColumnName("last_modified")
                .HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                .HasColumnName("modified_by")
                .HasMaxLength(255);

            entity.Property(e => e.PreviewName)
                .IsRequired()
                .HasColumnName("preview_name")
                .HasMaxLength(200);

            entity.Property(e => e.SmsToken)
                .IsRequired()
                .HasColumnName("sms_token")
                .HasMaxLength(100);
        }
    }
    //public class TSmsProviderHolderEntityMapping
    //{
    //    public TSmsProviderHolderEntityMapping(EntityTypeBuilder<TSmsProviderHolder> entity)
    //    {
    //        entity.HasKey(e => e.CredId);

    //        entity.ToTable("t_sms_provider_holder");

    //        entity.Property(e => e.CredId).HasColumnName("cred_id");

    //        entity.Property(e => e.CredentialPlaceholder)
    //                        .IsRequired()
    //                        .HasColumnName("credential_placeholder")
    //                        .HasMaxLength(100);
    //    }
    //}
}
