using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HolidayRoutings.Models;

namespace HolidayRoutings.Controllers
{
    [Authorize(Users = @"VDL\angelod,VDL\sdrentje,VDL\carlab")]
    public class holidaysController : Controller
    {
        private vdlwebcontrolEntities3 db = new vdlwebcontrolEntities3();

        // GET: holidays/Create
        public ActionResult Create(int? id)
        {
            vl_HRC_HBC_ASSIGNED_HOLIDAYS holidays = new vl_HRC_HBC_ASSIGNED_HOLIDAYS();
            holidays.year_related = DateTime.Now.Year;
            ViewBag.name = db.t__users.Find(id).user_name;
            ViewBag.user_id = id;
            ViewBag.id = new SelectList(db.t__users, "id", "user_name", id);
            ViewBag.mandatory = new List<SelectListItem> {
                new SelectListItem { Value="0", Text="Não" },
                new SelectListItem { Value="1", Text="Sim" },
            };

            return View(holidays);
        }

        // POST: holidays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,year_related,total_days,bonus_days,weekends_allowed,half_fridays,first_year")] vl_HRC_HBC_ASSIGNED_HOLIDAYS vl_HRC_HBC_ASSIGNED_HOLIDAYS)
        {
            var what = db.vl_HRC_HBC_ASSIGNED_HOLIDAYS.FirstOrDefault(x => x.id == vl_HRC_HBC_ASSIGNED_HOLIDAYS.id && x.year_related == vl_HRC_HBC_ASSIGNED_HOLIDAYS.year_related);
            if (db.vl_HRC_HBC_ASSIGNED_HOLIDAYS.FirstOrDefault(x => x.id == vl_HRC_HBC_ASSIGNED_HOLIDAYS.id && x.year_related == vl_HRC_HBC_ASSIGNED_HOLIDAYS.year_related) != null)
            {
                ModelState.AddModelError("year_related", "Ano já existe.");
            }

            if (ModelState.IsValid)
            {
                db.vl_HRC_HBC_ASSIGNED_HOLIDAYS.Add(vl_HRC_HBC_ASSIGNED_HOLIDAYS);
                db.SaveChanges();
                return RedirectToAction("Details", "Users", new { id = vl_HRC_HBC_ASSIGNED_HOLIDAYS.id });
            }

            ViewBag.user_id = vl_HRC_HBC_ASSIGNED_HOLIDAYS.id;
            ViewBag.mandatory = new List<SelectListItem> {
                new SelectListItem { Value="0", Text="Não" },
                new SelectListItem { Value="1", Text="Sim" },
            };
            return View(vl_HRC_HBC_ASSIGNED_HOLIDAYS);
        }

        // GET: holidays/Edit/5
        public ActionResult Edit(int id, int year, string name)
        {
            ViewBag.name = name;
            vl_HRC_HBC_ASSIGNED_HOLIDAYS vl_HRC_HBC_ASSIGNED_HOLIDAYS = db.vl_HRC_HBC_ASSIGNED_HOLIDAYS.FirstOrDefault(x => x.id == id && x.year_related == year);
            if (vl_HRC_HBC_ASSIGNED_HOLIDAYS == null)
            {
                return HttpNotFound();
            }

            return View(vl_HRC_HBC_ASSIGNED_HOLIDAYS);
        }

        // POST: holidays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,year_related,total_days,bonus_days,weekends_allowed,half_fridays,first_year")] vl_HRC_HBC_ASSIGNED_HOLIDAYS vl_HRC_HBC_ASSIGNED_HOLIDAYS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vl_HRC_HBC_ASSIGNED_HOLIDAYS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Users", new { id = vl_HRC_HBC_ASSIGNED_HOLIDAYS.id });
            }

            return View(vl_HRC_HBC_ASSIGNED_HOLIDAYS);
        }

        // GET: holidays/Delete/5
        public ActionResult Delete(int id, int year, string name)
        {
            vl_HRC_HBC_ASSIGNED_HOLIDAYS vl_HRC_HBC_ASSIGNED_HOLIDAYS = db.vl_HRC_HBC_ASSIGNED_HOLIDAYS.FirstOrDefault(x => x.id == id && x.year_related == year);
            ViewBag.user_name = name;
            if (vl_HRC_HBC_ASSIGNED_HOLIDAYS == null)
            {
                return HttpNotFound();
            }
            return View(vl_HRC_HBC_ASSIGNED_HOLIDAYS);
        }

        // POST: holidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int year)
        {
            vl_HRC_HBC_ASSIGNED_HOLIDAYS vl_HRC_HBC_ASSIGNED_HOLIDAYS = db.vl_HRC_HBC_ASSIGNED_HOLIDAYS.FirstOrDefault(x => x.id == id && x.year_related == year);
            int user_id = vl_HRC_HBC_ASSIGNED_HOLIDAYS.id;
            db.vl_HRC_HBC_ASSIGNED_HOLIDAYS.Remove(vl_HRC_HBC_ASSIGNED_HOLIDAYS);
            db.SaveChanges();
            return RedirectToAction("Details", "Users", new { id = user_id });
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
