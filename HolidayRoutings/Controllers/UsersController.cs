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
    public class UsersController : Controller
    {
        private readonly vdlwebcontrolEntities3 db = new vdlwebcontrolEntities3();

        // GET: Users
        public ActionResult Index(string searchString)
        {            
            IEnumerable<t__users> t__users_list = db.t__users.Where(x => x.status == 1).OrderBy(y => y.user_name).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                t__users_list = t__users_list.Where(x => x.user_name.ToLower().Contains(searchString));
            }
            return View(t__users_list);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            t__users t__user = db.t__users.Find(id);
            List<vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING> routingsList = db.vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING.Where(x => x.user_id == id).ToList();

            foreach (vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING item in routingsList)
            {
                item.person_in_charge_name = db.t__users.Find(item.person_in_charge_id).user_name;
            }

            List<vl_HRC_HBC_ASSIGNED_HOLIDAYS> holydaysList = db.vl_HRC_HBC_ASSIGNED_HOLIDAYS.Where(x => x.id == id).ToList();

            if (t__user == null)
            {
                return HttpNotFound();
            }

            var viewModel = new UserDetailsViewModel { User = t__user, routingsList = routingsList, holidaysList = holydaysList };
            return View(viewModel);
        }

        // GET: t__usersController2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            t__users t__users = db.t__users.Find(id);
            if (t__users == null)
            {
                return HttpNotFound();
            }
            ViewBag.comp_id = new SelectList(db.t__companies, "id", "CompDescription", t__users.comp_id);
            ViewBag.dep_id = new SelectList(db.t__departments, "id", "department_pt", t__users.dep_id);
            return View(t__users);
        }

        // POST: t__usersController2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_logon,user_pass,user_pass_temp,user_name,user_alias,user_alias_pt,user_alias_en,user_alias_de,user_title_pt,user_title_en,user_title_de,user_email,user_pc_name,user_phone_ext,user_phone_1,user_phone_2,user_phone_3,user_phone_4,user_phone_5,user_lang,user_gender,dep_id,comp_id,toner_requests,valor_hora,valor_hora_custo,pcs_enabled,pcs_user_level,pcs_results_per_page,hbc_enabled,hbc_user_id,hbc_user_level,hbc_routing,hbc_check_users,hbc_results_per_page,mas_enabled,mas_user_level,mas_results_per_page,dil_enabled,dil_user_level,dil_results_per_page,dil_language,dil_permissions,wcm_enabled,wcm_user_level,wcm_results_per_page,wcm_editable_pages,vcs_enabled,vcs_user_level,vcs_results_per_page,tas_enabled,tas_user_level,tas_results_per_page,oe_enabled,ir_oe_user_level,ir_oe_enabled,ir_oe_results_per_page,ppr_print,ppr_level,bdg_enabled,bdg_user_level,prop_enabled,pcr_enabled,evc_dep_enabled,gir_enabled,rhhe_comp_id,rhhe_enabled,rhhe_user_id,isa_enabled,isa_user_level,isa_results_per_page,isa_language,ecf_enabled,ecf_user_level,ecf_commisions,VendorNo,villalist_enabled,villalist_user_level,rca_enabled,rca_user_level,rca_dep,hsk_app_enabled,hsk_app_user_level,prs_enabled,prs_user_level,cms_enabled,cms_user_level,addressbook,login_date,login_ip,insert_date,insert_author,update_date,update_author,status")] t__users t__users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t__users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.comp_id = new SelectList(db.t__companies, "id", "CompDescription", t__users.comp_id);
            ViewBag.dep_id = new SelectList(db.t__departments, "id", "department_pt", t__users.dep_id);
            return View(t__users);
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
