//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class t__companies
    {
        public int id { get; set; }
        [DisplayName("Empresa")]
        public string CompDescription { get; set; }
        public string CompDB { get; set; }
        public string CompPHC { get; set; }
        public string CompPC { get; set; }
        public string CompNAV { get; set; }
        public string queueNAV { get; set; }
        public Nullable<int> pcs_enabled { get; set; }
        public Nullable<int> das_enabled { get; set; }
        public Nullable<int> hbc_enabled { get; set; }
        public Nullable<int> hbc_form_enabled { get; set; }
        public Nullable<int> mas_enabled { get; set; }
        public Nullable<int> status { get; set; }
        public string NIF { get; set; }
        public string Cod { get; set; }
        public string NAVComp { get; set; }
    }
}
