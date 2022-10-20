using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crime_Monitoring_Sys.Listeners
{
    public class TaskListener : Java.Lang.Object, IOnSuccessListener, IOnFailureListener
    {
        public event EventHandler Failure;
        public event EventHandler Success;

        public void OnFailure(Java.Lang.Exception e)
        {
            Failure?.Invoke(this, new EventArgs());
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            Success?.Invoke(this, new EventArgs());
        }
    }
}