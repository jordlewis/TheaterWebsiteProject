﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Mvc;
using TheatreCMS3.Areas.Rent.Models;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Rent.Controllers
{
    public class RentalItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent/RentalItems
        public ActionResult Index(string searchString)
        {

            return View(db.RentalItems.Where(x => x.Item.Contains(searchString) || x.ItemDescription.Contains(searchString) || searchString == null).ToList());
        }

        // GET: Rent/RentalItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalItem rentalItem = db.RentalItems.Find(id);
            if (rentalItem == null)
            {
                return HttpNotFound();
            }
            return View(rentalItem);
        }

        // GET: Rent/RentalItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rent/RentalItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalItemId,Item,ItemDescription,PickupDate,ReturnDate,ItemPhoto")] RentalItem rentalItem, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    rentalItem.ItemPhoto = ImageToByte(imageFile);
                    db.RentalItems.Add(rentalItem);
                }
                else
                {
                    db.RentalItems.Add(rentalItem);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentalItem);
        }

        // GET: Rent/RentalItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalItem rentalItem = db.RentalItems.Find(id);
            if (rentalItem == null)
            {
                return HttpNotFound();
            }
            return View(rentalItem);
        }

        // POST: Rent/RentalItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalItemId,Item,ItemDescription,PickupDate,ReturnDate,ItemPhoto")] RentalItem rentalItem, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    db.Entry(rentalItem).State = EntityState.Modified;
                    rentalItem.ItemPhoto = ImageToByte(imageFile);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentalItem);
        }

        // GET: Rent/RentalItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalItem rentalItem = db.RentalItems.Find(id);
            if (rentalItem == null)
            {
                return HttpNotFound();
            }
            return View(rentalItem);
        }

        // POST: Rent/RentalItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalItem rentalItem = db.RentalItems.Find(id);
            db.RentalItems.Remove(rentalItem);
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


        // Gets uploaded img file and converts it into byte[].
        public byte[] ImageToByte(HttpPostedFileBase imageFile)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(imageFile.InputStream))
            {
                bytes = br.ReadBytes(imageFile.ContentLength);
            }
            return bytes;
        }

        // Retrieves the stored byte[] from entity in RentalItem table/db using id.
        public byte[] GetImageFromDB(int id)
        {
            RentalItem item = db.RentalItems.Find(id);
            byte[] itemPhoto = item.ItemPhoto;
            return itemPhoto;
        }

        //Retrieve and display image from db using id.
        public ActionResult DisplayImage(RentalItem id)
        {
            RentalItem item = db.RentalItems.Find(id.RentalItemId);
            byte[] image = GetImageFromDB(item.RentalItemId);
            if (image != null)
            {
                return File(image, "image/png");
            }
            else
            {
                return null;
            }
        }
    }
}
