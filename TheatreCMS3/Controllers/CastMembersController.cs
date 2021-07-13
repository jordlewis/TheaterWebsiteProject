﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Models;

namespace TheatreCMS3.Controllers
{
    public class CastMembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CastMembers
        public ActionResult Index()
        {
            return View(db.CastMembers.ToList());
        }

        // GET: CastMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastMembers castMembers = db.CastMembers.Find(id);
            if (castMembers == null)
            {
                return HttpNotFound();
            }
            return View(castMembers);
        }

        // GET: CastMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CastMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CastMemberId,Name,YearJoined,MainRole,Bio,Photo,CurrenMember,Character,CastYearLeft,DebutYear")] CastMembers castMembers)
        {
            if (ModelState.IsValid)
            {
                db.CastMembers.Add(castMembers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(castMembers);
        }

        // GET: CastMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastMembers castMembers = db.CastMembers.Find(id);
            if (castMembers == null)
            {
                return HttpNotFound();
            }
            return View(castMembers);
        }

        // POST: CastMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CastMemberId,Name,YearJoined,MainRole,Bio,Photo,CurrenMember,Character,CastYearLeft,DebutYear")] CastMembers castMembers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(castMembers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(castMembers);
        }

        // GET: CastMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastMembers castMembers = db.CastMembers.Find(id);
            if (castMembers == null)
            {
                return HttpNotFound();
            }
            return View(castMembers);
        }

        // POST: CastMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CastMembers castMembers = db.CastMembers.Find(id);
            db.CastMembers.Remove(castMembers);
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