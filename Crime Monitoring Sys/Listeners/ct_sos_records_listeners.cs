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
    public class ct_sos_records_listeners : Java.Lang.Object, IValueEventListener
    {
        List<sos_records_ct> sos_records_list = new List<sos_records_ct>();
        public event EventHandler<SosRecordDataEventArgs> sos_records_retrieved;
        FirebaseAuth auth = AppDataHelper.GetFirebaseAuth();
        public class SosRecordDataEventArgs : EventArgs
        {
            public List<sos_records_ct> Item { get; set; }
        }
        public void OnCancelled(DatabaseError error)
        {
           
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            var child = snapshot.Children.ToEnumerable<DataSnapshot>();
            sos_records_list.Clear();
            try
            {
                foreach (DataSnapshot records in child)
                {
                    sos_records_ct record = new sos_records_ct();
                    record.sos_ID = records.Key;
                    if(records.Child("usrID").Value.ToString() == auth.Uid)
                    {
                        record.sos_name = records.Child("full_name").Value.ToString();
                        record.sos_latitude = records.Child("latitude").Value.ToString();
                        record.sos_longitude= records.Child("longitude").Value.ToString();
                        record.sos_status = records.Child("status").Value.ToString();
                        record.time_stamp = records.Child("time_stamp").Value.ToString();
                        sos_records_list.Add(record);
                    }
                   
                }
                sos_records_retrieved.Invoke(this, new SosRecordDataEventArgs { Item = sos_records_list });
            }catch(Exception ex)
            {
                Toast.MakeText(Application.Context, "Something went wrong! try again later.", ToastLength.Short).Show();
            }
           
        }

        public void CreateData()
        {
            DatabaseReference refre = AppDataHelper.GetDatabase().GetReference("sos_records");
            refre.AddValueEventListener(this);
        }
    }
}