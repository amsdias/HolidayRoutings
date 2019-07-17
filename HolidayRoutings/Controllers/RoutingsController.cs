using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HolidayRoutings.Models;

namespace HolidayRoutings.Controllers
{
    [Authorize(Users = @"VDL\angelod,VDL\sdrentje,VDL\carlab")]
    public class RoutingsController : Controller
    {
        private vdlwebcontrolEntities3 db = new vdlwebcontrolEntities3();

        // GET: Routings/Create
        public ActionResult Create(int? id)
        {
            ViewBag.user_id = new SelectList(db.t__users.Where(x => x.status == 1).OrderBy(x => x.user_name), "id", "user_name", id);
            ViewBag.id = id;
            ViewBag.user_name = db.t__users.Find(id).user_name;
            ViewBag.person_in_charge_id = new SelectList(db.t__users.Where(x => x.status == 1).OrderBy(x => x.user_name), "id", "user_name");

            ViewBag.mandatory = new List<SelectListItem> {
                new SelectListItem { Value="0", Text="Não" },
                new SelectListItem { Value="1", Text="Sim" },                
            };

            return View();
        }

        // POST: Routings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,person_in_charge_id,mandatory")] vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING)
        {
            if (db.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.Find(vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_id, vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.person_in_charge_id) != null)
            {
                ModelState.AddModelError("person_in_charge_id", "Routing já existe.");
            }

            if (ModelState.IsValid)
            {
                db.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.Add(vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING);
                db.SaveChanges();
                return RedirectToAction("Details", "Users", new { id = vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_id });
            }

            ViewBag.user_id = new SelectList(db.t__users.Where(x => x.status == 1).OrderBy(x => x.user_name), "id", "user_name", vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_id);
            ViewBag.person_in_charge_id = new SelectList(db.t__users.Where(x => x.status == 1).OrderBy(x => x.user_name), "id", "user_name", vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.person_in_charge_id);
            ViewBag.mandatory = new List<SelectListItem> {
                new SelectListItem { Value="0", Text="Não" },
                new SelectListItem { Value="1", Text="Sim" },
            };
            return View(vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING);
        }

        // GET: Routings/Edit/5
        public ActionResult Edit(int user_id, int person_in_charge_id)
        {
            vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING = db.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.Find(user_id, person_in_charge_id);
            vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_name = db.t__users.Find(user_id).user_name;
            vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.person_in_charge_name = db.t__users.Find(person_in_charge_id).user_name;
            ViewBag.old_person_in_charge_id = person_in_charge_id;

            if (vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING == null)
            {
                return HttpNotFound();
            }
 
            ViewBag.user_id = new SelectList(db.t__users.Where(x => x.status == 1).OrderBy(x => x.user_name), "id", "user_name", vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_id);
            ViewBag.person_in_charge_id = new SelectList(db.t__users.Where(x => x.status == 1).OrderBy(x => x.user_name), "id", "user_name", vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.person_in_charge_id);

            return View(vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING);
        }

        // POST: Routings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_id,person_in_charge_id,mandatory")] vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING, int old_person)
        {
            if(db.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.Find(vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_id, vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.person_in_charge_id) != null)
            {
                ModelState.AddModelError("person_in_charge_id", "Routing já existe.");
            }

            if (ModelState.IsValid)
            {
                int? user_id = vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_id;
                //db.Entry(vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING).State = EntityState.Modified;
                var what = db.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.SqlQuery("SELECT * FROM dbo.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING WHERE user_id = 768").ToList();
                var command = "UPDATE dbo.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING SET person_in_charge_id = " + vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.person_in_charge_id + " WHERE user_id = " + vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_id + " AND person_in_charge_id = " + old_person;
                var result = db.Database.ExecuteSqlCommand(command);
                if(result == 1)
                {

                }

                return RedirectToAction("Details", "Users", new { id = user_id });
            }

            ViewBag.user_id = new SelectList(db.t__users.Where(x => x.status == 1).OrderBy(x => x.user_name), "id", "user_name", vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_id);
            ViewBag.person_in_charge_id = new SelectList(db.t__users.Where(x => x.status == 1).OrderBy(x => x.user_name), "id", "user_name", vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.person_in_charge_id);
            return View(vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING);
        }

        // GET: Routings/Delete/5
        public ActionResult Delete(int user_id, int person_in_charge_id)
        {
            vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING = db.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.Find(user_id, person_in_charge_id);
            if (vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING == null)
            {
                return HttpNotFound();
            }
            vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.user_name = db.t__users.Find(user_id).user_name;
            vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.person_in_charge_name = db.t__users.Find(person_in_charge_id).user_name;
            return View(vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING);
        }

        // POST: Routings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int user_id, int person_in_charge_id)
        {
            vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING = db.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.Find(user_id, person_in_charge_id);

            db.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.Remove(vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING);
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
