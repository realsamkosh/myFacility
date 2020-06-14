using myFacility.DataObject.SysAdmin.MessageObject;
using myFacility.Infrastructure;
using myFacility.Models.DataObjects.MessageObject;
using myFacility.Models.Domains.Messaging;
using myFacility.Services.Contract;
using myFacility.Utilities;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.ExceptionUtility;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myFacility.Services.Handler
{
    public class MessageManagementService : IMessageManagementService
    {
        private readonly IAuthUser _authUser;
        private readonly ILogger<MessageManagementService> _logger;
        private readonly IServiceScopeFactory _serviceScope;
        private readonly authDbContext _context;
        private readonly IDataProtector _protector;

        public MessageManagementService(authDbContext context, IDataProtectionProvider dataProtectionProvider,
             IAuthUser authUser, ILogger<MessageManagementService> logger,
             IServiceScopeFactory serviceScope)
        {
            _context = context;
            _protector = dataProtectionProvider.CreateProtector(GetType().FullName);
            _authUser = authUser;
            _logger = logger;
            _serviceScope = serviceScope;
        }

        #region Email Template

        public async Task<EmailTemplateViewModel> GetEmailTemplate(int id, bool status = true)
        {
            try
            {
                var fetchData = await _context.TEmailtemplate.FirstOrDefaultAsync(x => x.EtemplateId == id && x.IsActive == status);

                return new EmailTemplateViewModel
                {
                    templateid = id,
                    code = fetchData.Code,
                    name = fetchData.Name,
                    subject = fetchData.Subject,
                    body = fetchData.Body,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<IEnumerable<EmailTemplateViewModel>> GetEmailTemplates(bool status = true)
        {
            //Get Logged in user data
            return await _context.TEmailtemplate.Where(c => c.IsActive == status)
                .Select(x => new EmailTemplateViewModel
                {
                    templateid = x.EtemplateId,
                    code = x.Code,
                    name = x.Name,
                    subject = x.Subject,
                    body = x.Body,
                }).ToListAsync();
        }
        public async Task<string> CreateEmailTemplate(EmailTemplateDTO obj)
        {
            string status = "";
            try
            {
                var checkexist = await _context.TEmailtemplate
                    .AnyAsync(x => x.Name == obj.name || x.Code == obj.code);
                if (checkexist == false)
                {
                    TEmailtemplate newrecord = new TEmailtemplate
                    {

                        Name = obj.name,
                        Body = obj.body,
                        Code = obj.code,
                        Subject = obj.subject,
                        CreatedBy = _authUser.Name,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        LastModified = null,
                        ModifiedBy = null
                    };

                    _context.TEmailtemplate.Add(newrecord);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                return status;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
        public async Task<string> UpdateEmailTemplate(EmailTemplateDTO obj, int id)
        {
            string status = "";
            try
            {
                //Check if record exist not as same id
                var checkexist = await _context.TEmailtemplate
                  .AnyAsync(x => x.EtemplateId != id &&
                   (x.Name == obj.name));
                if (checkexist == false)
                {
                    var state = await _context.TEmailtemplate
                   .FirstOrDefaultAsync(x => x.EtemplateId == id);
                    state.Name = obj.name;
                    state.Body = obj.body;
                    //state.Code = obj.code;
                    state.EtemplateId = id;
                    //state.Subject = obj.subject;
                    state.ModifiedBy = _authUser.Name;
                    state.LastModified = DateTime.Now;
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                return status;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }
        public async Task<bool> DeleteEmailTemplate(int id, bool status)
        {
            try
            {
                var coutry = await _context.TEmailtemplate
                    .FirstOrDefaultAsync(x => x.EtemplateId == id);
                coutry.IsActive = status;
                coutry.ModifiedBy = _authUser.Name;
                coutry.LastModified = DateTime.Now;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        #endregion

        #region SMS Template

        public async Task<SmsTemplateViewModel> GetSmsTemplate(int id, bool status = true)
        {
            try
            {

                var fetchData = await _context.TSmstemplate.FirstOrDefaultAsync(x => x.SmstemplateId == id);

                if (fetchData != null)
                {
                    return new SmsTemplateViewModel
                    {
                        smstemplateid = fetchData.SmstemplateId,
                        name = fetchData.Name,
                        sender = fetchData.Sender,
                        message = fetchData.Message,
                    };
                }
                else
                {
                    _logger.LogError(string.Format(ResponseErrorMessageUtility.NotValidProtectedId, "SmsTemplateId"));
                    throw new ExceptionHandlerHelper(ResponseErrorMessageUtility.NotValidProtectedId, "SmsTemplateId");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<SmsTemplateViewModel>> GetSmsTemplates(bool status = true)
        {
            return await _context.TSmstemplate.Where(c => c.IsActive == status).Select(x => new SmsTemplateViewModel
            {
                smstemplateid = x.SmstemplateId,
                code = x.Code,
                name = x.Name,
                sender = x.Sender,
                message = x.Message,
            }).ToListAsync();
        }

        public async Task<bool> DeleteSmsTemplate(int id, bool status)
        {
            try
            {
                var coutry = await _context.TSmstemplate
                    .FirstOrDefaultAsync(x => x.SmstemplateId == id);
                coutry.IsActive = status;
                coutry.ModifiedBy = _authUser.Name;
                coutry.LastModified = DateTime.Now;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<string> CreateSmsTemplate(SmsTemplateDTO obj)
        {
            string status = "";
            try
            {
                var checkexist = await _context.TSmstemplate
                    .AnyAsync(x => x.Name == obj.name || x.Code == obj.code);
                if (checkexist == false)
                {
                    TSmstemplate newrecord = new TSmstemplate
                    {

                        Name = obj.name,
                        Sender = obj.sender,
                        Message = obj.message,
                        Code = obj.code,
                        CreatedBy = _authUser.Name,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        LastModified = null,
                        ModifiedBy = null
                    };

                    _context.TSmstemplate.Add(newrecord);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                return status;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        public async Task<string> UpdateSmsTemplate(SmsTemplateDTO obj, int id)
        {
            string status = "";
            try
            {
                //Check if record exist not as same id
                var checkexist = await _context.TSmstemplate
                  .AnyAsync(x => x.SmstemplateId != id &&
                   (x.Name == obj.name || x.Code == obj.code));
                if (checkexist == false)
                {
                    var state = await _context.TSmstemplate
                   .FirstOrDefaultAsync(x => x.SmstemplateId == id);
                    state.Name = obj.name;
                    state.Sender = obj.sender;
                    state.Message = obj.message;
                    state.Code = obj.code;
                    state.SmstemplateId = id;
                    state.ModifiedBy = _authUser.Name;
                    state.LastModified = DateTime.Now;
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        status = "1";
                        return status;
                    }
                    else
                    {
                        status = ResponseErrorMessageUtility.RecordNotSaved;
                        return status;
                    }
                }
                status = string.Format(ResponseErrorMessageUtility.RecordExistBefore, obj.name);
                return status;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                status = ResponseErrorMessageUtility.RecordNotSaved;
                return status;
            }
        }

        #endregion

        #region Message Broadcast
        //public async Task CreateMessageBroadcast(BroadcastScheduleDTO obj)
        //{
        //    try
        //    {
        //        //Check Frequency
        //        var fredcode = await _context.TLkupFrequency.FirstOrDefaultAsync(x => x.Code == obj.frequencycode);
        //        var nextdate = FrequencyHelper.CalculateNextBroadcastDate(obj.frequencycode, DateTime.Parse(obj.dateofschedule));
        //        if (obj.receivertype == RecieverType.patient && fredcode != null)
        //        {
        //            if (obj.broadcasttype == BroadcastType.email)
        //            {
        //                await _bus.SendCommand(new PatientBroadcastScheduleCreateCommand
        //                {
        //                    PatientgroupId = _patientManagementService.GenericRetrieval(obj.messagegroup),
        //                    EmailtemplateId = Convert.ToInt64(_protector.Unprotect(obj.templateid)),
        //                    SmstemplateId = null,
        //                    BroadcastType = BroadcastType.email.ToString(),
        //                    FrequencyId = fredcode.FrequencyId,
        //                    BroadCastStartDate = DateTime.Parse(obj.dateofschedule),
        //                    BroadcastNextDate = nextdate,
        //                }).ConfigureAwait(false);
        //            }
        //            else if (obj.broadcasttype == BroadcastType.sms)
        //            {
        //                await _bus.SendCommand(new PatientBroadcastScheduleCreateCommand
        //                {
        //                    PatientgroupId = _patientManagementService.GenericRetrieval(obj.messagegroup),
        //                    EmailtemplateId = null,
        //                    SmstemplateId = Convert.ToInt64(_protector.Unprotect(obj.templateid)),
        //                    BroadcastType = BroadcastType.sms.ToString(),
        //                    FrequencyId = fredcode.FrequencyId,
        //                    BroadCastStartDate = DateTime.Parse(obj.dateofschedule),
        //                    BroadcastNextDate = nextdate,
        //                }).ConfigureAwait(false);
        //            }
        //        }
        //        else if (obj.receivertype == RecieverType.employee && fredcode != null)
        //        {
        //            if (obj.broadcasttype == BroadcastType.email)
        //            {
        //                await _bus.SendCommand(new EmployeeBroadcastScheduleCreateCommand
        //                {
        //                    EmployeegroupId = _employeeManagementService.GenericRetrieval(obj.messagegroup),
        //                    EmailtemplateId = Convert.ToInt64(_protector.Unprotect(obj.templateid)),
        //                    SmstemplateId = null,
        //                    BroadcastType = BroadcastType.email.ToString(),
        //                    FrequencyId = fredcode.FrequencyId,
        //                    BroadCastStartDate = DateTime.Parse(obj.dateofschedule),
        //                    BroadcastNextDate = nextdate,
        //                }).ConfigureAwait(false);
        //            }
        //            else if (obj.broadcasttype == BroadcastType.sms)
        //            {
        //                await _bus.SendCommand(new PatientBroadcastScheduleCreateCommand
        //                {
        //                    PatientgroupId = _patientManagementService.GenericRetrieval(obj.messagegroup),
        //                    EmailtemplateId = null,
        //                    SmstemplateId = Convert.ToInt64(_protector.Unprotect(obj.templateid)),
        //                    BroadcastType = BroadcastType.sms.ToString(),
        //                    FrequencyId = fredcode.FrequencyId,
        //                    BroadCastStartDate = DateTime.Parse(obj.dateofschedule),
        //                    BroadcastNextDate = nextdate,
        //                }).ConfigureAwait(false);
        //            }
        //        }
        //        else
        //        {
        //            throw new ExceptionHandlerHelper("Receiver Type Not Supported or Frequency Code is Wrong");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        #endregion

        #region Email Tokens
        public async Task<IEnumerable<EmailTokensViewModel>> GetEmailTokens(bool status = true)
        {
            //Get Logged in user data
            return await _context.TEmailToken.Where(c => c.IsActive == status)
                .Select(x => new EmailTokensViewModel
                {
                    emailtokenid = x.EmailtokenId,
                    emailtoken = x.EmailToken,
                    previewname = x.PreviewName
                }).ToListAsync();
        }
        #endregion

        #region Sms Tokens
        public async Task<IEnumerable<SmsTokensViewModel>> GetSmsTokens(bool status = true)
        {
            //Get Logged in user data
            return await _context.TSmsToken.Where(c => c.IsActive == status)
                .Select(x => new SmsTokensViewModel
                {
                    smstokenid = x.SmstokenId,
                    smstoken = x.SmsToken,
                    previewname = x.PreviewName
                }).ToListAsync();
        }
        #endregion
    }
}
