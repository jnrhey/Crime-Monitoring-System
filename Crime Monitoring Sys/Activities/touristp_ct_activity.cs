using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    [Activity(Label = "@string/app_name")]
    public class touristp_ct_activity : AppCompatActivity
    {
        BottomNavigationView bottomNav;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.touristp_citizen_layout);
                bottomNav = (BottomNavigationView)FindViewById(Resource.Id.bottom_nav_ct);
                bottomNav.ItemSelected += BottomNav_ItemSelected;
                LoadFragment(Resource.Id.action_home);
                

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void LoadFragment(int id)
        {
            var frag = SupportFragmentManager.BeginTransaction();
            switch (id)
            {
                case Resource.Id.action_file:
                    ct_file_fragment file_Fragment = new ct_file_fragment();
                    frag.Replace(Resource.Id.landing_ct_layout, file_Fragment);
                    Console.WriteLine("FILE");
                    break;

                case Resource.Id.action_home:
                    ct_home_fragment home_Fragment = new ct_home_fragment();
                    frag.Replace(Resource.Id.landing_ct_layout, home_Fragment);
                    Console.WriteLine("HOME");
                    break;

                case Resource.Id.action_user:
                    ct_user_fragment user_Fragment = new ct_user_fragment();
                    frag.Replace(Resource.Id.landing_ct_layout, user_Fragment);
                    Console.WriteLine("USER");
                    break;
            }
            frag.Commit();
        }

        private void BottomNav_ItemSelected(object sender, Google.Android.Material.Navigation.NavigationBarView.ItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
        

    }
}