using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class login_activity : Activity , IValueEventListener
    {
        ISharedPreferences session = Application.Context.GetSharedPreferences("_session", FileCreationMode.Private);
        ISharedPreferencesEditor editor;
        Button btnOk;
        TextView btnRegister;
        EditText userEmail, userPassword;
        string email, password, accRole;
        FirebaseAuth auth = AppDataHelper.GetFirebaseAuth();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_layout);
            btnOk = (Button)FindViewById(Resource.Id.buttonlogin);
            btnRegister = (TextView)FindViewById(Resource.Id.RegisterAcc);
            userEmail = (EditText)FindViewById(Resource.Id.logEmail);
            userPassword = (EditText)FindViewById(Resource.Id.logPassword);
            btnOk.Click += BtnOk_Click;
            btnRegister.Click += BtnRegister_Click;

            if(session.GetString("session","") == "true" && session.GetString("accRole","") == "Citizen")
            {
                StartActivity(typeof(touristp_ct_activity));
                Finish();
            }else if (session.GetString("session", "") == "true" && session.GetString("accRole", "") == "Tourist")
            {
                StartActivity(typeof(touristp_ct_activity));
                Finish();
            }else if (session.GetString("session", "") == "true" && session.GetString("accRole", "") == "Admin")
            {
                StartActivity(typeof(admin_activity));
                Finish();
            }
            else if (session.GetString("session", "") == "true" && session.GetString("accRole", "") == "Police")
            {
                StartActivity(typeof(admin_activity));
                Finish();
            }
            else
            {
                //session closed
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {

            StartActivity(typeof(register_activity));
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {

            email = userEmail.Text;
            password = userPassword.Text;
            if (email == null || !email.Contains("@"))
            {
                Toast.MakeText(this, "Enter a valid Email", ToastLength.Short).Show();
                return;
            }
            else if (password == null || password.Length < 8)
            {
                Toast.MakeText(this, "Enter a valid Password!", ToastLength.Short).Show();
                return;
            }
            else if (password == null || email == null)
            {
                Toast.MakeText(this, "Please fill in your Cridentials to login", ToastLength.Short).Show();
                return;
            }
            else
            {
                TaskListener taskListener = new TaskListener();
                taskListener.Success += TaskListener_Success;
                taskListener.Failure += TaskListener_Failure;

                auth.SignInWithEmailAndPassword(email, password)
                    .AddOnSuccessListener(taskListener)
                    .AddOnFailureListener(taskListener);
            }
        }
        private void TaskListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Email or Password is incorrect!", ToastLength.Short).Show();
            return;
        }

        private void TaskListener_Success(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Successfully Logged in!", ToastLength.Short).Show();
            DatabaseReference userRef = AppDataHelper.GetDatabase().GetReference("users/" + auth.Uid);
            userRef.AddValueEventListener(this);
            
        }

        public void OnCancelled(DatabaseError error)
        {
            Console.WriteLine(error.Message);
        }

        public void OnDataChange(DataSnapshot snapshot)
        {

            accRole = snapshot.Child("account_role").Value.ToString();           
            if(accRole == "Citizen" || accRole == "Tourist")
            {
                editor = session.Edit();
                editor.PutString("usrID", auth.Uid);
                editor.PutString("session", "true");
                editor.PutString("accRole" , accRole);
                editor.Apply();
                StartActivity(typeof(touristp_ct_activity));
                Finish();
            }else if (accRole == "Admin" || accRole == "Police")
            {
                editor = session.Edit();
                editor.PutString("usrID", auth.Uid);
                editor.PutString("session", "true");
                editor.PutString("accRole", accRole);
                editor.Apply();
                StartActivity(typeof(admin_activity));
                Finish();
            }          
        }
    }
}