using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ebuy.Models;

namespace Ebuy.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /* GET: Items
         * Index, search-based in accordance to lab 2
         * If a string is input (logged in userID),
         * return a list based on only their ads.
         */
        public ActionResult Index(string title)
        {
            
            if (!String.IsNullOrEmpty(title))
            {
                var usersItems = db.Items.Where(s => s.UserId.Equals(title));
                return View(usersItems.ToList());
            }
            else
            {
                return View(db.Items.ToList());
            }
            
        }

        public ActionResult MyProducts(string title)
        {

            if (!String.IsNullOrEmpty(title))
            {
                var usersItems = db.Items.Where(s => s.UserId.Equals(title));
                return View(usersItems.ToList());
            }
            else
            {
                return View(db.Items.ToList());
            }

        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }


        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {

            //Credit for file upload;
            //https://www.c-sharpcorner.com/article/asp-net-mvc-form-with-file-upload/

            //Use Namespace called :  System.IO  
            string FileName = Path.GetFileNameWithoutExtension(item.ImageFile.FileName);

            //To Get File Extension  
            string FileExtension = Path.GetExtension(item.ImageFile.FileName);

            //Add Current Date To Attached File Name  
            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;


            string path = System.IO.Path.Combine(
                                   Server.MapPath("~/Images/"), FileName);

            string pathForDb = "../../Images/" + FileName;


            //To copy and save file into server.  
            item.ImageFile.SaveAs(path);

            //To save Club Member Contact Form Detail to database table.  
            var db = new ApplicationDbContext();

            //lazy object creation
            Item newItem = new Item();

            newItem.ImagePath = pathForDb;
            newItem.ItemName = item.ItemName;
            newItem.Category = item.Category;
            newItem.Price = item.Price;
            newItem.Location = item.Location;
            newItem.Quantity = item.Quantity;
            newItem.UserId = item.UserId;
            newItem.Description = item.Description;

            //save to db
            db.Items.Add(newItem);
            db.SaveChanges();

            //Redirect to index function, with the userId for created item as parameter
            //this results in 'my products' - page.
            return RedirectToAction("Index", new { title = item.UserId});

        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ItemName,Category,Price,Description,Location,Quantity,UserId,ImagePath")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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


        public FileResult getFile()
        {

            return File("~/App_Data/currencytext.txt", System.Net.Mime.MediaTypeNames.Text.Plain);

        }
    }
}
