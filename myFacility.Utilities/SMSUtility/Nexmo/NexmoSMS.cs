using Nexmo.Api;
using Nexmo.Api.Request;
using static Nexmo.Api.SMS;

namespace myFacility.Utilities.SMSUtility.NexmoSMS
{
    public class SendNexmoMessage
    {
        /// <summary>
        /// Send Single Message with From Phonenumber
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="apiSecret"></param>
        /// <param name="body"></param>
        /// <param name="from">+15017122661</param>
        /// <param name="to">+15558675310</param>
        /// <returns></returns>
        public static string SendSingleNexmoMessage(string apiKey, string apiSecret,
                                               string body, string from, string to)
        {
            var client = new Client(creds: new Credentials
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret
            });

            var results = client.SMS.Send(new SMSRequest
            {
                from = from,
                to = to,
                text = body
            });

            return results.messages.ToString();
        }
    }
}
