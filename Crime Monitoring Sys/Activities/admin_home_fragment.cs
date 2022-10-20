using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    public class admin_home_fragment : AndroidX.Fragment.App.Fragment
    {
        View view;
        Button btnCheckComplaintRecord, btnCheckSosRecords;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.admin_home_layout, container, false);
            btnCheckComplaintRecord = (Button)view.FindViewById(Resource.Id.btnAdminCheckComplaint);
            btnCheckSosRecords = (Button)view.FindViewById(Resource.Id.btnAdminChekSosReport);

            btnCheckComplaintRecord.Click += BtnCheckComplaintRecord_Click;
            btnCheckSosRecords.Click += BtnCheckSosRecords_Click;

            return view;
        }

        private void BtnCheckSosRecords_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Application.Context, Java.Lang.Class.FromType(typeof(admin_sos_records_activity)));
            StartActivity(i);
        }

        private void BtnCheckComplaintRecord_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Application.Context, Java.Lang.Class.FromType(typeof(admin_complaint_records_activity)));
            StartActivity(i);
        }
    }
}