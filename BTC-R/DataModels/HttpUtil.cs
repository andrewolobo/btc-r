using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BTC_R.DataModels
{
    public class HttpUtil
    {
        public class POST
        {

        }
        public class GET
        {
            public String _execute(String url, object param)
            {
                String response = "";
                using (WebClient client = new WebClient())
                {
                    response = client.DownloadString(url+GetQueryString(param));
                }
                return response;
            }

            public string GetQueryString(object obj)
            {
                var properties = from p in obj.GetType().GetProperties()
                                 where p.GetValue(obj, null) != null
                                 select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

                return String.Join("&", properties.ToArray());
            }
        }

    }
}