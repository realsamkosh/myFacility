using myFacility.Utilities.SMSUtility.SMSProviderNG.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace myFacility.Utilities.SMSUtility.SMSProviderNG
{
    public class SMSProviderNGsms
    {
        public static string SendSingleSMSProviderMessage(string username, string password,
                                               string body, string from, string to)
        {
            try
            {
                string baseurl = BaseConstants.SMSProviderNGBaseEndPoint;

                HttpWebRequest s = (HttpWebRequest)WebRequest.Create(baseurl);

                UTF8Encoding enc = new UTF8Encoding();

                string postdata = string.Format("username={0}&password={1}&message={2}&sender={3}&mobiles={4}", username, password, body, from, to);
                byte[] postdatabytes = enc.GetBytes(postdata);
                s.Method = "POST";
                s.ContentType = "application/x-www-form-urlencoded";
                s.ContentLength = postdatabytes.Length;

                Stream stream = s.GetRequestStream();
                stream.Write(postdatabytes, 0, postdatabytes.Length);
                stream.Close();

                // Close the Stream object.
                WebResponse result = s.GetResponse();
                // Open the stream using a StreamReader for easy access.

                // Get the stream containing content returned by the server.
                stream = result.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Clean up the streams.
                reader.Close();
                stream.Close();
                result.Close();


                return responseFromServer;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
