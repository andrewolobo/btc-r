using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTC_R.Models.Login_and_Registration
{
    public class LoginRegistration
    {
    }
    public class User
    {
        public Guid id { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String display_name { get; set; }
        public String email_address { get; set; }
        public byte[] password { get; set; }
        public bool verified { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
    public class Account
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public String account_name { get; set; }
        public DateTime created { get; set; }
    }
    public class TransactionKeys
    {
        public Guid id { get; set; }
        public Guid account_id { get; set; }
        public String transaction_key{get;set;}
        public bool complete{get;set;}
        public DateTime created_at { get; set; }
    }
    public class Transactions
    {
        public Guid id { get; set; }
        public Guid account_id { get; set; }
        public Guid transaction_key { get; set; }
        public String transaction_name { get; set; }
        public DateTime created_at { get; set; }
    }
    public class Logs
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public String action { get; set; }
        public DateTime date { get; set; }
    }
    public class BtcAddress
    {
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public String address { get;set;}
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}