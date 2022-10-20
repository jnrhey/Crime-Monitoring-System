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
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    [Activity(Label = "admin_sos_records_activity")]
    public class admin_sos_records_activity : Activity
    {
        ImageView back;
        RecyclerView recyclerView;
        List<sos_records_admin> records;
        sos_admin_records_adapter adapter;
        admin_sos_records_listeners listeners;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.sos_admin_records_layout);
            back = (ImageView)FindViewById(Resource.Id.adminSosBackbtn);
            recyclerView = (RecyclerView)FindViewById(Resource.Id.sosAdminRecycler);
            back.Click += Back_Click;
            RetrieveData();
        }

        void SetupRecycler()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
            adapter = new sos_admin_records_adapter(records);
            adapter.updateClick += Adapter_updateClick;
            adapter.traceClick += Adapter_traceClick;
            recyclerView.SetAdapter(adapter);
        }
        void RetrieveData()
        {
            listeners = new admin_sos_records_listeners();
            listeners.ReadData();
            listeners.sos_record_retrieved += Listeners_sos_record_retrieved;
        }

        private void Listeners_sos_record_retrieved(object sender, admin_sos_records_listeners.SosRecordsDataEventArgs e)
        {
            records = e.Item;
            SetupRecycler();
        }

        private void Adapter_traceClick(object sender, sos_admin_records_adapterClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(admin_trace_maps_activity));
            intent.PutExtra("latitude", records[e.Position].sos_latitude.ToString());
            intent.PutExtra("longitude", records[e.Position].sos_longitude.ToString());
            StartActivity(intent);
        }

        private void Adapter_updateClick(object sender, sos_admin_records_adapterClickEventArgs e)
        {
            TaskListener taskListener = new TaskListener();
            taskListener.Success += TaskListener_Success;
            taskListener.Failure += TaskListener_Failure;

            DatabaseReference cartRef = AppDataHelper.GetDatabase().GetReference("sos_records/" + records[e.Position].sos_ID + "/status");
            cartRef.SetValue("Solved").AddOnSuccessListener(taskListener)
                .AddOnFailureListener(taskListener);
            
        }

        private void TaskListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Something went wrong.. Try again later!", ToastLength.Short).Show();
        }

        private void TaskListener_Success(object sender, EventArgs e)
        {
            Toast.MakeText(this, "SOS record Successfully updated!", ToastLength.Short).Show();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
        }
    }
}