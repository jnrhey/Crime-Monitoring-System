using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Activities
{
    [Activity(Label = "admin_trace_maps_activity")]
    public class admin_trace_maps_activity : Activity, IOnMapReadyCallback
    {
        ImageView back;
        private GoogleMap GMAp;
        public void OnMapReady(GoogleMap googleMap)
        {
            this.GMAp = googleMap;
            GMAp.UiSettings.ZoomControlsEnabled = true;
            string latitude = Intent.GetStringExtra("latitude");
            string longitude = Intent.GetStringExtra("longitude");
            Console.WriteLine("COORDINATES:\n" + "Latitude => " + latitude + "\n" + "Longitude => " + longitude);
            LatLng ltlng = new LatLng(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(ltlng, 15);
            GMAp.MoveCamera(camera);

            MarkerOptions options = new MarkerOptions()
                .SetPosition(ltlng)
                .SetTitle("SOS");
            GMAp.AddMarker(options);

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.admin_maps_layout);
            back = (ImageView)FindViewById(Resource.Id.adminMapBackbtn);
            back.Click += Back_Click;
            SetUpMap();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.OnBackPressed();
        }

        private void SetUpMap()
        {
            if(GMAp == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.trackGMaps).GetMapAsync(this); 
            }
        }
    }
}