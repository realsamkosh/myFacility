using myFacility.Infrastructure;
using myFacility.Payment.Core.Paystack.Contstants;
using myFacility.Payment.Core.Paystack.Models;
using myFacility.Services.Contract;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.PaymentUtility.PayStack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using myFacility.Model.DataObjects.General.Payment;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace myFacility.Services.Handler
{
    public class PaymentService : IPaymentService
    {
        private readonly authDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<PaymentService> _logger;
        private readonly IAuthUser _authUser;
        private readonly StatisticsService _statisticsService;

        public PaymentService(authDbContext context, IConfiguration config, ILogger<PaymentService> logger,
            IAuthUser authUser, StatisticsService statisticsService)
        {
            _context = context;
            _config = config;
            _logger = logger;
            _authUser = authUser;
            _statisticsService = statisticsService;
        }

        private PayStackSettings GetPayStackSetting()
        {
            try
            {
                var appSettingsSection = _config.GetSection("PayStackSettings");
                var appSettings = appSettingsSection.Get<PayStackSettings>();
                return appSettings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<PaymentInitalizationResponseModel> InitializePaymentToPayStack(decimal amount)
        {
            var setting = GetPayStackSetting();
            var user = await _statisticsService.UserFullName();
            //Fetch
            PaymentInitalizationResponseModel modal = new PaymentInitalizationResponseModel();
            TransactionInitializationRequestModel reqModel = new TransactionInitializationRequestModel
            {
                amount = (amount * 100).ToString(),
                email = user.email,
                callbackUrl = setting.callbackUrl,
                //reference = "Invoice Generated"
            };
            var SecKey = string.Format("Bearer {0}", setting.SecretKey);

            var baseAddress = $"{BaseConstants.PaystackBaseEndPoint}{BaseConstants.PaystackInitializeTransactionEndPoint}";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Headers.Add("Authorization", SecKey);
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            string parsedContent = JsonConvert.SerializeObject(reqModel);
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();
            var cc = JsonConvert.DeserializeObject(content);
            var cd = JsonConvert.DeserializeObject(cc.ToString());
            modal = JsonConvert.DeserializeObject<PaymentInitalizationResponseModel>(content);
            return modal;
        }

        public string VerifyPayStackPayment(string RefrenceCode)
        {
            var setting = GetPayStackSetting();

            if (!string.IsNullOrEmpty(RefrenceCode))
            {
                PaymentInitalizationResponseModel ResponseModel = new PaymentInitalizationResponseModel();
                var baseAddress = "https://api.paystack.co/transaction/verify/" + RefrenceCode;

                var SecKey = string.Format("Bearer {0}", setting.SecretKey);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Headers.Add("Authorization", SecKey);
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "GET";

                var response = http.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                ResponseModel = JsonConvert.DeserializeObject<PaymentInitalizationResponseModel>(content);
                if (ResponseModel.data.status.ToLower() == "success")
                {
                    return "Success";
                }
                else
                {
                    return "Payment verification failed";
                }
            }
            else
            {
                return "No Reference code passed";
            }
        }

        public async Task<IEnumerable<PaymentHistoryViewModel>> GetPaymentHistory()
        {
            try
            {
                var fetchdata = await _context.TTransactionLog.Where(x => x.UserId == _authUser.UserId)
                    .Select(x => new PaymentHistoryViewModel
                    {
                        stagingid = x.StagingId,
                        transactiondate = x.CreatedDate.ToString(),
                        trxdesc = x.TrxDesc,
                        trxref = x.TrxRef,
                        status = x.PytConfirmed
                    }).ToListAsync();
                return fetchdata;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
