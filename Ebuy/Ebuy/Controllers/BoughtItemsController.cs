using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ebuy.Models;

namespace Ebuy.Controllers
{
    public class BoughtItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /* GET: BoughtItems
         * 'title' is userId, 
         *  value is 'purchase'/'sold', based on what button is pressed.
        */
        [Authorize]
        public ActionResult Index(string title, string value)
        {


            //For purchase history
            if (title != null && value == "purchase")
            {
                Debug.WriteLine("------------inside bought, title: " + title);
                var boughtItems = db.BoughtItems.Where(b => b.UserId.Equals(title));

                return View(boughtItems.ToList());
            }
            //for selling history
            if (title != null && value == "sold")
            {
                Debug.WriteLine("---------------inside sold, title: " + title);
                var soldtItems = db.BoughtItems.Where(b => b.Item.UserId.Equals(title));
                return View(soldtItems.ToList());
            }

            //Base case
            return View();


        }

        // GET: BoughtItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoughtItem boughtItem = db.BoughtItems.Find(id);
            if (boughtItem == null)
            {
                return HttpNotFound();
            }
            return View(boughtItem);
        }

        /* GET: BoughtItems/Create
         * Create new boughtItem then redirect to ->
         * BoughtItems -> Index with parameters; UserId, "purchase"
         * to go to "my purchases" after a puchase.
         * 
         */
         //TODO: Fix so that item.quantity is negated.
        [Authorize]
        public ActionResult Create(int boughtItemId, string buyer)
        {
            
            BoughtItem bi = new BoughtItem();
            bi.ItemId = boughtItemId;
            bi.UserId = buyer;

            db.BoughtItems.Add(bi);
            db.SaveChanges();

            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ItemName");
            return RedirectToAction("Index", new { title= buyer,  value = "purchase"});
        }

        // POST: BoughtItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BoughtItem boughtItem)
        {
            if (ModelState.IsValid)
            {

                db.BoughtItems.Add(boughtItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ItemName", boughtItem.ItemId);
            return View(boughtItem);
        }

        // GET: BoughtItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoughtItem boughtItem = db.BoughtItems.Find(id);
            if (boughtItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ItemName", boughtItem.ItemId);
            return View(boughtItem);
        }

        // POST: BoughtItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BoughtItemId,UserId,ItemId")] BoughtItem boughtItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boughtItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ItemName", boughtItem.ItemId);
            return View(boughtItem);
        }

        // GET: BoughtItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoughtItem boughtItem = db.BoughtItems.Find(id);
            if (boughtItem == null)
            {
                return HttpNotFound();
            }
            return View(boughtItem);
        }

        // POST: BoughtItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoughtItem boughtItem = db.BoughtItems.Find(id);
            db.BoughtItems.Remove(boughtItem);
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
