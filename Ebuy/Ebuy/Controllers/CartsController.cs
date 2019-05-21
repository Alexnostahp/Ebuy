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
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carts
        [Authorize]
        public ActionResult Index(string buyerID, int? itemID)
        {

            //to avoid bug with 'go to cart' instead of 'add to cart'.
            if (itemID != null)
            {
                Cart newCart = new Cart();

                newCart.UserId = buyerID;

                var addedItem = db.Items.Find(itemID);
                newCart.Item = addedItem;

                db.Carts.Add(newCart);
                db.SaveChanges();
            }



            //Get the current cart(s) of this user
            var userCart = db.Carts.Where(s => s.UserId.Equals(buyerID));
            return View(userCart.ToList());
        }


        // GET: Carts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        public ActionResult BuyAll(string userID)
        {

            var cart = db.Carts.Where(s => s.UserId.Equals(userID)).ToList();
            Item item = null;
            BoughtItem bi = null;

            foreach (Cart temp in cart)
            {

                item = temp.Item;
                bi = new BoughtItem();
                bi.ItemId = temp.ItemId;
                bi.UserId = userID;
                item.Quantity -= 1;

                db.BoughtItems.Add(bi);
                db.Carts.Remove(temp);
                item = null;
                bi = null;

            }
            db.SaveChanges();

            return RedirectToAction("Index", "Home");


            return RedirectToAction("Index", "Home");
        }


        public ActionResult DeleteAll(string userID)
        {

            var cart = db.Carts.Where(s => s.UserId.Equals(userID));
            foreach (Cart temp in cart)
            {
                db.Carts.Remove(temp);

            }
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string userID)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
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
