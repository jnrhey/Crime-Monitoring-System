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
    public class admin_comp_records_listeners : Java.Lang.Object, IValueEventListener
    {
        List<comp_records_admin> comp_Records_Admins = new List<comp_records_admin>();
        public event EventHandler<CompsRecordsDataEventArgs> comps_record_retrieved;

        public class CompsRecordsDataEventArgs : EventArgs
        {
            public List<comp_records_admin> Item { get; set; }
        }
        public void OnCancelled(DatabaseError error)
        {
            
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            var child = snapshot.Children.ToEnumerable<DataSnapshot>();
            comp_Records_Admins.Clear();
            try
            {
                foreach(DataSnapshot data in child)
                {
                        comp_records_admin records = new comp_records_admin();
                        records.compID = data.Key;
                    if(data.Child("comp_status").Value.ToString() != "Solved")
                    {
                        records.compName = data.Child("comp_name").Value.ToString();
                        records.compDesc = data.Child("comp_desc").Value.ToString();
                        records.compType = data.Child("type_of_comp").Value.ToString();
                        records.time_stamp = data.Child("time_stamp").Value.ToString();
                        records.compStats = data.Child("comp_status").Value.ToString();
                        records.usrID = data.Child("usrID").Value.ToString();
                        records.submittedBy = data.Child("submitted_by").Value.ToString(); comp_Records_Admins.Add(records);
                    }

                }
                comps_record_retrieved.Invoke(this, new CompsRecordsDataEventArgs { Item = comp_Records_Admins });
            }
            catch(Exception ex)
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