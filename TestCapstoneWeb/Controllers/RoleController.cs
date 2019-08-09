﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace TestCapstoneWeb.Controllers
{
    public class RoleController : Controller
    {

        public ActionResult Page(int? PageNumber, int? PageSize)
        {
            int PageN = (PageNumber.HasValue) ? PageNumber.Value : 0;
            int PageS = (PageSize.HasValue) ? PageSize.Value : ApplicationConfig.DefaultPageSize;
            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<RoleBLL> Model = new List<RoleBLL>();
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainRoleCount();
                    Model = ctx.GetRoles(PageN*PageS, PageS);
                }
                return View("Index",Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }

        }

        // GET: Role
        public ActionResult Index()
        {
            // return View(object);  // object is the MODEL
            // View("String",object) // object is the MODEL,  string is the VIEW NAME
            // View("String") // no MODEL but Name of View is String
            //RoleBLL r1 = new RoleBLL();
            //r1.RoleID = 1000;
            //r1.RoleName = "Brian";
            //RoleBLL r2 = new RoleBLL();
            //r2.RoleID = 1001;
            //r2.RoleName = "Carol";
            //
            //Model.Add(r1);
            //Model.Add(r2);
            List<RoleBLL> Model = new List<RoleBLL>();
            try
            {
                using(ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.PageNumber = 0;
                    ViewBag.PageSize = ApplicationConfig.DefaultPageSize;
                    ViewBag.TotalCount = ctx.ObtainRoleCount();
                    Model =   ctx.GetRoles(0, ViewBag.PageSize);
                }
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }

            return View(Model); // model is list of roles, name of view is same as method name
        }

        // GET: Role/Details/5
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
