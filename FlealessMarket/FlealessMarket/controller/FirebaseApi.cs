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
        //Login globals
        private static int _loginStatus = 0;
        public static int LoginStatus
        {
            get { return _loginStatus; }
            set
            {
                _loginStatus = value;
                if (_loginStatus == 1)
                {
                    //User
                    Application.Current.MainPage = new NavigationPage(new UserHome());
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


    }
}
