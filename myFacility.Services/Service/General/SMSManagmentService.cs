using Genesys.SMS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using myFacility.Infrastructure;
using myFacility.Model.Domain.General.Messaging;
using myFacility.Models.Domains.Messaging;
using myFacility.Services.Contract;
using myFacility.SMS.Core.TwilioSMS;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.SMSUtility;
using myFacility.Utilities.SMSUtility.Nexmo.Model;
using myFacility.Utilities.SMSUtility.NexmoSMS;
using myFacility.Utilities.SMSUtility.SMSProviderNG;
using myFacility.Utilities.SMSUtility.SMSProviderNG.Model;
using myFacility.Utilities.SMSUtility.TwilioSMS.Model;

namespace myFacility.Services.Handler
{
    public class SMSManagmentService : ISMSManagementService
    {
        private readonly IConfiguration _config;
        private readonly IServiceScopeFactory _serviceScope;
        private readonly ILogger<SMSManagmentService> _logger;
        private readonly IAuthUser _authuser;
        public SMSManagmentService(IConfiguration config, IServiceScopeFactory serviceScope, ILogger<SMSManagmentService> logger,
            IAuthUser authuser)
        {
            _config = config;
            _serviceScope = serviceScope;
            _logger = logger;
            _authuser = authuser;
        }

