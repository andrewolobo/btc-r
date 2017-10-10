using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTC_R.DataModels
{
    public class BTCModel
    {
    }
    public class btc_create
    {
        public String password { get; set; }
        public String api_code { get; set; }
        public String priv { get; set; }
        public String label { get; set; }
        public String email { get; set; }
    }
    public class btc_create_response
    {
        public String guid { get; set; }
        public String address { get; set; }
        public String label { get; set; }
    }
    public class btc_send 
    {
        public String password { get; set; }
        public String second_password { get; set; }
        public String to { get; set; }
        public String amount { get; set; }
        public String from { get; set; }
        public String fee { get; set; }
    }
    public class btc_send_response
    {
        public String message { get; set; }
        public String tx_hash { get; set; }
        public String notice { get; set; }
    }
    public class btc_send_many
    {
        public String password { get; set; }
        public String second_password { get; set; }
        public List<String> recipients { get; set; }
        public String from { get; set; }
        public String fee { get; set; }
    }
    public class btc_send_many_response
    {
        public String message { get; set; }
        public String tx_hash { get; set; }

    }
    public class btc_balance
    {
        public string password { get; set; }
    }
    public class btc_balance_response
    {
        public List<Address> addresses { get; set; }
    }
    public class Address
    {
        public String balance { get; set; }
        public String address { get; set; }
        public String label { get; set; }
        public String total_received { get; set; }
    }
    public class btc_balance_address
    {
        public String password { get; set; }
        public String main_password { get; set; }
        public String address { get; set; }

    }
    public class btc_create_address
    {
        public String password { get; set; }
        public String second_password { get; set; }
        public String label { get; set; }

    }
    public class btc_create_address_response
    {
        public String address { get; set; }
        public String label { get; set; }

    }
    public class btc_archive
    {
        public String password { get; set; }
        public String second_password { get; set; }
        public String address { get; set; }
    }



}