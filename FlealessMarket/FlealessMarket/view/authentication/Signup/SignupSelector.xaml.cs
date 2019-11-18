using System;
using Firebase.Database;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Net;
using FlealessMarket.controller;

namespace FlealessMarket
{
    public partial class SignupSelector : ContentPage
    {
        private UnknownUser user;

        //Type explained: 0 - user, 1 - driver, 2 - consignment store

        public SignupSelector(UnknownUser user)
        {
            InitializeComponent();

            this.user = user;

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

            this.user_desc.WidthRequest = width * 0.75;
            this.user_desc.TranslationX = width * 0.125;
            this.user_desc.TranslationY = height * 0.45 - subtractionValue;
            this.user_desc.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.user_desc.FontSize = height * 0.03;
            this.user_desc.TextColor = textColorFree;

            this.userbutton.WidthRequest = width * 0.64;
            this.userbutton.HeightRequest = height * 0.075;
            this.userbutton.TranslationX = width * 0.18;
            this.userbutton.TranslationY = height * 0.5 - subtractionValue;
            this.userbutton.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            this.userbutton.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.userbutton.CornerRadius = (int)(this.userbutton.HeightRequest * 0.5);
            this.userbutton.FontSize = height * 0.033;
            this.userbutton.TextColor = textColor;

            this.driver_desc.WidthRequest = width * 0.75;
            this.driver_desc.TranslationX = width * 0.125;
            this.driver_desc.TranslationY = height * 0.62 - subtractionValue;
            this.driver_desc.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.driver_desc.FontSize = height * 0.03;
            this.driver_desc.TextColor = textColorFree;

            this.driver.WidthRequest = width * 0.64;
            this.driver.HeightRequest = height * 0.075;
            this.driver.TranslationY = height * 0.67 - subtractionValue;
            this.driver.TranslationX = width * 0.18;
            this.driver.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.driver.CornerRadius = (int)(this.driver.HeightRequest * 0.5);
            this.driver.FontSize = height * 0.033;
            this.driver.TextColor = textColor;

            this.consignment_desc.WidthRequest = width * 0.75;
            this.consignment_desc.TranslationX = width * 0.125;
            this.consignment_desc.TranslationY = height * 0.79 - subtractionValue;
            this.consignment_desc.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.consignment_desc.FontSize = height * 0.03;
            this.consignment_desc.TextColor = textColorFree;

            this.consignment.WidthRequest = width * 0.64;
            this.consignment.HeightRequest = height * 0.075;
            this.consignment.TranslationY = height * 0.84 - subtractionValue;
            this.consignment.TranslationX = width * 0.18;
            this.consignment.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.consignment.CornerRadius = (int)(this.driver.HeightRequest * 0.5);
            this.consignment.FontSize = height * 0.033;
            this.consignment.TextColor = textColor;
        }

        private void Back_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupUser(this.user);
        }

        //TODO -- Add to db
        private void User_Signup(object sender, EventArgs e)
        {
            this.user.type = 0;
            Application.Current.MainPage = new PhoneNumberEntryPage(this.user);
        }

        //TODO -- Add to db
        private void Driver_Signup(object sender, EventArgs e)
        {
            this.user.type = 1;
            Application.Current.MainPage = new DriverUpload(user);
        }

        //TODO -- Add to db
        private void Consignment_Signup(object sender, EventArgs e)
        {
            this.user.type = 2;
            Application.Current.MainPage = new ConsignInfo(user);
        }
    }
}
