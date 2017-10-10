using AmHandler.Libraries;
using BTC_R.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace BTC_R.Controllers
{
    public class MerchantController : ApiController
    {

        [HttpGet]
        [ActionName("Create")]
        public String Create()
        {
            HttpUtil.GET _GET = new HttpUtil.GET();
            return Constants.base_url + "?" + _GET.GetQueryString(new { search = "oloborama@gmail.com" });
        }
        [HttpGet]
        [ActionName("Authenticate")]
        public String Authenticate()
        {
            Dictionary<String, String> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + Constants.api_key);

            download d = new download(Constants.base_address + Constants.user, "GET", headers);
            d._complete += delegate()
            {
                Console.WriteLine(d.postdatas);
            };
            return "";
        }
        [HttpPost]
        [ActionName("Payment")]
        public String Payment()
        {
            HttpContext.Current.Request.InputStream.Position = 0;
            var result = new System.IO.StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd();

            Notification n = JsonConvert.DeserializeObject<Notification>(result);
            return n.data.address;
        }
        [HttpGet]
        [ActionName("Notifications")]
        public object Notification()
        {
            Coinbase coin = new Coinbase();
            return coin.Request("/v2/notifications");

        }
    }
}
