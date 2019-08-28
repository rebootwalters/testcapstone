using BusinessLogicLayer;
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            // displays empty login screen with predefined returnURL
            LoginModel m = new LoginModel();
            m.Message = TempData["Message"]?.ToString() ?? "";
            m.ReturnURL = TempData["ReturnURL"]?.ToString() ?? @"~/Home";
            m.EMail = "Email Address";
            m.Password = "genericpassword";

            return View(m);
        }

        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            using (BusinessLogicLayer.ContextBLL ctx = new BusinessLogicLayer.ContextBLL())
            {
                BusinessLogicLayer.UserBLL user = ctx.FindUserByEMail(info.EMail);
                if (user == null)
                {
                    info.Message = $"The Username '{info.EMail}' does not exist in the database";
                    return View(info);
                }
                string actual = user.Hash;
                //string potential = info.Password + user.Salt ;
                //bool validateduser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual, potential);
                string potential = info.Password;
                string ValidationType = $"ClearText:({user.UserID})";
                bool validateduser = actual == potential;
                if (!validateduser)
                {
                    potential = info.Password + user.Salt ;
                    try
                    {
                       // this try catches the event where a cleartext user types the 
                       // wrong password.  The VerifyHashedPassword will throw exception 
                       // because salt is invalid.
                        validateduser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual, potential);
                        ValidationType = $"HASHED:({user.UserID})";
                    }
                    catch (Exception)
                    {

                        validateduser = false;
                    }
                }
                if (validateduser)
                {
                    Session["AUTHUsername"] = user.EMail;
                    Session["AUTHRoles"] = user.RoleName;
                    Session["AUTHTYPE"] = ValidationType;
                    return Redirect(info.ReturnURL);
                }
                info.Message = "The password was incorrect";
                return View(info);
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel info)
        {
            using (BusinessLogicLayer.ContextBLL ctx = new BusinessLogicLayer.ContextBLL())
            {
                BusinessLogicLayer.UserBLL user = ctx.FindUserByEMail(info.EMail);
                if (user != null)
                {
                    info.Message = $"The EMail Address '{info.EMail}' already exists in the database";
                    return View(info);
                }
                user = new UserBLL();
                user.DateOfBirth = info.DateOfBirth;
                user.EMail = info.EMail;
                user.Salt = System.Web.Helpers.Crypto.
                    GenerateSalt(MagicConstants.SaltSize);
                user.Hash = System.Web.Helpers.Crypto.
                    HashPassword(info.Password + user.Salt);
                user.RoleID = 3;
                
                ctx.CreateUser(user);
                    Session["AUTHUsername"] = user.EMail;
                    Session["AUTHRoles"] = user.RoleName;
                    Session["AUTHTYPE"] = "HASHED";
                    return RedirectToAction("Index"); 
                }
            }

        public ActionResult Hash()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("NotLoggedIn");

            }
            if (User.Identity.AuthenticationType.StartsWith("HASHED"))
            {
                return View("AlreadyHashed");
            }
            if (User.Identity.AuthenticationType.StartsWith("IMPERSONATED"))
            {
                return View("ActionNotAllowed");
            }
            using (BusinessLogicLayer.ContextBLL ctx = new BusinessLogicLayer.ContextBLL())
            {
                BusinessLogicLayer.UserBLL user = ctx.FindUserByEMail(User.Identity.Name);
                if (user == null)
                {
                    Exception Message = new Exception($"The Username '{User.Identity.Name}' does not exist in the database");
                    ViewBag.Exception = Message;
                    return View("Error");
                }
                user.Salt = System.Web.Helpers.Crypto.GenerateSalt(MagicConstants.SaltSize);
                user.Hash = System.Web.Helpers.Crypto.HashPassword(user.Hash + user.Salt);
                ctx.UpdateUser(user);
                    
                   string ValidationType = $"HASHED:({user.UserID})";
                
                    Session["AUTHUsername"] = user.EMail;
                    Session["AUTHRoles"] = user.RoleName;
                    Session["AUTHTYPE"] = ValidationType;

                return RedirectToAction("Index", "Home") ;
            }

        }


    }


}
