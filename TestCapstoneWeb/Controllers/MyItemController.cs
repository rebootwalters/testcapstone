using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCapstoneWeb.Controllers
{
    public class MyItemController : Controller
    {
        bool IsThisMine(ContextBLL ctx ,OwnedItemBLL Mine)
        {
            if (User.IsInRole(MagicConstants.AdminRoleName))
            {
                return true;
            }
            UserBLL me = ctx.FindUserByEMail(User.Identity.Name);
            if (me == null)
            {
                return false;
            }

            return me.UserID == Mine.OwnerID;
        }
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
                    UserBLL ThisUser = ctx.FindUserByEMail(User.Identity.Name);
                    if (null == ThisUser)
                    {
                        ViewBag.Exception = new Exception($"Could Not Find Record for User: {User.Identity.Name}");
                        return View("Error");
                    }
                    ViewBag.TotalCount = ctx.ObtainCountOfItemsOwnedByUser(ThisUser.UserID);
                    Model = ctx.GetOwnedItemsRelatedToUser(ThisUser.UserID,PageNumber * PageSize, PageSize);
                }
                return View("..\\Item\\Index", Model);
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

            return RedirectToRoute(new { Controller = "MyItem", Action = "Page", PageNumber = 0, PageSize = ApplicationConfig.DefaultPageSize });
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            OwnedItemBLL Model;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Model = ctx.FindOwnedItemByID(id);
                    if (null == Model)
                    {
                        return View("ItemNotFound"); 
                    }
                    if (!IsThisMine(ctx,Model))
                    {
                        return View("NotYourItem");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View("..\\Item\\Details",Model);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            OwnedItemBLL defItem = new OwnedItemBLL();
            defItem.OwnedItemID = 0;
            using (ContextBLL ctx = new ContextBLL())
            {
                ViewBag.Users = GetUserItems(ctx);
                return View("..\\Item\\Create", defItem);
            }
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(BusinessLogicLayer.OwnedItemBLL collection)
        {
            try
            {
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
            OwnedItemBLL Model;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Model = ctx.FindOwnedItemByID(id);
                    if (null == Model)
                    {
                        return View("ItemNotFound"); // BKW make this view
                    }
                    if (!IsThisMine(ctx, Model))
                    {
                        return View("NotYourItem");
                    }
                    ViewBag.Users = GetUserItems(ctx);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
           
            return View("..\\Item\\Edit", Model);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BusinessLogicLayer.OwnedItemBLL collection)
        {
            try
            {
                
            using (ContextBLL ctx = new ContextBLL())
                {
                    OwnedItemBLL Mine = ctx.FindOwnedItemByID(collection.OwnedItemID);
                    if (null == Mine)
                    {
                        return View("ItemNotFound"); 
                    }
                    if (!IsThisMine(ctx, Mine))
                    {
                        return View("NotYourItem");
                    }
               
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
            OwnedItemBLL Model;
            try
            {
                using (ContextBLL ctx = new ContextBLL())
                {
                    Model = ctx.FindOwnedItemByID(id);

                    if (null == Model)
                    {
                        return View("ItemNotFound"); // BKW make this view
                    }
                    if (!IsThisMine(ctx, Model))
                    {
                        return View("NotYourItem");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }
            return View("..\\Item\\Delete", Model);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BusinessLogicLayer.OwnedItemBLL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (ContextBLL ctx = new ContextBLL())
                {
                    OwnedItemBLL Mine = ctx.FindOwnedItemByID(id);
                    if (null == Mine)
                    {
                        return View("ItemNotFound"); 

                    }
                    if (!IsThisMine(ctx, Mine))
                    {
                        return View("NotYourItem");
                    }
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
                    UserBLL ThisUser = ctx.FindUserByEMail(User.Identity.Name);
                    if (null == ThisUser)
                    {
                        ViewBag.Exception = new Exception($"Could Not Find Record for User: {User.Identity.Name}");
                        return View("Error");
                    }
                    int TotalCount = ctx.ObtainCountOfItemsOwnedByUser(ThisUser.UserID);
                   Items = ctx.GetOwnedItemsRelatedToUser(ThisUser.UserID, 0,TotalCount);


                    MeaningfulCalculator mc = new MeaningfulCalculator();
                    Model = mc.ComputeStats(Items);

                }
                return View("..\\Item\\Stats", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Error");
            }

        }

    }
}