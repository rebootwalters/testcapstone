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
                  Model =   ctx.GetRoles(0, 20);
                }
            }
            catch(Exception ex)
            {
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
                return View("Error");
            }
            return View(Role) ;
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
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
                    //ctx.
                }

                    return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                return View("Error");
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id,int Brian, int Carol)
        {
            return View();
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusinessLogicLayer.RoleBLL collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BusinessLogicLayer.RoleBLL collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
