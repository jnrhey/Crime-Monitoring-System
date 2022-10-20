using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    [Activity(Label = "Admin & Police Dashboard")]
    public class admin_activity : AppCompatActivity
    {
        BottomNavigationView bottomNav;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.admin_police_layout);
                bottomNav = (BottomNavigationView)FindViewById(Resource.Id.bottom_nav_admin);
                bottomNav.ItemSelected += BottomNav_ItemSelected;
                LoadFragment(Resource.Id.action_admin_home);

            }
            catch (Exception ex) {
            Console.WriteLine(ex);
            }
           
            
        }

        private void BottomNav_ItemSelected(object sender, Google.Android.Material.Navigation.NavigationBarView.ItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        private void LoadFragment(int id)
        {
            var frag = SupportFragmentManager.BeginTransaction();
            switch (id)
            {
                case Resource.Id.action_admin_user:
                    admin_user_fragment user_Fragment = new admin_user_fragment();
                    frag.Replace(Resource.Id.landing_admin_layout, user_Fragment);
                    Console.WriteLine("USER");
                    break;

                case Resource.Id.action_admin_home:
                    admin_home_fragment home_Fragment = new admin_home_fragment();
                    frag.Replace(Resource.Id.landing_admin_layout, home_Fragment);
                    Console.WriteLine("HOME");
                    break;   
            }
            frag.Commit();
        }
    }
}