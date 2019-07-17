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
    using System.Linq;
    using System.Web.Mvc;

    public partial class vw_holiday_routings
    {
        public int id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> person_in_charge_id { get; set; }
        public Nullable<int> mandatory { get; set; }
    
        public virtual t__users t__users { get; set; }
        public virtual t__users t__users1 { get; set; }

        public IEnumerable<SelectListItem> OptionList
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem> {
                        new SelectListItem() {
                            Text = "Sim", Value = "1"
                        },
                        new SelectListItem() {
                            Text = "N�o", Value = "0"
                        }
                    };
                return list.Select(l => new SelectListItem { Selected = (l.Value == mandatory.ToString()), Text = l.Text, Value = l.Value });
            }
        }
    }
}