        #region SMS Credentials
        public SMSSettings GetSmsCredentials()
        {
            try
            {
                var appSettingsSection = _config.GetSection("SMSSettings");
                var appSettings = appSettingsSection.Get<SMSSettings>();
                return appSettings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        #endregion

        #region Appointment Notifications
        public async Task SendAppointmentBookingSms(long merchantid, string fullname, string hospital, string phonenumber, string appointdate, string specialist)
        {
            try
            {
                using var scope = _serviceScope.CreateScope();
                var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                //Get SMS Template
                var fetchtemplate = await GetSystemEditableSMSTemplate("APT_BKN");

                //Replace Template Place Holders
                fetchtemplate.Message.Replace("[FirstName]", fullname);
                fetchtemplate.Message.Replace("[AppointmentDate]", appointdate);
                fetchtemplate.Message.Replace("[MerchantName]", hospital);

                //Fetch the SMS Credentials
                var cred = SendGeneralSms(fetchtemplate.Message, fetchtemplate.Sender, phonenumber);
                return;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendAppointmentReschedulingSms(long merchantid, string fullname, string hospital,
            string phonenumber, string nextdate, string appoiintmentdate, string specialist = null, string clinic = null)
        {
            try
            {
                using (var scope = _serviceScope.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                    //Get SMS Template
                    var fetchtemplate = await GetSystemEditableSMSTemplate("APT_RSC");

                    //Replace Template Place Holders
                    fetchtemplate.Message.Replace("[FirstName]", fullname);
                    fetchtemplate.Message.Replace("[AppointmentDate]", appoiintmentdate);
                    fetchtemplate.Message.Replace("[NewDate]", nextdate);
                    fetchtemplate.Message.Replace("[MerchantName]", hospital);

                    //Fetch the SMS Credentials
                    var cred = SendGeneralSms(fetchtemplate.Message, fetchtemplate.Sender, phonenumber);
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendAppointmentCancelationSms(long merchantid, string fullname, string hospital, string phonenumber, string appointmentdate)
        {
            try
            {
                using (var scope = _serviceScope.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                    //Get SMS Template
                    var fetchtemplate = await GetSystemEditableSMSTemplate("APT_CSL");

                    //Replace Template Place Holders
                    fetchtemplate.Message.Replace("[FirstName]", fullname);
                    fetchtemplate.Message.Replace("[AppointmentDate]", appointmentdate);
                    fetchtemplate.Message.Replace("[MerchantName]", hospital);

                    //Fetch the SMS Credentials
                    var cred = SendGeneralSms(fetchtemplate.Message, fetchtemplate.Sender, phonenumber);
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task SendAppointmentRemindersSMS()
        //{
        //    try
        //    {
        //        using (var scope = _serviceScope.CreateScope())
        //        {
        //            var dbcontext = scope.ServiceProvider.GetService<authDbContext>();
        //            // Get Current Time
        //            var currentDate = DateTime.Now;


        //            //Get Merchant Reminder Intervals
        //            var intervals = await dbcontext.TGnsysMerchantReminderIntervals
        //                .Where(x => x.MerchantId == item.MerchantId).ToListAsync();

        //            //Get the Branches
        //            var merch = new SqlParameter("MerchantId", item.MerchantId);
        //            string sqlQuery = "EXEC [dbo].[appointment_sp_reminderlist] @MerchantId";
        //            var result = await dbcontext.Query<AppointmentReminderList>()
        //                                        .FromSql(sqlQuery, merch)
        //                                        .ToListAsync();
        //            //Get SMS Template
        //            var fetchtemplate = await GetSystemEditableSMSTemplate("APT_RMD", item.MerchantId);
        //            //Replace Template Place Holders
        //            fetchtemplate.Message.Replace("[FirstName]", branch.PatientName);
        //            fetchtemplate.Message.Replace("[LastName]", branch.PatientName);
        //            fetchtemplate.Message.Replace("[AppointmentDate]", DateFormatter.FormatDate_Appointment(branch.AppointmentDate));

        //            foreach (var interval in intervals)
        //            {
        //                if (DateCalculator.CalculateReminderIntervals(currentDate,
        //                    branch.AppointmentDate,
        //                    interval.DurationMetricsNavigation.Name, interval.Intervals))
        //                {
        //                    var cred = SendGeneralSms(fetchtemplate.Message, fetchtemplate.Sender, PhoneNumber);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        #endregion

        #region Patient Notification
        public async Task SendPatientWelcomeSMS(string url, string PatientNo, string Fullname, string PhoneNumber)
        {
            try
            {
                using var scope = _serviceScope.CreateScope();
                var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                //Get SMS Template
                var fetchtemplate = await GetSystemEditableSMSTemplate("PWLCM_SMS");


                //Replace Template Place Holders
                string MessageBody = fetchtemplate.Message.Replace("[Fullname]", Fullname);
                MessageBody = MessageBody.Replace("[PatientNo]", PatientNo);
                MessageBody = MessageBody.Replace("[Url]", url);

                var cred = await SendGeneralSms(MessageBody, fetchtemplate.Sender, PhoneNumber);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task SendPasswordResetSMS(string phonenumber, string username, string reseturl, long merchantid)
        {
            //Fetch Subject from DB based on supplied template ID
            var fetchtemplate = await GetSystemEditableSMSTemplate("RST_PWD");

            //Replace placeholder in template with message sent
            string MessageBody = fetchtemplate.Message.Replace("[Username]", username);
            MessageBody = MessageBody.Replace("[Url]", reseturl);
            var cred = await SendGeneralSms(MessageBody, fetchtemplate.Sender, phonenumber);
            return;
        }

        #endregion

        public Task SendBatchSMSAsync()
        {
            throw new NotImplementedException();
        }



        public Task SendSMSEmployeeMessageBroadcast()
        {
            throw new NotImplementedException();
        }

        public Task SendSMSPatientMessageBroadcast()
        {
            throw new NotImplementedException();
        }


        public async Task<TSmstemplate> GetSystemEditableSMSTemplate(string code)
        {
            try
            {
                using var scope = _serviceScope.CreateScope();
                var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                return await dbcontext.TSmstemplate
                    .Select(x => new TSmstemplate
                    {
                        Code = x.Code,
                        Message = x.Message,
                        Sender = x.Sender,
                        Name = x.Name
                    }).FirstOrDefaultAsync(x => x.Code == code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task<bool> SendGeneralSms(string messages, string sender, string phonenumber)
        {
            var cred = GetSmsCredentials();
            // Get the SMS Provider
            switch (Enum.Parse(typeof(SMSProviderEnum), cred.ActiveSmsProvider))
            {
                case SMSProviderEnum.Twilio:
                    // Twilio SMS Credentials
                    var appSettingsSection = _config.GetSection("TwilioKey");
                    var appSettings = appSettingsSection.Get<TwilioKey>();

                    //Check Intervals
                    var twi_message = SendMessage
                        .SendSingleMessage(appSettings.ACCOUNTSID, appSettings.AUTHTOKEN, messages, sender, phonenumber);
                    break;
                case SMSProviderEnum.Nexmo:

                    //Nexmo SMS Credentials
                    var appNex = _config.GetSection("NexmoKey");
                    var appSettingsNex = appNex.Get<NexmoKey>();

                    var nex_message = SendNexmoMessage
                        .SendSingleNexmoMessage(appSettingsNex.APIKEY, appSettingsNex.APISECRET, messages, sender, phonenumber);
                    break;
                case SMSProviderEnum.SMSProviderNG:
                    //SMS Provider NG Credentials
                    var apppProv = _config.GetSection("SMSProviderNGKey");
                    var appSettingsProv = apppProv.Get<SMSProviderNGKey>();

                    string message = SMSProviderNGsms.SendSingleSMSProviderMessage(appSettingsProv.USERNAME, appSettingsProv.PASSWORD,
                                                           messages, sender, phonenumber);

                    ResponseResult value = JsonConvert.DeserializeObject<ResponseResult>(message);
                    if (value.status == "OK")
                    {
                        //Log Sent message
                        TSmsLog newrec = new TSmsLog
                        {
                            Sendimmediately = true,
                            Datetosend = DateTime.Now,
                            CanSend = true,
                            Name = sender,
                            From = sender,
                            Sent = true,
                            To = phonenumber,
                            Message = messages,
                            ErrorMessage = message,
                            Subject = sender,
                            Createdby = "Anonymous",
                        };
                        await LogSms(newrec);
                    }
                    else
                    {
                        //Log Sent message
                        TSmsLog newrec = new TSmsLog
                        {
                            Sendimmediately = true,
                            Datetosend = DateTime.Now,
                            CanSend = true,
                            Name = sender,
                            From = sender,
                            Message = messages,
                            ErrorMessage = message,
                            Sent = false,
                            To = phonenumber,
                            Subject = sender,
                            Createdby = "Anonymous",
                        };
                        await LogSms(newrec);
                    }

                    break;
                default:
                    break;
            }
            return true;
        }

        public async Task LogSms(TSmsLog smsLog)
        {
            try
            {
                using (var scope = _serviceScope.CreateScope())
                {
                    var dbcontext = scope.ServiceProvider.GetService<authDbContext>();

                    smsLog.LastFailed = null;
                    smsLog.Lastmodified = null;
                    smsLog.Createddate = DateTime.Now;
                    dbcontext.TSmsLog.Add(smsLog);
                    await dbcontext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
