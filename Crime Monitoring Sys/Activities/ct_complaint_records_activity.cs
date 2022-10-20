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
    [Activity(Label = "ct_complaint_records_activity")]
    public class ct_complaint_records_activity : Activity
    {
        ImageView back;
        RecyclerView recyclerView;
        List<comp_records_ct> record_list;
        comp_ct_record_adapter adapter;
        ct_comp_records_listeners record_listener;
        FirebaseAuth auth = AppDataHelper.GetFirebaseAuth();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.complaint_ct_records_layout);
            back = (ImageView)FindViewById(Resource.Id.ctCompBackbtn);
            recyclerView = (RecyclerView)FindViewById(Resource.Id.complaintCtRecycler);
            back.Click += Back_Click;
            RetriveData();
        }
        void SetupRecycler()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
            adapter = new comp_ct_record_adapter(record_list);
            adapter.deleteClick += Adapter_deleteClick;
            recyclerView.SetAdapter(adapter);
        }
        void RetriveData()
        {
            record_listener = new ct_comp_records_listeners();
            record_listener.ReadData();
            record_listener.comp_record_retrived += Record_listener_comp_record_retrived;
        }

        private void Record_listener_comp_record_retrived(object sender, ct_comp_records_listeners.CompRecordDataEventArgs e)
        {
            record_list = e.Item;
            SetupRecycler();
        }

        private void Adapter_deleteClick(object sender, comp_ct_record_adapterClickEventArgs e)
        {
            TaskListener taskListener = new TaskListener();
            taskListener.Success += TaskListener_Success;
            taskListener.Failure += TaskListener_Failure;

            DatabaseReference dbRef = AppDataHelper.GetDatabase().GetReference("comp_records/" + record_list[e.Position].comp_ID);
            dbRef.RemoveValue().AddOnSuccessListener(taskListener)
              .AddOnFailureListener(taskListener);

            
        }

        private void TaskListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Unable to delete record", ToastLength.Short).Show();
        }

        private void TaskListener_Success(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Record Successfully Deleted!", ToastLength.Short).Show();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
        }
    }
}