using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace myFacility.SMS.Core.TwilioSMS
{
    public class SendMessage
    {

        /// <summary>
        /// Send Single Message with From Phonenumber
        /// </summary>
        /// <param name="accountSid"></param>
        /// <param name="authToken"></param>
        /// <param name="body"></param>
        /// <param name="from">+15017122661</param>
        /// <param name="to">+15558675310</param>
        /// <returns></returns>
        public static string SendSingleMessage(string accountSid, string authToken, 
                                               string body, string from, string to)
        {
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber(from), 
                to: new Twilio.Types.PhoneNumber(to)                
            );

            return message.Sid.ToString();
        }
    }


}
