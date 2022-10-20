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
    public class sos_records_ct
    {
        public String sos_ID { get; set; }
        public String sos_name { get; set;}
        public String sos_latitude { get; set;}
        public String sos_longitude { get; set;}
        public String sos_status { get; set;}
        public String time_stamp { get; set;}
    }
}