using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.GetSet
{
    public class comp_records_admin
    {
        public String compID { get; set; }
        public String compName { get; set; }
        public String compType { get; set; }
        public String compDesc { get; set; }
        public String time_stamp { get; set; }
        public String compStats { get; set; }
        public String submittedBy { get; set; }
        public String usrID { get; set; }
    }
}