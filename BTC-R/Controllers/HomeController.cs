using BTC_R.DataModels;
using BTC_R.Models;
using BTC_R.Models.Login_and_Registration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTC_R.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login(int? login)
        {
            if (login == 0)
            {
                ViewBag.info = "You're username and password don't seem to match. Please try again!";
            }
            return View();
        }
        private ActionResult Authenticate(ActionResult view)
        {
            if (Session["id"] != null)
            {
                ViewBag.session = Session["id"];

                return view;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Dashboard()
        {
            Guid user = Guid.Parse(Session["id"].ToString());
            ViewBag.logs = db.UserLogs.Where(x => x.user_id == user).Take(5);
            Log(Guid.Parse(Session["id"].ToString()), "User Visited Dashboard");
            return Authenticate(View());
        }
        public ActionResult Addresses()
        {
            Log(Guid.Parse(Session["id"].ToString()), "User Visited Addresses");
            return View();
        }
        public ActionResult Fundme()
        {
            Log(Guid.Parse(Session["id"].ToString()), "User Checked out Fundme");
            return View();
        }
        public ActionResult Transactions()
        {
            Log(Guid.Parse(Session["id"].ToString()), "User Visited Transactions");
            return View();
        }
        public ActionResult Subaccounts()
        {
            Log(Guid.Parse(Session["id"].ToString()), "User Visited Subaccounts");
            return View();
        }
        public ActionResult Logout()
        {
            Session["id"] = null;
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Registration(FormCollection collection)
        {

            if (Verify(collection["email"]))
            {
                AppUtil util = new AppUtil();
                var guid = Guid.NewGuid();
                db.Users.Add(new User() { verified = false, first_name = collection["first_name"], display_name = collection["display_name"], last_name = collection["last_name"], email_address = collection["email"], created = DateTime.Now, updated = DateTime.Now, id = guid, password = util.Hash(collection["password"]) });
                if (collection["btc_address"] != null)
                {
                    db.Addresses.Add(new BtcAddress() { id = Guid.NewGuid(), user_id = guid, address = collection["btc_address"], created = DateTime.Now, updated = DateTime.Now });
                }
                util.Email(new EmailUser() { To = collection["email"], MessageTxt = Constants.local_url + "Home/Verification?auth_id=" + guid });
                Log(guid, collection["email"] + " registered for a new account");
                db.SaveChanges();
            }
            return View("Verify");

        }
        private void Log(Guid user,String action)
        {
            db.UserLogs.Add(new Logs
            {
                id = Guid.NewGuid(),
                action = action,
                user_id = user,
                date = DateTime.Now
            });
            db.SaveChanges();
        }
        [HttpPost]
        public ActionResult LoginUser(FormCollection collection)
        {
            String email = collection["email"];
            String password = collection["password"];
            AppUtil util = new AppUtil();
            var result = db.Users.Where(x => x.email_address.Equals(email)).Select(x => x);
            foreach (var item in result)
            {
                var i = util.Hash(password);
                var t = util.UnHash(item.password);
                var s = util.UnHash(i);
                if (s == t)
                {
                    Session["id"] = item.id;
                    return RedirectToAction("Dashboard", "Home");

                }
            }
            return RedirectToAction("Login", "Home", new { login = 0 });

        }
        private Boolean Verify(String email)
        {
            var result = db.Users.Select(x => x.email_address.Equals(email));
            if (result.Count() != 0)
            {
                return false;
            }
            return true;
        }
        [HttpGet]
        public ActionResult Verification(String auth_id)
        {
            var flag = false;
            var auth = Guid.Parse(auth_id);
            var users = db.Users.Where(x => x.id.Equals(auth));
            foreach (var user in users)
            {
                user.verified = true;
                flag = true;
                db.Entry(user).State = EntityState.Modified;
            }
            db.SaveChanges();
            if (flag)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Unverified", "Home");
            }

        }
        public ActionResult Unverified()
        {
            return View();
        }
    }
}