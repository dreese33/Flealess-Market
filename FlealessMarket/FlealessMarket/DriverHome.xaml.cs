using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.Generic;

namespace FlealessMarket
{
    public partial class DriverHome : ContentPage
    {
        private Dictionary<String, String> addresses = new Dictionary<String, String>();
        private bool currentlyGo = false;

        public DriverHome()
        {
            InitializeComponent();

            //Set locations
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            this.header.WidthRequest = width;
            this.map.WidthRequest = width;

            this.header.HeightRequest = 0.20 * width;

            this.map.TranslationY = 0.20 * width;
            this.map.HeightRequest = height - (0.20 * width);

            //Add go button to screen
            this.goButton.BackgroundColor = Xamarin.Forms.Color.LightBlue;

            //Make circular
            this.goButton.WidthRequest = 76;
            this.goButton.HeightRequest = 76;
            this.goButton.CornerRadius = 38;

            this.goButton.Text = "GO";
            this.goButton.TextColor = Xamarin.Forms.Color.Black;

            this.goButton.TranslationX = width / 2 - 38;
            this.goButton.TranslationY = height * (7.0/8.0) - 38;

            this.goButton.Clicked += goButtonClicked;

            this.relative.RaiseChild(this.goButton);

            //Add finding trips layout
            this.internal_relative.WidthRequest = width;
            this.internal_relative.HeightRequest = height * (1.5 / 8.0);
            this.internal_relative.TranslationY = height * (6.5 / 8.0);

            //Add text to layout
            this.searching.Text = "Looking for rides:";
            this.searching.TextColor = Xamarin.Forms.Color.Black;
            this.searching.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.searching.WidthRequest = 150;
            this.searching.TranslationY = (height * 6.5 / 8.0) + 20;
            this.searching.TranslationX = (width / 2) - 75;

            //Add cancel button
            this.cancel.Text = "Cancel";
            this.cancel.WidthRequest = 76;
            this.cancel.HeightRequest = 25;
            this.cancel.TranslationX = 5;
            this.cancel.TranslationY = height - 5 - this.cancel.HeightRequest;
            this.cancel.Clicked += cancelButtonClicked;

            //Add activity indicator
            this.activity.HeightRequest = 76;
            this.activity.WidthRequest = 76;
            this.activity.IsRunning = false;
            this.activity.TranslationX = (width / 2) - 38;
            this.activity.TranslationY = (height * 6.5 / 8.0) + 20 + this.searching.HeightRequest + 20;

            this.relative.RaiseChild(this.internal_relative);
            this.relative.RaiseChild(this.searching);
            this.relative.RaiseChild(this.cancel);
            this.relative.RaiseChild(this.activity);

            //Default invisible values
            this.internal_relative.IsVisible = false;
            this.searching.IsVisible = false;
            this.cancel.IsVisible = false;
            this.activity.IsVisible = false;
        }

        //Begin waiting for rides
        private void goButtonClicked(object sender, EventArgs e)
        {
            this.goButton.IsVisible = false;

            this.internal_relative.IsVisible = true;
            this.searching.IsVisible = true;
            this.cancel.IsVisible = true;
            this.activity.IsVisible = true;

            this.activity.IsRunning = true;

            //Subscribe
            FirebaseApi.firebaseClient.Child("locations").AsObservable<LocalLocation>().Subscribe(updatedLocations => this.handleSubscriptions(updatedLocations));
            this.currentlyGo = true;
        }

        //Handle subscription to new locations added to database
        private void handleSubscriptions(Firebase.Database.Streaming.FirebaseEvent<LocalLocation> updatedLocations)
        {
            if (this.currentlyGo && !addresses.ContainsKey(updatedLocations.Key))
            {
                this.addresses.Add(updatedLocations.Key, updatedLocations.Object.potentialAddress);
                Debug.WriteLine("New address " + updatedLocations.Key);
            }
        }

        //End waiting for rides
        private void cancelButtonClicked(object sender, EventArgs e)
        {
            this.goButton.IsVisible = true;

            this.internal_relative.IsVisible = false;
            this.searching.IsVisible = false;
            this.cancel.IsVisible = false;
            this.activity.IsVisible = false;

            this.activity.IsRunning = false;
            this.currentlyGo = false;
        }
    }
}
