using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BTC_R.DataModels
{
    public class Coinbase
    {
        public static String api_key = "3iPsLmts5b4hL8TE";
        public static String hsecret = "5b6851a11320af525652da91e248d338cbbc1fa922a207343213a0d2136b3945";
        public enum RTYPE {GET,POST};
        public String hmac(String method, String body)
        {
            var key = Encoding.ASCII.GetBytes(api_key);
            Int32 timestamp = Date();
            string requestBody = "";
            string requestPath = body;
            String message = timestamp + method + requestPath + requestBody;


            return BitConverter.ToString(hmacSHA256(message, api_key)).Replace("-", "").ToLower();
        }
        static byte[] hmacSHA256(String data, String key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key)))
            {
                return hmac.ComputeHash(Encoding.ASCII.GetBytes(data));
            }
        }
        public Int32 Date()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            return secondsSinceEpoch;
        }
        public object Request(string body)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://api.coinbase.com/"+body);


                request.ContentType = "application/json";
                request.Headers.Add("CB-ACCESS-KEY", api_key);
                request.Headers.Add("CB-ACCESS-SIGN", hmac(RTYPE.GET.ToString(),body));
                request.Headers.Add("CB-ACCESS-TIMESTAMP", Date() + "");

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (WebException e)
            {
                return e;
            }

        }
    }
    public class Notification
    {
        public String id { get; set; }
        public String type { get; set; }
        public data data { get; set; }
        public additional_data additional_data { get; set; }
        public user user { get; set; }
        public account account { get; set; }
        public String delivery_attempts { get; set; }
        public String created_at { get; set; }
        public String resource { get; set; }
        public String resource_path { get; set; }

    }
    public class data:resourceStruct
    {
        public String address { get; set; }
        public String name { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
    }
    public class resourceStruct
    {
        public String id { get; set; }
        public String resource { get; set; }
        public String resource_path { get; set; }
    }
    public class user:resourceStruct
    {

    }
    public class additional_data
    {
        public String hash { get; set; }
        public amountStruct amount { get; set; }
        public transaction transaction { get; set; }
    }
    public class transaction:resourceStruct
    {

    }
    public class account:resourceStruct
    {

    }
    public class amountStruct
    {
        public String amount { get; set; }
        public String currency { get; set; }
    }

}