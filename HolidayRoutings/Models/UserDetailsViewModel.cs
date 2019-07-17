using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace HolidayRoutings.Models
{
    public class UserDetailsViewModel
    {
        public t__users User { get; set; }
        //public List<vw_holiday_routings> routingsList { get; set; }
        public List<vl_HRC_HBC_HOLIDAY_DEPARTMENT_ROUTING> routingsList { get; set; }
        public List<vl_HRC_HBC_ASSIGNED_HOLIDAYS> holidaysList { get; set; }
    }
}