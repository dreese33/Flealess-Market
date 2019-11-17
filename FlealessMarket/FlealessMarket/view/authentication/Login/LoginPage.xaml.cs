using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;
using Firebase.Database;
using System.Threading.Tasks;
using FlealessMarket.controller;

namespace FlealessMarket
{
    public partial class LoginPage : ContentPage
    {
        private RelativeLayout loginBox;

        public LoginPage()
        {
            InitializeComponent();

            this.background.Source = "BluePurple";
            this.main.LowerChild(this.background);

            //Setup other UI components
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            var textColor = Xamarin.Forms.Color.White;
            var textColorFree = Xamarin.Forms.Color.White;

            var subtractionValue = height * 0.05;

            this.logo.WidthRequest = height * 0.15;
            this.logo.HeightRequest = height * 0.15;
            this.logo.TranslationX = (width / 2) - (this.logo.WidthRequest / 2);
            this.logo.TranslationY = height * 0.1 - subtractionValue;
            this.logo.Source = "RecycleDollar";

            this.title.WidthRequest = width * 0.9;
            this.title.TranslationY = height * 0.25 - subtractionValue;
            this.title.TranslationX = width * 0.05;
            this.title.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.title.FontSize = height * 0.075;
            this.title.TextColor = textColorFree;
            this.title.FontFamily = "Open Sans";

            this.description.WidthRequest = width * 0.75;
            this.description.TranslationX = width * 0.125;
            this.description.TranslationY = height * 0.35 - subtractionValue;
            this.description.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.description.FontSize = height * 0.03;
            this.description.TextColor = textColorFree;
            this.description.FontFamily = "Hwt-Artz";
            this.description.FontAttributes = FontAttributes.Italic;

            this.back.Source = "BackButton";
            this.back.WidthRequest = height * 0.05;
            this.back.HeightRequest = height * 0.05;
            this.back.TranslationX = width * 0.03;
            this.back.TranslationY = height * 0.05;
            this.back.BackgroundColor = Xamarin.Forms.Color.Transparent;

            this.email.Placeholder = "Email";
            this.password.Placeholder = "Password";

            this.email.WidthRequest = width * 0.75;
            this.email.HeightRequest = height * 0.075;
            this.email.TranslationX = width * 0.125;
            this.email.TranslationY = height * 0.45 - subtractionValue;

            this.password.WidthRequest = width * 0.75;
            this.password.HeightRequest = height * 0.075;
            this.password.TranslationX = width * 0.125;
            this.password.TranslationY = height * 0.55 - subtractionValue;

            this.forgot.WidthRequest = width * 0.45;
            this.forgot.HeightRequest = height * 0.05;
            this.forgot.TranslationX = width * 0.47;
            this.forgot.TranslationY = height * 0.64 - subtractionValue;
            this.forgot.VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;
            this.forgot.BackgroundColor = Xamarin.Forms.Color.Transparent;
            this.forgot.FontSize = height * 0.02;

            this.signin.WidthRequest = width * 0.64;
            this.signin.HeightRequest = height * 0.075;
            this.signin.TranslationX = width * 0.18;
            this.signin.TranslationY = height * 0.67;
            this.signin.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            this.signin.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.signin.CornerRadius = (int)(this.signin.HeightRequest * 0.5);
            this.signin.FontSize = height * 0.033;
            this.signin.TextColor = textColor;

            this.signup.WidthRequest = width * 0.3;
            this.signup.HeightRequest = height * 0.05;
            this.signup.TranslationX = width * 0.1;
            this.signup.TranslationY = height * 0.76;// - subtractionValue;
            this.signup.VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;
            this.signup.BackgroundColor = Xamarin.Forms.Color.Transparent;
            this.signup.FontSize = height * 0.02;
        }

        private void Back_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Signup_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupUser();
        }

        private void Forgot_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ForgotPassword();
        }

        private void User_Signin(object sender, EventArgs e)
        {
            var usersFirebase = Task.Run(async () => await AppClient.firebaseClient.Child("user").OnceAsync<UnknownUser>());

            //Attempt login
            if (this.email.Text != null && this.password.Text != null)
            {
                foreach (FirebaseObject<UnknownUser> currUser in usersFirebase.Result)
                {
                    var user = currUser.Object;
                    if (user.email.ToLower().Equals(this.email.Text.ToLower()))
                    {
                        if (user.password.ToLower().Equals(this.password.Text.ToLower()))
                        {
                            Debug.WriteLine("User exists, logging in");
                            switch (user.type)
                            {
                                case (int)UserTypes.USER:
                                    //User
                                    FirebaseApi.LoginStatus = 1;
                                    break;
                                case (int)UserTypes.CONSIGNER:
                                    //Consignment
                                    FirebaseApi.LoginStatus = 2;
                                    break;
                                case (int)UserTypes.DRIVER:
                                    //Driver
                                    FirebaseApi.LoginStatus = 3;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            //Incorrect password
                            Debug.WriteLine("Incorrect Password");
                        }
                    }
                    else
                    {
                        //User does not exist
                        Debug.WriteLine("User does not exist");
                    }
                }
            }
        }
    }
}
