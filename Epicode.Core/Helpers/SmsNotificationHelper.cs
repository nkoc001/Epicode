using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Epicode.Interfaces;

namespace Epicode.Core.Helpers
{
    public class SmsNotificationHelper
    {
        public static string GenerateSevenBitSms(INotificationConfiguration configuration, ISmsNotification message)
        {
            var queryStr = new StringBuilder();
            queryStr.Append("username=");
            queryStr.Append(HttpUtility.UrlEncode(configuration.UserName, Encoding.GetEncoding("ISO-8859-1")));
            queryStr.Append("&password=");
            queryStr.Append(HttpUtility.UrlEncode(configuration.Password, Encoding.GetEncoding("ISO-8859-1")));
            queryStr.Append("&message=");
            queryStr.Append(HttpUtility.UrlEncode(message.Message, Encoding.GetEncoding("ISO-8859-1")));
            queryStr.Append("&msisdn=");
            queryStr.Append(message.Recipient);
            queryStr.Append("&want_report=1");

            return queryStr.ToString();
        }

        public static string SendSms(string url, string data)
        {
            string result = string.Empty;
            try
            {
                var buffer = Encoding.Default.GetBytes(data);

                var webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.ContentLength = buffer.Length;
                var postData = webReq.GetRequestStream();

                postData.Write(buffer, 0, buffer.Length);
                postData.Close();
                var webResp = (HttpWebResponse)webReq.GetResponse();
                Console.WriteLine(webResp.StatusCode);

                var response = webResp.GetResponseStream();
                if (response != null)
                {
                    using (var reader = new StreamReader(response))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                result = string.Format("Error Occured: {0}", ex.Message);
            }

            return result;
        }
    }
}
