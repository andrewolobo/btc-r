using BTC_R.Models.Login_and_Registration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BTC_R.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<BtcAddress> Addresses { get; set; }
        public DbSet<Logs> UserLogs { get; set; }
    }
}