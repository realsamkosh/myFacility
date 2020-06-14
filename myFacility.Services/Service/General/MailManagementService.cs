using myFacility.Infrastructure;
using myFacility.Models.Domains.Messaging;
using myFacility.Services.Contract;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.ImageUtility;
using myFacility.Utilities.MailUtility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using myFacility.Model.DataObjects.General.Mail;

namespace myFacility.Services.Handler
{
    public class MailManagementService : IMailManagementService
    {
        private readonly IConfiguration _config;
        private readonly IServiceScopeFactory _serviceScope;
        private readonly ILogger<MailManagementService> _logger;
        private readonly IAuthUser _authuser;

        public MailManagementService(IConfiguration config, IServiceScopeFactory serviceScope, ILogger<MailManagementService> logger,
            IAuthUser authuser)
        {
            _config = config;
            _serviceScope = serviceScope;
            _logger = logger;
            _authuser = authuser;
        }
        #region Email Credentials
        public MailSettings GetEmailCredentials()
        {
            try
            {
                var appSettingsSection = _config.GetSection("MailSettings");
                var appSettings = appSettingsSection.Get<MailSettings>();
                return appSettings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        #endregion

        #region Email Template
        public async Task<TEmailtemplate> GetMerchantEmailTemplate(long merchantid, string code)
        {
            try
            {
                using (var scope = _serviceScope.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                    return await dbcontext.TEmailtemplate
                        .Select(x => new TEmailtemplate
                        {
                            Subject = x.Subject,
                            EtemplateId = x.EtemplateId,
                            Body = x.Body,
                            Code = x.Code,
                            Name = x.Name
                        }).FirstOrDefaultAsync(x => x.Code == code && x.IsActive == true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<TEmailtemplate> GetSystemEmailTemplate(string code)
        {
            try
            {
                using (var scope = _serviceScope.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                    return await dbcontext.TEmailtemplate.Select(x => new TEmailtemplate
                    {
                        Body = x.Body,
                        Code = x.Code,
                        Subject = x.Subject,
                        Name = x.Name
                    }).FirstOrDefaultAsync(x => x.Code == code);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        #endregion

        #region Mail Logging
        public async Task LogBatchEmail(List<TEmailLog> eMailLogs)
        {
            try
            {
                using (var scope = _serviceScope.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                    dbcontext.TEmailLog.AddRange(eMailLogs);
                    await dbcontext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task LogEmail(TEmailLog emailLog)
        {
            try
            {
                using (var scope = _serviceScope.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                    emailLog.LastFailed = null;
                    emailLog.Lastmodified = null;
                    emailLog.Createddate = DateTime.Now;
                    dbcontext.TEmailLog.Add(emailLog);
                    await dbcontext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        #endregion

        #region Welcome Email Services
        public async Task<int> SendWelcomeEmail(string emailadd, string username, string password, string loginurl, string fullname)
        {
            //Fetch Subject from DB based on supplied template ID
            var fetchtemplate = await GetSystemEmailTemplate("CONF_EML");

            //Replace placeholder in template with message sent
            string MessageBody = fetchtemplate.Body.Replace("[Username]", username);
            MessageBody = MessageBody.Replace("[Fullname]", fullname);
            MessageBody = MessageBody.Replace("[Username]", username);
            MessageBody = MessageBody.Replace("[LoginURL]", loginurl);
            //String SafeMessageBody = MessageBody.Replace(password, "XXXXXXXXXXX");

            var cred = GetEmailCredentials();
            try
            {
                MailMessage NewEmail = new MailMessage(cred.SenderEmail, emailadd, fetchtemplate.Subject, MessageBody)
                {
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    From = new MailAddress(cred.SenderEmail, cred.SenderName)
                };

                // Add a carbon copy recipient.
                //MailAddress copy = new MailAddressCollection(cred.CC,);

                using SmtpClient smtpMail = new SmtpClient();
                smtpMail.Host = cred.SMTPHost;
                smtpMail.Port = cred.SMTPPort;
                smtpMail.EnableSsl = cred.EnableSsl;
                smtpMail.Credentials = new NetworkCredential(cred.SenderEmail, cred.Password);

                //var x = (!string.IsNullOrEmpty(cred.CC)) ? NewEmail.CC.Add(cred.CC) : null;
                await smtpMail.SendMailAsync(NewEmail);
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = fetchtemplate.Name,
                    From = cred.SenderEmail,
                    Sent = true,
                    To = emailadd,
                    Subject = fetchtemplate.Subject,
                    Body = MessageBody,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = "Anonymous",
                };
                await LogEmail(newrec);
            }
            catch (Exception ex)
            {
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = fetchtemplate.Name,
                    From = cred.SenderEmail,
                    FailedSending = true,
                    LastFailed = DateTime.Now,
                    Sent = false,
                    To = emailadd,
                    Subject = fetchtemplate.Subject,
                    Body = MessageBody,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = "Anonymous",
                };
                await LogEmail(newrec);
                _logger.LogError(ex.Message);
                return 1001;
            }
            return 0;
        }
        #endregion

        #region Application Live Email Services
        public async Task<int> SendApplicationLiveEmail(string emailadd, string loginurl, string firstname, string merchantFullname)
        {
            //Fetch Subject from DB based on supplied template ID
            var fetchtemplate = await GetSystemEmailTemplate("APPL_LIVE");

            //Replace placeholder in template with message sent
            string MessageBody = fetchtemplate.Body.Replace("[firstname]", firstname);
            MessageBody = MessageBody.Replace("[merchant_fullname]", merchantFullname);
            MessageBody = MessageBody.Replace("[LoginURL]", loginurl);
            //String SafeMessageBody = MessageBody.Replace(password, "XXXXXXXXXXX");

            var cred = GetEmailCredentials();
            try
            {
                MailMessage NewEmail = new MailMessage(cred.SenderEmail, emailadd, fetchtemplate.Subject, MessageBody)
                {
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    From = new MailAddress(cred.SenderEmail, cred.SenderName)
                };

                // Add a carbon copy recipient.
                //MailAddress copy = new MailAddressCollection(cred.CC,);

                using SmtpClient smtpMail = new SmtpClient();
                smtpMail.Host = cred.SMTPHost;
                smtpMail.Port = cred.SMTPPort;
                smtpMail.EnableSsl = cred.EnableSsl;
                smtpMail.Credentials = new NetworkCredential(cred.SenderEmail, cred.Password);

                //var x = (!string.IsNullOrEmpty(cred.CC)) ? NewEmail.CC.Add(cred.CC) : null;
                await smtpMail.SendMailAsync(NewEmail);
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = fetchtemplate.Name,
                    From = cred.SenderEmail,
                    Sent = true,
                    To = emailadd,
                    Subject = fetchtemplate.Subject,
                    Body = MessageBody,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = "Anonymous",
                };
                await LogEmail(newrec);
            }
            catch (Exception ex)
            {
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = fetchtemplate.Name,
                    From = cred.SenderEmail,
                    FailedSending = true,
                    LastFailed = DateTime.Now,
                    Sent = false,
                    To = emailadd,
                    Subject = fetchtemplate.Subject,
                    Body = MessageBody,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = "Anonymous",
                };
                await LogEmail(newrec);
                _logger.LogError(ex.Message);
                return 1001;
            }
            return 0;
        }
        #endregion

        #region Generic Message Sender
        public async Task<int> SendMail(string emailadd, string subject, string body, string name, long merchantid)
        {
            var cred = GetEmailCredentials();
            try
            {
                MailMessage NewEmail = new MailMessage(cred.SenderEmail, emailadd, subject, body)
                {
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    From = new MailAddress(cred.SenderEmail, cred.SenderName)
                };

                // Add a carbon copy recipient.
                //MailAddress copy = new MailAddressCollection(cred.CC,);

                using (SmtpClient smtpMail = new SmtpClient())
                {
                    smtpMail.Host = cred.SMTPHost;
                    smtpMail.Port = cred.SMTPPort;
                    smtpMail.EnableSsl = cred.EnableSsl;
                    smtpMail.Credentials = new NetworkCredential(cred.SenderEmail, cred.Password);

                    //var x = (!string.IsNullOrEmpty(cred.CC)) ? NewEmail.CC.Add(cred.CC) : null;
                    await smtpMail.SendMailAsync(NewEmail);
                    TEmailLog newrec = new TEmailLog
                    {
                        Sendimmediately = true,
                        Datetosend = DateTime.Now,
                        CanSend = true,
                        Name = name,
                        From = cred.SenderEmail,
                        Sent = true,
                        To = emailadd,
                        Subject = subject,
                        Body = body,
                        EmailCc = cred.CC,
                        EmailBcc = cred.BCC,
                        Createdby = "System",
                    };
                    await LogEmail(newrec);
                }
            }
            catch (Exception ex)
            {
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = name,
                    From = cred.SenderEmail,
                    FailedSending = true,
                    LastFailed = DateTime.Now,
                    Sent = false,
                    To = emailadd,
                    Subject = subject,
                    Body = body,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = "System",
                };
                await LogEmail(newrec);
                _logger.LogError(ex.Message);
                return 1001;
            }
            return 0;
        }

        public async Task<bool> SendMailAsync(TEmailLog emailLog)
        {
            try
            {
                MailMessage mMessage = new MailMessage();
                var cred = GetEmailCredentials();
                mMessage.To.Add(emailLog.To);
                mMessage.Subject = emailLog.Subject;
                if (emailLog.From.Contains(">"))
                {
                    mMessage.From = new MailAddress(emailLog.From);
                }
                else
                {
                    mMessage.From = new MailAddress($"{emailLog.Name} <{emailLog.From}>");
                }

                //Check if it has Carbon Copy
                if (!string.IsNullOrEmpty(emailLog.EmailCc))
                {
                    string[] CCId = emailLog.EmailCc.Split(',');

                    foreach (string CCEmail in CCId)
                    {
                        mMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    }
                }

                mMessage.Body = emailLog.Body;
                mMessage.Priority = MailPriority.High;
                mMessage.IsBodyHtml = true;

                if (emailLog.HasAttachment)
                {
                    var attachments = emailLog.AttachmentLoc.Split(',');
                    foreach (var attt in attachments)
                    {
                        var leAttachment = Path.Combine(Directory.GetCurrentDirectory(), attt);
                        mMessage.Attachments.Add(new Attachment(leAttachment));
                    }
                }

                using (SmtpClient smtpMail = new SmtpClient())
                {
                    smtpMail.Host = cred.SMTPHost;
                    smtpMail.Port = Convert.ToInt32(cred.SMTPPort);
                    smtpMail.EnableSsl = true;
                    smtpMail.Credentials = new NetworkCredential(cred.SenderEmail, cred.Password);

                    await smtpMail.SendMailAsync(mMessage);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task SendBatchMailAsync()
        {
            try
            {
                using (var scope = _serviceScope.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                    var unsentEmails = await dbcontext.TEmailLog.Where(x => x.Sent == false && x.Sendimmediately
                    && (x.Datetosend.Date == DateTime.Now.Date || DateTime.Now.Date > x.Datetosend.Date)).ToListAsync();

                    foreach (var email in unsentEmails)
                    {
                        email.Sent = await SendMailAsync(email);
                        dbcontext.Update(email);
                        await dbcontext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Password Reset Mail Service
        public async Task<int> SendPasswordResetEmail(string emailadd, string username, string reseturl, long merchantid)
        {
            //Fetch Subject from DB based on supplied template ID
            var fetchtemplate = await GetSystemEmailTemplate("RST_PWD");

            //Replace placeholder in template with message sent
            string MessageBody = fetchtemplate.Body.Replace("[Username]", username);
            MessageBody = MessageBody.Replace("[ResetURL]", reseturl);
            MessageBody = MessageBody.Replace("[TWITTERICON]", Base64Images.TwitterIcon);
            MessageBody = MessageBody.Replace("[FBICON]", Base64Images.FacebookIcon);
            MessageBody = MessageBody.Replace("[Username]", username);
            MessageBody = MessageBody.Replace("[TWITTERURL]", _config.GetSection("TwitterUrl").Value);
            MessageBody = MessageBody.Replace("[FBURL]", _config.GetSection("FacebookUrl").Value);

            var cred = GetEmailCredentials();
            try
            {
                MailMessage NewEmail = new MailMessage(cred.SenderEmail, emailadd, fetchtemplate.Subject, MessageBody)
                {
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    From = new MailAddress(cred.SenderEmail, cred.SenderName)
                };

                // Add a carbon copy recipient.
                //MailAddress copy = new MailAddressCollection(cred.CC,);

                using (SmtpClient smtpMail = new SmtpClient())
                {
                    smtpMail.Host = cred.SMTPHost;
                    smtpMail.Port = cred.SMTPPort;
                    smtpMail.EnableSsl = cred.EnableSsl;
                    smtpMail.Credentials = new NetworkCredential(cred.SenderEmail, cred.Password);

                    await smtpMail.SendMailAsync(NewEmail);
                    TEmailLog newrec = new TEmailLog
                    {
                        Sendimmediately = true,
                        Datetosend = DateTime.Now,
                        CanSend = true,
                        Name = fetchtemplate.Name,
                        From = cred.SenderEmail,
                        Sent = true,
                        To = emailadd,
                        Subject = fetchtemplate.Subject,
                        Body = MessageBody,
                        EmailCc = cred.CC,
                        EmailBcc = cred.BCC,
                        Createdby = username,
                    };
                    await LogEmail(newrec);
                }
            }
            catch (Exception ex)
            {
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = fetchtemplate.Name,
                    From = cred.SenderEmail,
                    FailedSending = true,
                    LastFailed = DateTime.Now,
                    Sent = false,
                    To = emailadd,
                    Subject = fetchtemplate.Subject,
                    Body = MessageBody,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = username,
                };
                await LogEmail(newrec);
                _logger.LogError(ex.Message);
                return 1001;
            }
            return 0;
        }
        #endregion

        #region Appointment Notification Services
        public async Task<int> SendAppointmentBooking(PatientAppointmentMailViewModel model)
        {
            //Fetch Subject from DB based on supplied template ID
            var fetchtemplate = await GetSystemEmailTemplate("PATAPT_BKN");

            //Replace placeholder in template with message sent
            string MessageBody = fetchtemplate.Body.Replace("[PatientName]", model.patientname);
            MessageBody = MessageBody.Replace("[ScheduledDate]", model.scheduleddate);
            MessageBody = MessageBody.Replace("[DoctorName]", model.doctorname);
            MessageBody = MessageBody.Replace("[TimeSlot]", model.timeslot);
            MessageBody = MessageBody.Replace("[HospitalName]", model.hospitalname);
            MessageBody = MessageBody.Replace("[HospitalContact]", model.hospitalcontact);
            MessageBody = MessageBody.Replace("[HospitalAddress]", model.hospitaladdress);

            var cred = GetEmailCredentials();
            try
            {
                MailMessage NewEmail = new MailMessage(cred.SenderEmail, model.emailaddress, fetchtemplate.Subject, MessageBody)
                {
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    From = new MailAddress(cred.SenderEmail, cred.SenderName)
                };

                // Add a carbon copy recipient.
                //MailAddress copy = new MailAddressCollection(cred.CC,);

                using (SmtpClient smtpMail = new SmtpClient())
                {
                    smtpMail.Host = cred.SMTPHost;
                    smtpMail.Port = cred.SMTPPort;
                    smtpMail.EnableSsl = cred.EnableSsl;
                    smtpMail.Credentials = new NetworkCredential(cred.SenderEmail, cred.Password);

                    //var x = (!string.IsNullOrEmpty(cred.CC)) ? NewEmail.CC.Add(cred.CC) : null;
                    await smtpMail.SendMailAsync(NewEmail);
                    TEmailLog newrec = new TEmailLog
                    {
                        Sendimmediately = true,
                        Datetosend = DateTime.Now,
                        CanSend = true,
                        Name = fetchtemplate.Name,
                        From = cred.SenderEmail,
                        Sent = true,
                        To = model.emailaddress,
                        Subject = fetchtemplate.Subject,
                        Body = MessageBody,
                        EmailCc = cred.CC,
                        EmailBcc = cred.BCC,
                        Createdby = "Anonymous",
                    };
                    await LogEmail(newrec);
                }
            }
            catch (Exception ex)
            {
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = fetchtemplate.Name,
                    From = cred.SenderEmail,
                    FailedSending = true,
                    LastFailed = DateTime.Now,
                    Sent = false,
                    To = model.emailaddress,
                    Subject = fetchtemplate.Subject,
                    Body = MessageBody,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = "Anonymous",
                };
                await LogEmail(newrec);
                _logger.LogError(ex.Message);
                return 1001;
            }
            return 0;
        }

        public async Task<int> SendDoctorAppointmentBooking(PatientAppointmentMailViewModel model)
        {
            //Fetch Subject from DB based on supplied template ID
            var fetchtemplate = await GetSystemEmailTemplate("DOCAPT_BKN");

            //Replace placeholder in template with message sent
            string MessageBody = fetchtemplate.Body.Replace("[PatientName]", model.patientname);
            MessageBody = MessageBody.Replace("[ScheduledDate]", model.scheduleddate);
            MessageBody = MessageBody.Replace("[DoctorName]", model.doctorname);
            MessageBody = MessageBody.Replace("[PatientPhoneNumber]", model.patientphonenumber);
            MessageBody = MessageBody.Replace("[TimeSlot]", model.timeslot);
            MessageBody = MessageBody.Replace("[HospitalName]", model.hospitalname);
            MessageBody = MessageBody.Replace("[HospitalContact]", model.hospitalcontact);
            MessageBody = MessageBody.Replace("[HospitalAddress]", model.hospitaladdress);

            var guid_string = Guid.NewGuid().ToString();
            var cred = GetEmailCredentials();
            try
            {
                MailMessage NewEmail = new MailMessage(cred.SenderEmail, model.emailaddress, fetchtemplate.Subject, MessageBody)
                {
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    From = new MailAddress(cred.SenderEmail, cred.SenderName)
                };

                // Add a carbon copy recipient.
                //MailAddress copy = new MailAddressCollection(cred.CC,);

                using (SmtpClient smtpMail = new SmtpClient())
                {
                    smtpMail.Host = cred.SMTPHost;
                    smtpMail.Port = cred.SMTPPort;
                    smtpMail.EnableSsl = cred.EnableSsl;
                    smtpMail.Credentials = new NetworkCredential(cred.SenderEmail, cred.Password);

                    //var x = (!string.IsNullOrEmpty(cred.CC)) ? NewEmail.CC.Add(cred.CC) : null;
                    await smtpMail.SendMailAsync(NewEmail);
                    TEmailLog newrec = new TEmailLog
                    {
                        Sendimmediately = true,
                        Datetosend = DateTime.Now,
                        CanSend = true,
                        Name = fetchtemplate.Name,
                        From = cred.SenderEmail,
                        Sent = true,
                        To = model.emailaddress,
                        Subject = fetchtemplate.Subject,
                        Body = MessageBody,
                        EmailCc = cred.CC,
                        EmailBcc = cred.BCC,
                        Createdby = "Anonymous",
                    };
                    await LogEmail(newrec);
                }
            }
            catch (Exception ex)
            {
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = fetchtemplate.Name,
                    From = cred.SenderEmail,
                    FailedSending = true,
                    LastFailed = DateTime.Now,
                    Sent = false,
                    To = model.emailaddress,
                    Subject = fetchtemplate.Subject,
                    Body = MessageBody,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = "Anonymous",
                };
                await LogEmail(newrec);
                _logger.LogError(ex.Message);
                return 1001;
            }
            return 0;
        }
        #endregion

        #region Zoom Meeting Notification Email Services
        public async Task<int> NotifyDoctorMeetingLink(ZoomMeetingNotificationDTO model)
        {
            //Fetch Subject from DB based on supplied template ID
            var fetchtemplate = await GetSystemEmailTemplate("ZM_MTN");

            //Replace placeholder in template with message sent
            string MessageBody = fetchtemplate.Body.Replace("[DoctorName]", model.doctorname);
            MessageBody = MessageBody.Replace("[PatientName]", model.patientname);
            MessageBody = MessageBody.Replace("[MeetingURL]", model.meetingurl);

            var cred = GetEmailCredentials();
            try
            {
                MailMessage NewEmail = new MailMessage(cred.SenderEmail, model.doctorsemail, fetchtemplate.Subject, MessageBody)
                {
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    From = new MailAddress(cred.SenderEmail, cred.SenderName)
                };

                // Add a carbon copy recipient.
                //MailAddress copy = new MailAddressCollection(cred.CC,);

                using SmtpClient smtpMail = new SmtpClient();
                smtpMail.Host = cred.SMTPHost;
                smtpMail.Port = cred.SMTPPort;
                smtpMail.EnableSsl = cred.EnableSsl;
                smtpMail.Credentials = new NetworkCredential(cred.SenderEmail, cred.Password);

                //var x = (!string.IsNullOrEmpty(cred.CC)) ? NewEmail.CC.Add(cred.CC) : null;
                await smtpMail.SendMailAsync(NewEmail);
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = fetchtemplate.Name,
                    From = cred.SenderEmail,
                    Sent = true,
                    To = model.doctorsemail,
                    Subject = fetchtemplate.Subject,
                    Body = MessageBody,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = "Anonymous",
                };
                await LogEmail(newrec);
            }
            catch (Exception ex)
            {
                TEmailLog newrec = new TEmailLog
                {
                    Sendimmediately = true,
                    Datetosend = DateTime.Now,
                    CanSend = true,
                    Name = fetchtemplate.Name,
                    From = cred.SenderEmail,
                    FailedSending = true,
                    LastFailed = DateTime.Now,
                    Sent = false,
                    To = model.doctorsemail,
                    Subject = fetchtemplate.Subject,
                    Body = MessageBody,
                    EmailCc = cred.CC,
                    EmailBcc = cred.BCC,
                    Createdby = "Anonymous",
                };
                await LogEmail(newrec);
                _logger.LogError(ex.Message);
                return 1001;
            }
            return 0;
        }
        #endregion
    }
}
