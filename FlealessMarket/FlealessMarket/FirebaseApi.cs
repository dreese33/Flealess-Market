using System;
using Firebase.Database;
using Firebase.Storage;
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

        public static FirebaseStorage firebaseStorage;

        private String imageUrl = "flealess-market.appspot.com";

        //Login globals
        public static int LoginStatus
        {
            get { return _loginStatus; }
            set
            {
                _loginStatus = value;
                if (_loginStatus == 1)
                {
                    //User
                    Application.Current.MainPage = new NavigationPage(new ConsignmentHome());
                }
                else if (_loginStatus == 2)
                {
                    //Consignment
                    Application.Current.MainPage = new NavigationPage(new Home());
                } else if (_loginStatus == 3)
                {
                    //Driver
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

            //Initialize firebase storage
            FirebaseApi.firebaseStorage = new FirebaseStorage(this.imageUrl);
        }
    }
}
