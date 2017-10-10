using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTC_R.DataModels
{
    public class Constants
    {
        public static String wallet_id = "ba333f41-15c5-46e3-8728-9c435647e275";
        public static string base_url = "https://blockchain.info/merchant/"+wallet_id;
        public static String local_url = "http://localhost:26204/";
        //Coinbase
        public static String base_address = "https://api.coinbase.com/v2/";
        public static String user = "user";
        public static String api_key = "3iPsLmts5b4hL8TE";
    }
}