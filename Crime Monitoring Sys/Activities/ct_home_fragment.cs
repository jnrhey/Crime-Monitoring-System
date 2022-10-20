using Android;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using Crime_Monitoring_Sys.Helpers;
using Firebase.Auth;
using Firebase.Database;
using Java.Util;
using Org.Apache.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using static Android.Manifest;

namespace Crime_Monitoring_Sys.Activities
{
    public class ct_home_fragment : AndroidX.Fragment.App.Fragment, IValueEventListener
    {
        View view;
        Button btnSosReports, btnComplaintReports, btnSendSOS;
        const int RequestID = 0;
        readonly string[] permissionsGroup =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
        };
        string latitude, longitude, usrFname, usrLname, usrFullName;
        FirebaseDatabase database = AppDataHelper.GetDatabase();
        FirebaseAuth usrAuth = AppDataHelper.GetFirebaseAuth();
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.ct_home_layout, container, false);
            btnComplaintReports = (Button)view.FindViewById(Resource.Id.btnCheckComplaints);
            btnSosReports = (Button)view.FindViewById(Resource.Id.btnChekSosReport);
            btnSendSOS = (Button)view.FindViewById(Resource.Id.btnsendSOS);
           
            //CHECK PERMISSION FOR MAPS
            CheckSpecialPermission();            
            btnSendSOS.Click += BtnSendSOS_Click;
            btnSosReports.Click += BtnSosReports_Click;
            btnComplaintReports.Click += BtnComplaintReports_Click;
            return view;
        }

        private async void BtnSendSOS_Click(object sender, EventArgs e)
        {
            
            try
            {
                Toast.MakeText(Context, "Retrieving your Location..", ToastLength.Short).Show();
                var location = await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    Toast.MakeText(Context, "SOS sent Successfully", ToastLength.Short).Show();
                    latitude = location.Latitude.ToString(); 
                    longitude = location.Longitude.ToString();
                    DatabaseReference userRef = AppDataHelper.GetDatabase().GetReference("users/" + usrAuth.Uid);
                    userRef.AddValueEventListener(this);

                    Console.WriteLine("latitude => " + latitude + "\n" + "longitude => " + longitude);
                }
                    
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Toast.MakeText(Context, "Unable to get your Location", ToastLength.Short).Show();
            }
           
              
        }
        public void OnCancelled(DatabaseError error) 
        {
            
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            usrFname = snapshot.Child("first_name").Value.ToString();
            usrLname = snapshot.Child("last_name").Value.ToString();
            usrFullName = usrFname + " " + usrLname;

            if(usrFullName != null)
            {
                string time = DateTime.Now.ToString("G");
                HashMap sosRecord = new HashMap();
                sosRecord.Put("full_name", usrFullName);
                sosRecord.Put("latitude" , latitude);
                sosRecord.Put("longitude" , longitude);
                sosRecord.Put("status", "Active");
                sosRecord.Put("time_stamp", time);
                sosRecord.Put("usrID", usrAuth.Uid);
                DatabaseReference sosRef = database.GetReference("sos_records/" + usrFullName);
                sosRef.SetValue(sosRecord);             
            }
        }

        private void BtnSosReports_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Application.Context, Java.Lang.Class.FromType(typeof(ct_sos_records_activity)));
            StartActivity(i);
        }

        private void BtnComplaintReports_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Application.Context, Java.Lang.Class.FromType(typeof(ct_complaint_records_activity)));
            StartActivity(i);
        }
        bool CheckSpecialPermission()
        {
            bool permissionGranted = false;
            if (ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted &&
                ActivityCompat.CheckSelfPermission(Application.Context, Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
            {
                RequestPermissions(permissionsGroup, RequestID);
            }
            else
            {
                permissionGranted = true;
            }

            return permissionGranted;
        }
    }
}