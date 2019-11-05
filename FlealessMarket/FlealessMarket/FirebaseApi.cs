using System;
using Firebase.Database;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FlealessMarket
{
    //CRUD style API for firebase database
    public class FirebaseApi : ContentPage
    {
        //Firebase client
        public static FirebaseClient firebaseClient;
        private String auth = "VbwAboESljcPR5t2O5VXCpsDyyHvMPqoPAUh3Dte";
        public static String url = "https://flealess-market.firebaseio.com/";

        public static String imageUrl = "gs://flealess-market.appspot.com/images/";

        //Login globals
        public static int LoginStatus
        {
            get { return _loginStatus; }
            set
            {
                _loginStatus = value;
                if (_loginStatus == 1)
                {
                    Application.Current.MainPage = new NavigationPage(new ConsignmentHome());
                }
                else if (_loginStatus == 2)
                {
                    Application.Current.MainPage = new NavigationPage(new Home());
                } else if (_loginStatus == 3)
                {
                    Application.Current.MainPage = new DriverHome();
                }
                else
                {
                    Application.Current.MainPage = new MainPage();
                }
            }
        }

        private static int _loginStatus = 0;

        public FirebaseApi()
        {
            //Initialize firebase client
            FirebaseApi.firebaseClient = new FirebaseClient(FirebaseApi.url, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(this.auth)
            });
        }
    }
}
