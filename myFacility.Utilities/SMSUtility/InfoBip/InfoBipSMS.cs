using myFacility.Utilities.SMSUtility.InfoBip.Model;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace myFacility.Utilities.SMSUtility.InfoBip
{
    public class InfoBipSMS
    {
        public static bool SendSingleSMS(string message, string PhoneNo, string personalizedurl, 
            string username, string password, string sender)
        {
            try
            {
                string API_URL = BaseConstants.InfoBipBaseUrl.Replace("[PersonalBaseUrl]", personalizedurl) + "sms/2/text/single";
                string userName = username;
                string Passwd = password;
                string Sender = sender;
                Uri uri = new Uri(API_URL);
                HttpWebRequest sRequest = (HttpWebRequest)WebRequest.Create(uri);

                String encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + Passwd));
                sRequest.Headers.Add("Authorization", "Basic " + encoded);

                sRequest.ContentType = "application/json";
                sRequest.Method = "POST";

                InfoBipSMSRequest smsRequest = new InfoBipSMSRequest
                {
                    to = PhoneNo,
                    from = Sender,
                    text = message
                };
                string JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(smsRequest);
                var data = Encoding.ASCII.GetBytes(JsonData);
                using (var stream = sRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                //Common.SwitchLog("API CALL: " + JsonData);
                var response = (HttpWebResponse)sRequest.GetResponse();
                if (response.StatusCode.ToString() != "OK")
                {
                    return false;
                }
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                // Common.SwitchLog(responseString);
                return true;
            }
            catch (Exception)
            {
                //_logger.LogError(ex.Message);
                return false;
            }

        }

        public static bool SendBulkSMS(String smsRecipient, String Message,
            string personalizedurl,
            string username, string password, string sender)
        {
            try
            {
                string API_URL = BaseConstants.InfoBipBaseUrl.Replace("[PersonalBaseUrl]", personalizedurl) + "sms/2/text/advanced";
                string userName = username;
                string Passwd = password;
                string Sender = sender;
                Uri uri = new Uri(API_URL);
                HttpWebRequest sRequest = (HttpWebRequest)WebRequest.Create(uri);
                //CredentialCache credentials = new CredentialCache();
                //NetworkCredential netCredential = new NetworkCredential(userName, Passwd);
                //credentials.Add(uri, "Basic", netCredential);
                //sRequest.Credentials = credentials;
                sRequest.ContentType = "application/json";
                sRequest.Method = "POST";
                SetBasicAuthHeader(sRequest, userName, Passwd);
                InfoBipSMSRequest smsRequest = new InfoBipSMSRequest
                {
                    to = smsRecipient,
                    from = Sender,
                    text = Message
                };
                String JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(smsRequest);
                //Common.WriteDebugLog("SMS payload :" + JsonData);
                var data = Encoding.ASCII.GetBytes(JsonData);
                using (var stream = sRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)sRequest.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //Common.WriteServiceLog("SMS Response payload :" + responseString);
                return true;
            }
            catch (Exception ex)
            {
                //Common.WriteLog(ex);
                return false;
            }
        }

        protected static void SetBasicAuthHeader(WebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }
    }
}
