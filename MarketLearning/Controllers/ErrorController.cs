using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarketLearning.Models;
using MarketLearning.Models.Entities;

namespace MarketLearning.Controllers
{
    public class ErrorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Error
        public ActionResult Index()
        {
            return View(db.Errors.ToList());
        }

        // GET: Error/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Error error = db.Errors.Find(id);
            if (error == null)
            {
                return HttpNotFound();
            }
            return View(error);
        }

        // GET: Error/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Error/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Occurance,Username,Message,Location,ClientSide")] Error error)
        {
            if (ModelState.IsValid)
            {
                db.Errors.Add(error);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(error);
        }

        // GET: Error/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Error error = db.Errors.Find(id);
            if (error == null)
            {
                return HttpNotFound();
            }
            return View(error);
        }

        // POST: Error/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Occurance,Username,Message,Location,ClientSide")] Error error)
        {
            if (ModelState.IsValid)
            {
                db.Entry(error).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(error);
        }

        // GET: Error/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Error error = db.Errors.Find(id);
            if (error == null)
            {
                return HttpNotFound();
            }
            return View(error);
        }

        // POST: Error/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Error error = db.Errors.Find(id);
            db.Errors.Remove(error);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
