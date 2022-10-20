using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Crime_Monitoring_Sys.Helpers;
using Crime_Monitoring_Sys.Listeners;
using Firebase.Auth;
using Firebase.Database;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    public class ct_file_fragment : AndroidX.Fragment.App.Fragment, IValueEventListener
    {
        View view;
        Button sendComplaint;
        EditText complaintName, complaintDesc;
        List<string> TypeOfComplaint;
        ArrayAdapter<string> adapter;
        Spinner typeComplaint;
        string typeComp;
        FirebaseAuth usrAuth = AppDataHelper.GetFirebaseAuth();
        TaskListener taskListener = new TaskListener();
        string senderName;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.ct_file_layout, container, false);
            sendComplaint = (Button)view.FindViewById(Resource.Id.btnAddComplaint);
            complaintName = (EditText)view.FindViewById(Resource.Id.complaintName);
            complaintDesc  = (EditText)view.FindViewById(Resource.Id.complaintDesc);
            typeComplaint = (Spinner)view.FindViewById(Resource.Id.spinnerType);
            TypeOfComplaint = new List<string>();
            TypeOfComplaint.Add("Murder");
            TypeOfComplaint.Add("Attemp to Murder");
            TypeOfComplaint.Add("Rape");
            TypeOfComplaint.Add("Custodial Rape");
            TypeOfComplaint.Add("Kidnapping & Abduction");
            TypeOfComplaint.Add("Phishing");
            TypeOfComplaint.Add("Robbery");
            TypeOfComplaint.Add("Burglary");
            TypeOfComplaint.Add("Theft");
            TypeOfComplaint.Add("Car Theft");
            TypeOfComplaint.Add("Missing");
            TypeOfComplaint.Add("Hold-up");
            TypeOfComplaint.Add("Wreckless Driver");
            TypeOfComplaint.Add("Torture");
            adapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, TypeOfComplaint);
            adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            typeComplaint.Adapter = adapter;
            typeComplaint.ItemSelected += TypeComplaint_ItemSelected;
            sendComplaint.Click += SendComplaint_Click;
            DatabaseReference Ref = AppDataHelper.GetDatabase().GetReference("users/" + usrAuth.Uid);
            Ref.AddValueEventListener(this);
            return view;
        }

        private void SendComplaint_Click(object sender, EventArgs e)
        {
            string name = complaintName.Text;
            string desc = complaintDesc.Text;
            if (complaintName == null || name.Length <= 3)
            {
                Toast.MakeText(Context, "Please enter a valid Name!", ToastLength.Short).Show();
                return;
            }
            else if (complaintDesc == null || desc.Length <= 3)
            {
                Toast.MakeText(Context, "Please fill in the description", ToastLength.Short).Show();
            }
            else
            {
                FirebaseDatabase db = AppDataHelper.GetDatabase();
                taskListener.Failure += TaskListener_Failure;
                taskListener.Success += TaskListener_Success;
                        if(senderName != null)
                {
                    string time = DateTime.Now.ToString("G");
                    HashMap compMap = new HashMap();
                    compMap.Put("comp_name", name);
                    compMap.Put("comp_desc", desc);
                    compMap.Put("type_of_comp", typeComp);
                    compMap.Put("time_stamp", time);
                    compMap.Put("comp_status", "Active");
                    compMap.Put("submitted_by", senderName);
                    compMap.Put("usrID", usrAuth.Uid);
                    DatabaseReference dbRef = db.GetReference("comp_records/" + name);
                    dbRef.SetValue(compMap)
                        .AddOnSuccessListener(taskListener)
                        .AddOnFailureListener(taskListener);
                }
                        
       
            }
        }

        private void TaskListener_Success(object sender, EventArgs e)
        {
            Toast.MakeText(Context, "Complaint send successfully!", ToastLength.Short).Show();
            complaintName.Text = "";
            complaintDesc.Text = "";
        }

        private void TaskListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(Context, "Something went wrong.. try again later.", ToastLength.Short).Show();
        }

        private void TypeComplaint_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            string x;
            if(e.Position != 1)
            {
                x = TypeOfComplaint[e.Position];
                typeComp = x;
            }
        }

        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            string x, y;
            x = snapshot.Child("first_name").Value.ToString();
            y = snapshot.Child("last_name").Value.ToString();
            senderName = x + " " + y;
        }
    }
}