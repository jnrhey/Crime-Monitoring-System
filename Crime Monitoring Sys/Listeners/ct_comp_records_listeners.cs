using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Crime_Monitoring_Sys.GetSet;
using Crime_Monitoring_Sys.Helpers;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Listeners
{
    public class ct_comp_records_listeners : Java.Lang.Object, IValueEventListener
    {
        List<comp_records_ct> comp_Records_s = new List<comp_records_ct>();
        public event EventHandler<CompRecordDataEventArgs> comp_record_retrived;
        FirebaseAuth auth = AppDataHelper.GetFirebaseAuth();
        public class CompRecordDataEventArgs : EventArgs
        {
            public List<comp_records_ct> Item { get; set; }
        }
        public void OnCancelled(DatabaseError error)
        {
            
        }

        public void OnDataChange(DataSnapshot snapshot)
        {          
            var child = snapshot.Children.ToEnumerable<DataSnapshot>();
            comp_Records_s.Clear();
            try 
            {
                    foreach (DataSnapshot data in child)
                    {
                         comp_records_ct records = new comp_records_ct();
                         records.comp_ID = data.Key;
                            if (data.Child("usrID").Value.ToString() == auth.Uid)
                            {
                                 records.comp_name = data.Child("comp_name").Value.ToString();
                                 records.comp_desc = data.Child("comp_desc").Value.ToString();
                                 records.time_stamp = data.Child("time_stamp").Value.ToString();
                                 records.type_of_comp = data.Child("type_of_comp").Value.ToString();
                                 records.comp_stat = data.Child("comp_status").Value.ToString();
                                 comp_Records_s.Add(records);
                            }
                    }
                    comp_record_retrived.Invoke(this, new CompRecordDataEventArgs { Item = comp_Records_s });
            }catch(Exception ex)
            {
                Toast.MakeText(Application.Context, "Something Went wrong! try again later.", ToastLength.Short).Show();
            } 
        }

        public void ReadData()
        {
            DatabaseReference dbRef = AppDataHelper.GetDatabase().GetReference("comp_records/");
            dbRef.AddValueEventListener(this);
        }
    }
}