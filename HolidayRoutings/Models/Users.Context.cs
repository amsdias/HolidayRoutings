﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HolidayRoutings.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class vdlwebcontrolEntities3 : DbContext
    {
        public vdlwebcontrolEntities3()
            : base("name=vdlwebcontrolEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<t__users> t__users { get; set; }
        public virtual DbSet<vw_holiday_routings> vw_holiday_routings { get; set; }
        public virtual DbSet<t__companies> t__companies { get; set; }
        public virtual DbSet<t__departments> t__departments { get; set; }
        public virtual DbSet<vl_HRC_HBC_ASSIGNED_HOLIDAYS> vl_HRC_HBC_ASSIGNED_HOLIDAYS { get; set; }
        public virtual DbSet<vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING> vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING { get; set; }
    }
}
