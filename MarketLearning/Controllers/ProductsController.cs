using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MarketLearning.Models;
using MarketLearning.Models.Entities;
using MarketLearning.Repositories;
using MarketLearning.Services;

namespace MarketLearning.Controllers
{
    public class ProductsController : Controller
    {

        ProductRepository repo = new ProductRepository();
        ProductService service = new ProductService();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            ViewBag.HighlightTab = "products";
            try
            {
                var result = await service.GetAllProducts();
                result = result.OrderBy(p => p.Name).ToList();
                return View(result);
            }
            catch (Exception e)
            {
                //await errorService.ErrorHandler(e, User.Identity.Name);
            }
            return null;
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.HighlightTab = "Products";
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = await service.GetProductByID(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            catch (Exception e)
            {
                //await errorService.ErrorHandler(e, User.Identity.Name);
            }
            return null;
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.HighlightTab = "products";

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Code,Info")]Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await service.AddProduct(product);
                    return Json(new { isSuccessful = true });
                }
                return Json(new { isSuccessful = false });
            }
            catch (Exception e)
            {
                //await errorService.ErrorHandler(e, User.Identity.Name);
            }
            return null;
        }

        // GET: Products/Edit/5
  
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = await service.GetProductByID(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            catch (Exception e)
            {
                //await errorService.ErrorHandler(e, User.Identity.Name);
            }
            return null;
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Code,Info")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await service.SaveUpdatedState(product);
                    return Json(new { isSuccessful = true });
                }
                return Json(new { isSuccessful = false });
            }
            catch (Exception e)
            {
                //await errorService.ErrorHandler(e, User.Identity.Name);
            }
            return null;
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = await service.GetProductByID(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            catch (Exception e)
            {
                //await errorService.ErrorHandler(e, User.Identity.Name);
            }
            return null;
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await service.DeleteProduct(id);
                return Json(new { isSuccessful = true });
            }
            catch (Exception e)
            {
                //await errorService.ErrorHandler(e, User.Identity.Name);
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
