using Microsoft.EntityFrameworkCore;
using myFacility.Models.Domains.Award;
using myFacility.Models.Domains.Document;
using myFacility.Models.Domains.Invoice;
using myFacility.Models.Domains.Job;
using myFacility.Models.Domains.KPI;
using myFacility.Models.Domains.Period;
using myFacility.Models.Domains.Quote;
using myFacility.Models.Domains.Vendor;
using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Infrastructure
{
    public class myFacilityDbContext : DbContext
    {
        public myFacilityDbContext()
        {
        }

        public myFacilityDbContext(DbContextOptions<myFacilityDbContext> options)
            : base(options)
        {
        }

        #region Job Award
        public virtual DbSet<TAwardedJobStatus> TAwardedJobStatus { get; set; }
        public virtual DbSet<TAwardedJobs> TAwardedJobs { get; set; }
        #endregion

        #region Document
        public virtual DbSet<TDocument> TDocument { get; set; }
        public virtual DbSet<TDocumentFormat> TDocumentFormat { get; set; }
        public virtual DbSet<TDocumentPurpose> TDocumentPurpose { get; set; }
        public virtual DbSet<TDocumentPurposeMapping> TDocumentPurposeMapping { get; set; }
        #endregion

        #region Invoice
        public virtual DbSet<TInvoice> TInvoice { get; set; }
        public virtual DbSet<TInvoiceDetails> TInvoiceDetails { get; set; }
        public virtual DbSet<TInvoiceStatus> TInvoiceStatus { get; set; }
        public virtual DbSet<TInvoiceType> TInvoiceType { get; set; }
        #endregion

        #region Job
        public virtual DbSet<TJob> TJob { get; set; }
        public virtual DbSet<TJobCategory> TJobCategory { get; set; }
        public virtual DbSet<TJobCategoryDocument> TJobCategoryDocument { get; set; }
        public virtual DbSet<TJobJobcategoryMapping> TJobJobcategoryMapping { get; set; }
        public virtual DbSet<TJobType> TJobType { get; set; }
        public virtual DbSet<TLowValueJob> TLowValueJob { get; set; }
        #endregion

        #region KPI
        public virtual DbSet<TKpiProfile> TKpiProfile { get; set; }
        public virtual DbSet<TKpiType> TKpiType { get; set; }
        #endregion

        public virtual DbSet<TPeriodSchedule> TPeriodSchedule { get; set; }
        public virtual DbSet<TPeriodType> TPeriodType { get; set; }
        public virtual DbSet<TQuote> TQuote { get; set; }
        public virtual DbSet<TQuoteKpiLog> TQuoteKpiLog { get; set; }
        public virtual DbSet<TVendorAccountStatus> TVendorAccountStatus { get; set; }
        public virtual DbSet<TVendorAppraisal> TVendorAppraisal { get; set; }
        public virtual DbSet<TVendorAppraisalKpi> TVendorAppraisalKpi { get; set; }
        public virtual DbSet<TVendorBusinessRegistration> TVendorBusinessRegistration { get; set; }
        public virtual DbSet<TVendorJobCategoryDoc> TVendorJobCategoryDoc { get; set; }
        public virtual DbSet<TVendorJobCategoryMapping> TVendorJobCategoryMapping { get; set; }
        public virtual DbSet<TVendorPersonalRegistration> TVendorPersonalRegistration { get; set; }
        public virtual DbSet<TVendorRejectionHistory> TVendorRejectionHistory { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Payment
            new TAwardedJobStatusEntityMapping(builder.Entity<TAwardedJobStatus>());
            new TAwardedJobsEntityMapping(builder.Entity<TAwardedJobs>());

            new TDocumentEntityMapping(builder.Entity<TDocument>());
            new TDocumentFormatEntityMapping(builder.Entity<TDocumentFormat>());
            new TDocumentPurposeEntityMapping(builder.Entity<TDocumentPurpose>());
            new TDocumentPurposeMappingEntityMapping(builder.Entity<TDocumentPurposeMapping>());

            new TInvoiceEntityMapping(builder.Entity<TInvoice>());
            new TInvoiceDetailsEntityMapping(builder.Entity<TInvoiceDetails>());
            new TInvoiceStatusEntityMapping(builder.Entity<TInvoiceStatus>());
            new TInvoiceTypeEntityMapping(builder.Entity<TInvoiceType>());

            new TJobEntityMapping(builder.Entity<TJob>());
            new TJobCategoryEntityMapping(builder.Entity<TJobCategory>());
            new TJobCategoryDocumentEntityMapping(builder.Entity<TJobCategoryDocument>());
            new TJobJobcategoryMappingEntityMapping(builder.Entity<TJobJobcategoryMapping>());
            new TJobTypeEntityMapping(builder.Entity<TJobType>());
            new TLowValueJobEntityMapping(builder.Entity<TLowValueJob>());

            new TKpiProfileEntityMapping(builder.Entity<TKpiProfile>());
            new TKpiTypeEntityMapping(builder.Entity<TKpiType>());

            new TQuoteEntityMapping(builder.Entity<TQuote>());
            new TQuoteKpiLogEntityMapping(builder.Entity<TQuoteKpiLog>());

            new TVendorAccountStatusEntityMapping(builder.Entity<TVendorAccountStatus>());
            new TVendorAppraisalEntityMapping(builder.Entity<TVendorAppraisal>());
            new TVendorAppraisalKpiEntityMapping(builder.Entity<TVendorAppraisalKpi>());
            new TVendorBusinessRegistrationEntityMapping(builder.Entity<TVendorBusinessRegistration>());
            new TVendorJobCategoryDocEntityMapping(builder.Entity<TVendorJobCategoryDoc>());
            new TVendorJobCategoryMappingEntityMapping(builder.Entity<TVendorJobCategoryMapping>());
            new TVendorPersonalRegistrationEntityMapping(builder.Entity<TVendorPersonalRegistration>());
            new TVendorRejectionHistoryEntityMapping(builder.Entity<TVendorRejectionHistory>());

            new TPeriodScheduleEntityMapping(builder.Entity<TPeriodSchedule>());
            new TPeriodTypeEntityMapping(builder.Entity<TPeriodType>());
            
        }
    }
}
