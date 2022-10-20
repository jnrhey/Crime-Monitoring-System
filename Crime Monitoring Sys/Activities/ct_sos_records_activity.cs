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
using Java.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    [Activity(Label = "ct_sos_records_activity")]
    public class ct_sos_records_activity : Activity
    {
        ImageView back;
        RecyclerView recyclerView;
        List<sos_records_ct> record_list;
        sos_ct_record_adapter adapter;
        ct_sos_records_listeners Records_Listeners;
        FirebaseAuth usrAuth = AppDataHelper.GetFirebaseAuth();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.sos_ct_records_layout);
            back = (ImageView)FindViewById(Resource.Id.ctBackbtn);
            recyclerView = (RecyclerView)FindViewById(Resource.Id.sosCtRecycler);
            back.Click += Back_Click;
            RetrieveData();
            
        }
        void SetupRecyclerView()
        {
            recyclerView.SetLayoutManager(new LinearLayoutManager(recyclerView.Context));
            adapter = new sos_ct_record_adapter(record_list);
            adapter.deleteClick += Adapter_deleteClick;
            recyclerView.SetAdapter(adapter);
        }

        void RetrieveData()
        {
            Records_Listeners = new ct_sos_records_listeners();
            Records_Listeners.CreateData();
            Records_Listeners.sos_records_retrieved += Records_Listeners_sos_records_retrieved;
        }

        private void Records_Listeners_sos_records_retrieved(object sender, ct_sos_records_listeners.SosRecordDataEventArgs e)
        {
            record_list = e.Item;
            SetupRecyclerView();
        }

        private void Adapter_deleteClick(object sender, sos_ct_record_adapterClickEventArgs e)
        {
            DatabaseReference cartRef = AppDataHelper.GetDatabase().GetReference("sos_records/" + record_list[e.Position].sos_ID);
            cartRef.RemoveValue();
            Toast.MakeText(this, "SOS record Successfully Deleted!", ToastLength.Short).Show();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
        }

     
    }
}