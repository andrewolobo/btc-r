using AmEmailHandler.Libraries;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BTC_R.DataModels
{
    public class AppUtil
    {
        static download d;
        public byte[] Hash(String password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(password));
            return sha1data;
        }
        public String UnHash(byte[] data)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            var hashedPassword = encoding.GetString(data);
            return hashedPassword;
        }
        public String Email(EmailUser m)
        {
            string api_user = "app41648016@heroku.com";
            string api_key = "sim3wgkk2351";
            string subject = "amHealth™";
            string fromAddress = "service@amhealth.com";
            string url = "https://sendgrid.com/api/mail.send.json";

            download.host = "sendgrid.com";
            download.method = "POST";
            download.contentType = "application/x-www-form-urlencoded";


            string parameters = "api_user=" + api_user + "&api_key=" + api_key + "&to=" + m.To +
            "&toname=" + m.To + "&subject=" + subject + "&text=" + m.MessageTxt +
            "&from=" + fromAddress;

            //Send Message
            d = new download(url, parameters, delegate()
            {
                m.Status = "SENT";
                m.Condition = "";
            });
            return "";

        }
    }
    public class EmailUser
    {
        public string Status;
        public string Condition;
        public string MessageTxt;
        public string To;

    }
}