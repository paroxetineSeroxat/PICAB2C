using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TB.Business.Interfaces;
using TB.Business.PurchaseOrder;
using TB.Domain.BE;
using TB.Web.Authentication;

namespace TB.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {
        // GET: PurchaseOrder
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                CustomIdentity customIdentity = null;
                if (ControllerContext.HttpContext.User.Identity.IsAuthenticated)
                    customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
                string username = string.Empty;
                if (customIdentity != null)
                    username = customIdentity.Name;

                IPurchaseOrder purchaseOrderBO = new PurchaseOrderBO(username);
               

                List<PurchaseOrder> list = purchaseOrderBO.GetPurchaseOrderbyCustomerId(customIdentity.Id);



                return View(list);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        [Authorize]
        // GET: PurchaseOrder/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                CustomIdentity customIdentity = null;
                if (ControllerContext.HttpContext.User.Identity.IsAuthenticated)
                    customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
                string username = string.Empty;
                if (customIdentity != null)
                    username = customIdentity.Name;

                IPurchaseOrder purchaseOrderBO = new PurchaseOrderBO(username);


                PurchaseOrderDetail purchaseOrderDetail = purchaseOrderBO.GetPurchaseOrderDetailById(id);



                return View(purchaseOrderDetail);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        // GET: PurchaseOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseOrder/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchaseOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PurchaseOrder/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: PurchaseOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
