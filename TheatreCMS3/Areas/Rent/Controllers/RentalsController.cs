﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Areas.Rent.Models;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Rent.Controllers
{
    public class RentalsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent/Rentals
        public ActionResult Index()
        {
            return View(db.Rentals.ToList());
        }

        // GET: Rent/Rentals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }


        // GET: Rent/Rentals/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Rent/Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "RentalId,RentalName,RentalCost,FlawsAndDamages,ChokingHazard,SuffocationHazard,PurchasePrice,RoomNumber,SquareFootage,MaxOccupancy")] Rental rental,
            RentalEquipment rentalEquipment,
            RentalRoom rentalRoom)
        {
            if (rentalEquipment.PurchasePrice != null)
            {
                var rentalEquipments = new RentalEquipment
                {
                    //RentalId = rental.RentalId,
                    RentalName = rental.RentalName,
                    RentalCost = rental.RentalCost,
                    FlawsAndDamages = rental.FlawsAndDamages,
                    ChokingHazard = rentalEquipment.ChokingHazard,
                    SuffocationHazard = rentalEquipment.SuffocationHazard,
                    PurchasePrice = rentalEquipment.PurchasePrice
            };

                db.Rentals.Add(rentalEquipments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (rentalRoom.RoomNumber != null)
            {
                var rentalRooms = new RentalRoom
                {
                    //RentalId = rental.RentalId,
                    RentalName = rental.RentalName,
                    RentalCost = rental.RentalCost,
                    FlawsAndDamages = rental.FlawsAndDamages,
                    RoomNumber = rentalRoom.RoomNumber,
                    SquareFootage = rentalRoom.SquareFootage,
                    MaxOccupancy = rentalRoom.MaxOccupancy
                };

                db.Rentals.Add(rentalRooms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (rentalRoom.RoomNumber == null && rentalEquipment.PurchasePrice == null)
            {
                db.Rentals.Add(rental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(rental);
        }


        // GET: Rent/Rentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            TempData["previous"] = rental;
            return View(rental);


        }


        // POST: Rent/Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalId,RentalName,RentalCost,FlawsAndDamages")] Rental rental, int? PurchasePrice, bool? ChokingHazard, bool? SuffocationHazard,
            int? RoomNumber, int? SquareFootage, int? MaxOccupancy)
        {
            if (PurchasePrice > 0)
            {

                RentalEquipment rentalEquipment = new RentalEquipment();
                rentalEquipment.RentalId = rental.RentalId;

                rentalEquipment.RentalName = rental.RentalName;
                rentalEquipment.RentalCost = rental.RentalCost;
                rentalEquipment.FlawsAndDamages = rental.FlawsAndDamages;
                rentalEquipment.ChokingHazard = Convert.ToBoolean(ChokingHazard);
                rentalEquipment.SuffocationHazard = Convert.ToBoolean(SuffocationHazard);
                rentalEquipment.PurchasePrice = Convert.ToInt32(PurchasePrice);

                db.Entry(rentalEquipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (RoomNumber > 0)
            {
                RentalRoom rentalRoom = new RentalRoom();
                rentalRoom.RentalId = rental.RentalId;

                rentalRoom.RentalName = rental.RentalName;
                rentalRoom.RentalCost = rental.RentalCost;
                rentalRoom.FlawsAndDamages = rental.FlawsAndDamages;
                rentalRoom.RoomNumber = Convert.ToInt32(RoomNumber);
                rentalRoom.SquareFootage = Convert.ToInt32(SquareFootage);
                rentalRoom.MaxOccupancy = Convert.ToInt32(MaxOccupancy);

                db.Entry(rentalRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.Entry(rental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rental);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(
        //    [Bind(Include = "RentalId,RentalName,RentalCost,FlawsAndDamages,ChokingHazard,SuffocationHazard,PurchasePrice,RoomNumber,SquareFootage,MaxOccupancy")] Rental rental, RentalEquipment rentalEquipment, RentalRoom rentalRoom)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //RentalEquipment previousEntryEq = (RentalEquipment)(TempData["previous"] ?? db.Rentals.Find(rental.RentalId));

        //        //RentalRoom previousEntryRm = (RentalRoom)(TempData["previous"] ?? db.Rentals.Find(rental.RentalId));

        //        //Rental previousEntryR = (Rental)(TempData["previous"] == null ? db.Rentals.Find(rental.RentalId) :
        //        //    (Rental)TempData["previous"]);

        //        //,ChokingHazard,SuffocationHazard,PurchasePrice,RoomNumber,SquareFootage,MaxOccupancy"   bool? chokingHazard, bool? suffocationHazard, int? purchasePrice, int? roomNumber, int? squareFootage, int? maxOccupancy
        //        //while(Convert.ToBoolean(ERentalType.RentalEquipment))

        //        if (rentalEquipment.PurchasePrice != null)
        //        {
        //            var rentalEquipment = new RentalEquipment
        //            {
        //                RentalId = rental.RentalId,
        //                RentalName = rental.RentalName,
        //                RentalCost = rental.RentalCost,
        //                FlawsAndDamages = rental.FlawsAndDamages,
        //                ChokingHazard = rentalEquipment.ChokingHazard,
        //                //SuffocationHazard = Convert.ToBoolean(suffocationHazard),
        //                //PurchasePrice = Convert.ToInt32(purchasePrice)
        //                SuffocationHazard = rentalEquipment.SuffocationHazard,
        //                PurchasePrice = rentalEquipment.PurchasePrice
        //            };

        //            //previousEntryEq.ChokingHazard = rentalEquipments.ChokingHazard;
        //            //previousEntryEq.SuffocationHazard = rentalEquipments.SuffocationHazard;
        //            //previousEntryEq.PurchasePrice = rentalEquipments.PurchasePrice;

        //            db.Entry(rentalEquipment).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");


        //        }

        //        if (rentalRoom.RoomNumber != null)
        //        //while(Convert.ToBoolean(ERentalType.RentalRoom))
        //        {
        //            var rentalRooms = new RentalRoom
        //            {
        //                RentalName = rental.RentalName,
        //                RentalCost = rental.RentalCost,
        //                FlawsAndDamages = rental.FlawsAndDamages,
        //                RoomNumber = rentalRoom.RoomNumber,
        //                SquareFootage = rentalRoom.SquareFootage,
        //                MaxOccupancy = rentalRoom.MaxOccupancy
        //            };

        //            //previousEntryRm.RoomNumber = rentalRoom.RoomNumber;
        //            //previousEntryRm.SquareFootage = rentalRoom.SquareFootage;
        //            //previousEntryRm.MaxOccupancy = rentalRoom.MaxOccupancy;

        //            db.Entry(rentalRooms).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        if (rentalRoom.RoomNumber == null && rentalEquipment.PurchasePrice == null)
        //        {
        //            db.Entry(rental).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(rental);
        //}

        // GET: Rent/Rentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rent/Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = db.Rentals.Find(id);
            db.Rentals.Remove(rental);
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
