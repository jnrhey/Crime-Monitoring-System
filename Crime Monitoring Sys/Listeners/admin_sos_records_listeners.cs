using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Crime_Monitoring_Sys.GetSet;
using Crime_Monitoring_Sys.Helpers;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Listeners
{
    public class admin_sos_records_listeners : Java.Lang.Object, IValueEventListener
    {
        List<sos_records_admin> sos_records = new List<sos_records_admin> ();
        public event EventHandler<SosRecordsDataEventArgs> sos_record_retrieved;
        public class SosRecordsDataEventArgs : EventArgs
        {
            public List<sos_records_admin> Item { get; set; }
        }
        public void OnCancelled(DatabaseError error)
        {
            
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            var child = snapshot.Children.ToEnumerable<DataSnapshot>();
            sos_records.Clear();
            try
            {
                foreach(DataSnapshot data in child)
                {
                    sos_records_admin records = new sos_records_admin();
                    records.sos_ID = data.Key;
                    if(data.Child("status").Value.ToString() != "Solved")
                    {
                        records.sos_name = data.Child("full_name").Value.ToString();
                        records.sos_latitude = data.Child("latitude").Value.ToString();
                        records.sos_longitude = data.Child("longitude").Value.ToString();
                        records.sos_status = data.Child("status").Value.ToString();
                        records.time_stamp = data.Child("time_stamp").Value.ToString();
                        sos_records.Add(records);
                    }
                }
                sos_record_retrieved.Invoke(this, new SosRecordsDataEventArgs { Item = sos_records });
            }catch(Exception ex)
            {
                Toast.MakeText(Application.Context, "Something Went wrong! try again later.", ToastLength.Short).Show();
            }
        }

        public void ReadData()
        {
            DatabaseReference dbRef = AppDataHelper.GetDatabase().GetReference("sos_records/");
            dbRef.AddValueEventListener(this);
        }
    }
}