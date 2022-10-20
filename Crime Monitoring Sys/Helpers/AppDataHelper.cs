using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Auth;

namespace Crime_Monitoring_Sys.Helpers
{
    public class AppDataHelper
    {

        public static FirebaseAuth GetFirebaseAuth()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseAuth usrAuth;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                   .SetApplicationId("crime-monitoring-system")
                    .SetApiKey("AIzaSyAGKhcXmgmihYLjFD9IMx7Xh5RVb348eWI")
                    .SetDatabaseUrl("https://crime-monitoring-system-default-rtdb.asia-southeast1.firebasedatabase.app")
                    .SetStorageBucket("crime-monitoring-system.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                usrAuth = FirebaseAuth.Instance;
            }
            else
            {
                usrAuth = FirebaseAuth.Instance;
            }
            return usrAuth;
        }

        public static FirebaseUser GetCurrentUser()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseAuth usrAuth;
            FirebaseUser user;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetApplicationId("crime-monitoring-system")
                     .SetApiKey("AIzaSyAGKhcXmgmihYLjFD9IMx7Xh5RVb348eWI")
                     .SetDatabaseUrl("https://crime-monitoring-system-default-rtdb.asia-southeast1.firebasedatabase.app")
                     .SetStorageBucket("crime-monitoring-system.appspot.com")
                     .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                usrAuth = FirebaseAuth.Instance;
                user = usrAuth.CurrentUser;
            }
            else
            {
                usrAuth = FirebaseAuth.Instance;
                user = usrAuth.CurrentUser;
            }
            return user;

        }

        public static FirebaseDatabase GetDatabase()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetApplicationId("crime-monitoring-system")
                    .SetApiKey("AIzaSyAGKhcXmgmihYLjFD9IMx7Xh5RVb348eWI")
                    .SetDatabaseUrl("https://crime-monitoring-system-default-rtdb.asia-southeast1.firebasedatabase.app")
                    .SetStorageBucket("crime-monitoring-system.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                database = FirebaseDatabase.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }
            return database;
        }

    }
}