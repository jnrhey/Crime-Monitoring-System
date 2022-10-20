using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Crime_Monitoring_Sys.Helpers;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    public class admin_user_fragment : AndroidX.Fragment.App.Fragment, IValueEventListener
    {
        View view;
        TextView usrFname, usrLname, usrPhone, usrEmail, usrRole;
        Button logout;
        string userFName, userLname, userPhone, userEmail, userRole;
        FirebaseAuth auth = AppDataHelper.GetFirebaseAuth();

        public void OnCancelled(DatabaseError error)
        {
            Console.Write(error);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.admin_user_layout, container, false);
            usrFname = (TextView)view.FindViewById(Resource.Id.userAdminFirstNameLbl);
            usrLname = (TextView)view.FindViewById(Resource.Id.userAdminLastNameLbl);
            usrEmail = (TextView)view.FindViewById(Resource.Id.userAdminEmailLbl);
            usrPhone = (TextView)view.FindViewById(Resource.Id.userAdminPhoneNumberLbl);
            usrRole = (TextView)view.FindViewById(Resource.Id.userAdminRoleLbl);
            logout = (Button)view.FindViewById(Resource.Id.btnAdminLogout);
            DatabaseReference userRef = AppDataHelper.GetDatabase().GetReference("users/" + auth.Uid);
            userRef.AddValueEventListener(this);
            logout.Click += Logout_Click;
            return view;
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            auth.SignOut();
            Activity.Finish();
            Intent i = new Intent(Application.Context, Java.Lang.Class.FromType(typeof(login_activity)));
            StartActivity(i);
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            userFName = snapshot.Child("first_name").Value.ToString();
            userLname = snapshot.Child("last_name").Value.ToString();
            userPhone = snapshot.Child("phone").Value.ToString();
            userEmail = snapshot.Child("email").Value.ToString();
            userRole = snapshot.Child("account_role").Value.ToString();
            if (userFName != null && userLname != null && userEmail != null && userPhone != null && userRole != null)
            {
                usrFname.Text = userFName;
                usrLname.Text = userLname;
                usrPhone.Text = userPhone;
                usrEmail.Text = userEmail;
                usrRole.Text = userRole;
            }
        }
    }
}