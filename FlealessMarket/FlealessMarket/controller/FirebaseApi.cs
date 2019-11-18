using System;
using Firebase.Database;
using Firebase.Storage;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Xamarin.Forms;
using FlealessMarket.controller;
using System.IO;
using System.Net;
using System.Text;

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
                    FirebaseApi.Set_Consign_Home();
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

        public static void Set_Consign_Home()
        {
            NavigationPage newPage = new NavigationPage(new Home());
            newPage.BarBackgroundColor = Xamarin.Forms.Color.FromHex("40b5bc");
            newPage.BarTextColor = Xamarin.Forms.Color.White;

            Application.Current.MainPage = newPage;
        }

        public static void Add_Firebase(UnknownUser user)
        {
            try
            {
                //Insert into firebase
                //Add some kind of delay wheel, this sometimes takes time to process
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                Debug.WriteLine("Json currently looks like: " + json.ToString());
                var request = WebRequest.CreateHttp(AppClient.url + "user/.json");
                request.Method = "POST";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                var response = request.GetResponse();
                json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                Debug.WriteLine(json);
            }
            catch (FirebaseException fireExc)
            {
                Debug.WriteLine(fireExc.Message);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        public static async Task UploadImage(Stream fileStream, string fileName)
        {
            await AppClient.firebaseStorage
                .Child("images")
                .Child(fileName)
                .PutAsync(fileStream);
        }
    }
}
