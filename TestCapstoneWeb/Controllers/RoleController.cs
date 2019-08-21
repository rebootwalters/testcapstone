using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace TestCapstoneWeb.Controllers
{
    public class RoleController : Controller
    {

        public ActionResult Page(int PageNumber, int PageSize)
        {
           
            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<RoleBLL> Model = new List<RoleBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainRoleCount();
                    Model = ctx.GetRoles(PageNumber*PageSize, PageSize);
                }
                return View("Index",Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }

        }

        public ActionResult Index()
        {
            return RedirectToRoute(new { Controller = "Role", Action = "Page", PageNumber = 0, PageSize = ApplicationConfig.DefaultPageSize });
        }

    
        public ActionResult Details(int id)
        {
            RoleBLL Role;
            try
            {
                using(ContextBLL ctx = new ContextBLL())
                {
                    Role = ctx.FindRoleByID(id);
                    if (null == Role)
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
            return View(Role) ;
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            RoleBLL defRole = new RoleBLL();
            defRole.RoleID = 0;
            return View(defRole);
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(BusinessLogicLayer.RoleBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateRole(collection);
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
            RoleBLL Role;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Role = ctx.FindRoleByID(id);
                    if (null == Role)
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
            return View(Role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusinessLogicLayer.RoleBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.UpdateRole(collection);
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
            RoleBLL Role;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Role = ctx.FindRoleByID(id);
                    if (null == Role)
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
            return View(Role);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BusinessLogicLayer.RoleBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteRole(id);
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
