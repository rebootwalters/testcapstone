using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCapstoneWeb.Controllers
{
    public class OneViewTwoTablesController : Controller
    {

        List<SelectListItem> GetRoleItems(ContextBLL ctx)
        {
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();

            List<RoleBLL> roles = ctx.GetRoles(0, 25);
            foreach (RoleBLL r in roles)
            {
                SelectListItem i = new SelectListItem();

                i.Value = r.RoleID.ToString();
                i.Text = r.RoleName;
                ProposedReturnValue.Add(i);
            }

            return ProposedReturnValue;
        }

     
        // GET: OneViewTwoTables
        public ActionResult Create(int id)
        {
            using (ContextBLL ctx = new ContextBLL())
            {
              
                ViewBag.Roles = GetRoleItems(ctx);
                RoleBLL role = ctx.FindRoleByID(id);
                OneViewTwoTablesModel Model = new OneViewTwoTablesModel();
                if (role != null)
                {
                    Model.RoleID = role.RoleID;
                    Model.NewRoleName = "";
                }
                return View(Model);
            }
        }

        [HttpPost]
        public ActionResult Create(OneViewTwoTablesModel collection)
        {
           
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                { if (!ModelState.IsValid)
                    {
                        ViewBag.Roles = GetRoleItems(ctx);
                        return View(collection);
                    }
                    if (!string.IsNullOrWhiteSpace(collection.NewRoleName))
                    {
                        collection.RoleID = ctx.CreateRole(collection.NewRoleName);
                    }
                    int UserID = ctx.CreateUser(collection.EMail, collection.Password, collection.Password,
                        collection.DateOfBirth, collection.RoleID);
                    ctx.CreateOwnedItem(UserID, collection.ItemDescription,collection.ItemPrice);

                }
                return RedirectToAction("Index", "User");
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
        }
    }
}