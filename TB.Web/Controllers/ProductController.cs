using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TB.Business.Product;
using TB.Cache.Product;
using TB.Domain.BE;
using TB.Web.Authentication;

namespace TB.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string SearchWord)
        {
            try
            {
                CustomIdentity customIdentity = null;
                if (ControllerContext.HttpContext.User.Identity.IsAuthenticated)
                    customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
                string username = string.Empty;
                if (customIdentity != null)
                    username = customIdentity.Name;

                //ProductBO productBO = new ProductBO(username);
                ProductCache cache = new ProductCache();
                List<Product> list = (List<Product>)cache.GetAvailableProducts(username);//productBO.GetCampains();


                //productBO.GetProductsByName(SearchWord);


                return View(list.Where(x => x.ProductName.ToLower().Contains(SearchWord.ToLower()) || x.ProductDescription.ToLower().Contains(SearchWord.ToLower())).ToList());
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [Authorize]
        public ActionResult ProductCode(string code)
        {
            try
            {
                CustomIdentity customIdentity = (CustomIdentity)ControllerContext.HttpContext.User.Identity;
                if (string.IsNullOrEmpty(code))
                    return View(new Product());
                else
                { 
                    ProductBO productBO = new ProductBO(customIdentity.Name);

                    Product product = productBO.GetProductsByCode(code);

                    return View(product);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /*
        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
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

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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
        */
    }
}
