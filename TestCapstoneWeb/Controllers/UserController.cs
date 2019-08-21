using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace TestCapstoneWeb.Controllers
{
    public class UserController : Controller
    {

        List<SelectListItem> GetRoleItems()
        {
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();
            using (ContextBLL ctx = new ContextBLL())
            {
                List<RoleBLL> roles = ctx.GetRoles(0, 25);
                foreach (RoleBLL r in roles)
                {
                    SelectListItem i = new SelectListItem();
                    
                    i.Value = r.RoleID.ToString();
                    i.Text = r.RoleName;
                    ProposedReturnValue.Add(i);
                }
            }
            return ProposedReturnValue;
        }

        public ActionResult Page(int PageNumber, int PageSize)
        {
          
            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<UserBLL> Model = new List<UserBLL>();
            
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainUserCount();
                    Model = ctx.GetUsers(PageNumber * PageSize, PageSize);
                }
                return View("Index", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }

        }

        // GET: User
        public ActionResult Index()
        {

            return RedirectToRoute(new { Controller = "User", Action = "Page", PageNumber = 0, PageSize = ApplicationConfig.DefaultPageSize });
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            UserBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUserByID(id);
                    if (null == User)
                    {
                        return View("ItemNotFound"); // BKW make this view
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(User);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            UserBLL defUser = new UserBLL();
            defUser.UserID = 0;
            ViewBag.Roles = GetRoleItems();
            return View(defUser);
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(BusinessLogicLayer.UserBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateUser(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            UserBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUserByID(id);
                    if (null == User)
                    {
                        return View("ItemNotFound"); // BKW make this view
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            ViewBag.Roles = GetRoleItems();
            return View(User);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusinessLogicLayer.UserBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.UpdateUser(collection);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            UserBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindUserByID(id);
                    if (null == User)
                    {
                        return View("ItemNotFound"); // BKW make this view
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View(User);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BusinessLogicLayer.UserBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteUser(id);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }
    }
}
