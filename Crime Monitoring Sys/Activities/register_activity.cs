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
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    [Activity(Label = "@string/app_name", MainLauncher = false)]
    public class register_activity : Activity
    {
        TextView btnLogin;
        Button btnRegister;
        Spinner spinnerAccRole;
        EditText usrEmail, usrFirstName, usrLastName, usrPassword, usrConPassword, usrPhoneNumber;
        List<string> AccountRoles;
        ArrayAdapter<string> adapter;
        string email, firstname, lastname, phone, password, passswordcon;
        TaskListener taskListener = new TaskListener();
        string strRole;
        FirebaseAuth usrAuth = AppDataHelper.GetFirebaseAuth();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register_layout);
            spinnerAccRole = FindViewById<Spinner>(Resource.Id.spinnerRole);
            btnLogin = FindViewById<TextView>(Resource.Id.btnLogin);
            usrEmail = FindViewById<EditText>(Resource.Id.regEmail);
            usrFirstName = FindViewById<EditText>(Resource.Id.regFirstName);
            usrLastName = FindViewById<EditText>(Resource.Id.regLastName);
            usrPhoneNumber = FindViewById<EditText>(Resource.Id.regNumber);
            usrPassword = FindViewById<EditText>(Resource.Id.regPassword);
            usrConPassword = FindViewById<EditText>(Resource.Id.regConPassword);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            AccountRoles = new List<string>();
            AccountRoles.Add("Tourist");
            AccountRoles.Add("Citizen");
            adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, AccountRoles);
            adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            spinnerAccRole.Adapter = adapter;
            spinnerAccRole.ItemSelected += SpinnerAccRole_ItemSelected;
            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;   
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            email = usrEmail.Text;
            firstname = usrFirstName.Text;
            lastname = usrLastName.Text;
            phone = usrPhoneNumber.Text;
            password = usrPassword.Text;
            passswordcon = usrConPassword.Text;

            //REGEX validator
            if (email.Length <= 3 || email == null || !email.Contains("@"))
            {
                Toast.MakeText(Application.Context, "Please enter a valid Email", ToastLength.Short).Show();
                return;
            }
            else if (firstname.Length <= 3 || firstname == null)
            {
                Toast.MakeText(Application.Context, "Please enter a valid First Name", ToastLength.Short).Show();
                return;
            }
            else if (lastname.Length <= 3 || lastname == null)
            {
                Toast.MakeText(Application.Context, "Please enter a valid Last Name", ToastLength.Short).Show();
                return;
            }
            else if (phone.Length < 11 || phone == null)
            {
                Toast.MakeText(Application.Context, "Please enter a valid Phone Number", ToastLength.Short).Show();
                return;
            }
            else if (password.Length <= 3 || password == null)
            {
                Toast.MakeText(Application.Context, "Password must be minimum 8 characters", ToastLength.Short).Show();
                return;
            }
            else if (password != passswordcon)
            {
                Toast.MakeText(Application.Context, "Password is not Match!", ToastLength.Short).Show();
                return;
            }
            else
            {
                Console.WriteLine("EMAIL : " + email);
                Console.WriteLine("First Name : " + firstname);
                Console.WriteLine("Last Name : " + lastname);
                Console.WriteLine("Phone : " + phone);
                Console.WriteLine("Password : " + password);
                Console.WriteLine("Account Role : " + strRole);
                RegisterAccount(email, password);
            }
           
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(login_activity));
            Finish();
        }

        private void SpinnerAccRole_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            string x;
            if (e.Position != -1)
            {
                x = AccountRoles[e.Position];
                strRole = x;
                Console.WriteLine(x);
            }
        }
        void RegisterAccount(string usrEmail, string usrPass)
        {
            taskListener.Success += TaskListener_Success;
            taskListener.Failure += TaskListener_Failure;
            usrAuth.CreateUserWithEmailAndPassword(usrEmail, usrPass)
                .AddOnSuccessListener(this, taskListener)
                .AddOnSuccessListener(this, taskListener);

        }

        private void TaskListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(Application.Context, "SignUp Failed! Please try again later", ToastLength.Short).Show();
        }

        private void TaskListener_Success(object sender, EventArgs e)
        {
            Toast.MakeText(Application.Context, "SignUp Successfully!", ToastLength.Short).Show();
            FirebaseDatabase database = AppDataHelper.GetDatabase();

            HashMap usrMap = new HashMap();
            usrMap.Put("first_name", firstname);
            usrMap.Put("last_name", lastname);
            usrMap.Put("email", email);
            usrMap.Put("phone", phone);
            usrMap.Put("account_role", strRole);

            DatabaseReference userRef = database.GetReference("users/" + usrAuth.CurrentUser.Uid);
            userRef.SetValue(usrMap);
            StartActivity(typeof(login_activity));
            Finish();
        }
    }
}