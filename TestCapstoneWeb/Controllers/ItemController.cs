using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCapstoneWeb.Controllers
{
    [TestCapstoneWeb.Models.MustBeInRole(Roles = MagicConstants.AdminRoleName)]

    public class ItemController : Controller
    {

        List<SelectListItem> GetUserItems(ContextBLL ctx)
        {
            List<SelectListItem> ProposedReturnValue = new List<SelectListItem>();
           
                List<UserBLL> roles = ctx.GetUsers(0, 100);
                foreach (UserBLL u in roles)
                {
                    SelectListItem i = new SelectListItem();

                    i.Value = u.UserID.ToString();
                    i.Text = u.EMail;
                    ProposedReturnValue.Add(i);
                }
            
            return ProposedReturnValue;
        }
        // GET: Item
        public ActionResult Page(int PageNumber, int PageSize)
        {

            ViewBag.PageNumber = PageNumber;
            ViewBag.PageSize = PageSize;
            List<OwnedItemBLL> Model = new List<OwnedItemBLL>();

            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    ViewBag.TotalCount = ctx.ObtainOwnedItemCount();
                    Model = ctx.GetOwnedItems(PageNumber * PageSize, PageSize);
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

            return RedirectToRoute(new { Controller = "Item", Action = "Page", PageNumber = 0, PageSize = ApplicationConfig.DefaultPageSize });
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            OwnedItemBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindOwnedItemByID(id);
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
            OwnedItemBLL defItem = new OwnedItemBLL();
            defItem.OwnedItemID = 0;
            using (ContextBLL ctx = new ContextBLL())
            {
                ViewBag.Users = GetUserItems(ctx);
                return View(defItem);
            }
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(BusinessLogicLayer.OwnedItemBLL collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.CreateOwnedItem(collection);
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
            OwnedItemBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindOwnedItemByID(id);
                    if (null == User)
                    {
                        return View("ItemNotFound"); // BKW make this view
                    }
                ViewBag.Users = GetUserItems(ctx);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
 
            return View(User);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusinessLogicLayer.OwnedItemBLL collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.UpdateOwnedItem(collection);
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
            OwnedItemBLL User;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    User = ctx.FindOwnedItemByID(id);
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
        public ActionResult Delete(int id, BusinessLogicLayer.OwnedItemBLL collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    ctx.DeleteOwnedItem(id);
                }

                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                ViewBag.Exception = Ex;
                return View("Error");
            }
        }

        public ActionResult Stats()
        {
            try
            {
                List<OwnedItemBLL> Items;
                List<ItemStats> Model;
                using (ContextBLL ctx = new ContextBLL())
                {
                    int TotalCount = ctx.ObtainOwnedItemCount();
                    Items = ctx.GetOwnedItems(0,TotalCount);
                    MeaningfulCalculator mc = new MeaningfulCalculator();
                    Model = mc.ComputeStats(Items);

                }
                return View("Stats", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }

        }
    }
}
