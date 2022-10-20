using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Crime_Monitoring_Sys.Adapters;
using Crime_Monitoring_Sys.GetSet;
using Crime_Monitoring_Sys.Helpers;
using Crime_Monitoring_Sys.Listeners;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    [Activity(Label = "admin_complaint_records_activity")]
    public class admin_complaint_records_activity : Activity
    {
        ImageView back;
        RecyclerView recyclerView;
        FirebaseAuth auth = AppDataHelper.GetFirebaseAuth();
        List<comp_records_admin> comps;
        com_admin_record_adapter adapter;
        admin_comp_records_listeners listeners;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.complaint_admin_record_layout);
            back = (ImageView)FindViewById(Resource.Id.AdminCompBackbtn);
            recyclerView = (RecyclerView)FindViewById(Resource.Id.complaintAdminRecycler);
            back.Click += Back_Click;
            RetrieveData();
        }
        void SetupRecycler()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
            adapter = new com_admin_record_adapter(comps);
            adapter.updateClick += Adapter_updateClick;
            recyclerView.SetAdapter(adapter);
        }
        void RetrieveData()
        {
            listeners = new admin_comp_records_listeners();
            listeners.ReadData();
            listeners.comps_record_retrieved += Listeners_comps_record_retrieved;
        }

        private void Listeners_comps_record_retrieved(object sender, admin_comp_records_listeners.CompsRecordsDataEventArgs e)
        {
            comps = e.Item;
            SetupRecycler();
        }

        private void Adapter_updateClick(object sender, com_admin_record_adapterClickEventArgs e)
        {
            
            TaskListener taskListener = new TaskListener();
            taskListener.Success += TaskListener_Success;
            taskListener.Failure += TaskListener_Failure;

            DatabaseReference dbRef = AppDataHelper.GetDatabase().GetReference("comp_records/" + comps[e.Position].compID + "/comp_status");
            dbRef.SetValue("Solved").AddOnSuccessListener(taskListener)
              .AddOnFailureListener(taskListener);
        }

        private void TaskListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Unable to update record. Try again later", ToastLength.Short).Show();
        }

        private void TaskListener_Success(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Record Updated Successfully!", ToastLength.Short).Show();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
            Finish();
        }
    }
}