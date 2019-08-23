using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCapstoneWeb.Controllers
{
 
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("AUTHUsername");
            Session.Remove("AUTHRoles");
            return View("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            // displays empty login screen with predefined returnURL
            LoginModel m = new LoginModel();
            m.Message = TempData["Message"]?.ToString() ?? "";
            m.ReturnURL = TempData["ReturnURL"]?.ToString() ?? @"~/Home";
            m.Username = "genericuser";
            m.Password = "genericpassword";

            return View(m);
        }

        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            using (BusinessLogicLayer.ContextBLL ctx = new BusinessLogicLayer.ContextBLL())
            {
                BusinessLogicLayer.UserBLL user = ctx.FindUserByEMail(info.Username);
                if (user == null)
                {
                    info.Message = $"The Username '{info.Username}' does not exist in the database";
                    return View(info);
                }
                string actual = user.Hash;
                //string potential = info.Password + user.Salt ;
                //bool validateduser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual, potential);
                string potential = info.Password;
                bool validateduser = actual == potential;
                if (!validateduser)
                {
                    potential = info.Password + user.Salt ;

                    validateduser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual, potential);
                }
                if (validateduser)
                {
                    Session["AUTHUsername"] = user.EMail;
                    Session["AUTHRoles"] = user.RoleName;
                    return Redirect(info.ReturnURL);
                }
                info.Message = "The password was incorrect";
                return View(info);
            }
        }
    }


}
