﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Areas.Prod.Models;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Prod.Controllers
{
    public class CalendarEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Prod/CalendarEvents
        public ActionResult Index()
        {
            return View(db.CalendarEventsModels.ToList());
        }

        // GET: Prod/CalendarEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalendarEventsModels calendarEventsModels = db.CalendarEventsModels.Find(id);
            if (calendarEventsModels == null)
            {
                return HttpNotFound();
            }
            return View(calendarEventsModels);
        }

        // GET: Prod/CalendarEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prod/CalendarEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,Title,StartDate,EndDate,StartTime,EndTime,AllDay,TicketsAvailable,isProduction")] CalendarEventsModels calendarEventsModels)
        {
            if (ModelState.IsValid)
            {
                db.CalendarEventsModels.Add(calendarEventsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(calendarEventsModels);
        }

        // GET: Prod/CalendarEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalendarEventsModels calendarEventsModels = db.CalendarEventsModels.Find(id);
            if (calendarEventsModels == null)
            {
                return HttpNotFound();
            }
            return View(calendarEventsModels);
        }

        // POST: Prod/CalendarEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,Title,StartDate,EndDate,StartTime,EndTime,AllDay,TicketsAvailable,isProduction")] CalendarEventsModels calendarEventsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calendarEventsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calendarEventsModels);
        }

        // GET: Prod/CalendarEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalendarEventsModels calendarEventsModels = db.CalendarEventsModels.Find(id);
            if (calendarEventsModels == null)
            {
                return HttpNotFound();
            }
            return View(calendarEventsModels);
        }

        // POST: Prod/CalendarEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CalendarEventsModels calendarEventsModels = db.CalendarEventsModels.Find(id);
            db.CalendarEventsModels.Remove(calendarEventsModels);
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