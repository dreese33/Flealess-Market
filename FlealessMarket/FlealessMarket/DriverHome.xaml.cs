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
        private const int SECONDS_ONE_DAY = 86400;

        //Ensures consistant driver state
        private int DriverState
        {
            get { return _driverState; }
            set
            {
                _driverState = value;
                if (_driverState == 0)
                {
                    //Waiting to go
                    this.goButton.IsVisible = true;
                    this.setSearchingForRidesInvisible();
                    this.popup_content.IsVisible = false;
                } else if (_driverState == 1)
                {
                    //Searching for rides
                    this.setSearchingForRidesVisible();
                    this.popup_content.IsVisible = false;
                } else if (_driverState == 2)
                {
                    //Ride found, make sure to set current item value for class
                    this.setSearchingForRidesInvisible();
                    this.popup_content.IsVisible = true;
                } else if (_driverState == 3)
                {
                    //Routing driver to proper location

                } else
                {
                    //Set to default
                    _driverState = 0;
                    this.goButton.IsVisible = true;
                    this.setSearchingForRidesInvisible();
                    this.popup_content.IsVisible = false;
                }
            }
        }

        private int _driverState = 0;

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

            //Setup popup window
            this.popup_content.TranslationX = 0.05 * width;
            this.popup_content.TranslationY = 0.3 * width;
            this.popup_content.HeightRequest = height - (0.4 * width);
            this.popup_content.WidthRequest = 0.9 * width;
            this.popup_content.BackgroundColor = Xamarin.Forms.Color.White;

            var itemHeight = this.popup_content.HeightRequest * 0.2;
            this.item_image.HeightRequest = itemHeight;
            this.item_image.WidthRequest = itemHeight;
            this.item_image.TranslationY = 0.05 * this.popup_content.HeightRequest;
            this.item_image.TranslationX = (0.5 * this.popup_content.WidthRequest) - (itemHeight / 2);
            this.item_image.BorderColor = Xamarin.Forms.Color.Black;
            this.item_image.BorderWidth = 1;

            var textHeight = this.popup_content.HeightRequest * 0.1;
            var textWidth = this.popup_content.WidthRequest - (0.1 * width);
            var textTransX = 0.05 * width;

            this.product_name.Text = "Product Name";
            this.product_name.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.product_name.WidthRequest = textWidth;
            this.product_name.HeightRequest = textHeight;
            this.product_name.TranslationX = textTransX;
            this.product_name.TranslationY = 0.35 * this.popup_content.HeightRequest;

            String distanceAmount = "0";
            this.distance.Text = "Distance: " + distanceAmount + " mi away";
            this.distance.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.distance.WidthRequest = textWidth;
            this.distance.HeightRequest = textHeight;
            this.distance.TranslationX = textTransX;
            this.distance.TranslationY = 0.45 * this.popup_content.HeightRequest;

            this.address.Text = "Address";
            this.address.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.address.WidthRequest = textWidth;
            this.address.HeightRequest = textHeight;
            this.address.TranslationX = textTransX;
            this.address.TranslationY = 0.55 * this.popup_content.HeightRequest;

            //Circular buttons on bottom
            this.yes.Clicked += this.yesClicked;
            this.yes.BorderColor = Xamarin.Forms.Color.Black;
            this.yes.BorderWidth = 2;

            this.no.Clicked += this.noClicked;
            this.no.BorderColor = Xamarin.Forms.Color.Black;
            this.no.BorderWidth = 2;

            //Ensures corner radius is consistant
            var yesNoSize = (int) (0.25 * this.popup_content.WidthRequest);
            this.yes.HeightRequest = yesNoSize;
            this.yes.WidthRequest = yesNoSize;
            this.yes.CornerRadius = yesNoSize / 2;
            this.yes.TranslationX = 0.05 * this.popup_content.WidthRequest;
            this.yes.TranslationY = this.popup_content.HeightRequest - (0.05 * this.popup_content.WidthRequest) - yesNoSize;

            this.no.HeightRequest = yesNoSize;
            this.no.WidthRequest = yesNoSize;
            this.no.CornerRadius = yesNoSize / 2;
            this.no.TranslationX = this.popup_content.WidthRequest - (0.05 * this.popup_content.WidthRequest) - yesNoSize;
            this.no.TranslationY = this.popup_content.HeightRequest - (0.05 * this.popup_content.WidthRequest) - yesNoSize;

            this.popup_content.IsVisible = false;
        }

        //Trip confirmed
        private void yesClicked(object sender, EventArgs e)
        {
            //this.popup_content.IsVisible = false;
            this.DriverState = 0;
        }

        //Trip rejected
        private void noClicked(object sender, EventArgs e)
        {
            //this.popup_content.IsVisible = false;
            //this.setSearchingForRidesVisible();
            this.DriverState = 1;
        }

        //Begin waiting for rides
        private void goButtonClicked(object sender, EventArgs e)
        {
            //this.setSearchingForRidesVisible();
            this.DriverState = 1;

            //Subscribe
            FirebaseApi.firebaseClient.Child("locations").AsObservable<LocalLocation>().Subscribe(updatedLocations => this.handleSubscriptions(updatedLocations));
        }

        //Handle subscription to new locations added to database
        private void handleSubscriptions(Firebase.Database.Streaming.FirebaseEvent<LocalLocation> updatedLocations)
        {
            var timeDifference = (DateTime.Now - updatedLocations.Object.time).TotalSeconds;

            //DO NOT DELETE THIS, IT IS NECESSARY TO REMEMBER THIS FACET OF FIREBASE SUBSCRIPTIONS
            if (SECONDS_ONE_DAY > timeDifference)
            {
                if (this.currentlyGo && !addresses.ContainsKey(updatedLocations.Key))
                {
                    this.addresses.Add(updatedLocations.Key, updatedLocations.Object.address);

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        this.DriverState = 2;
                    });
                }
            }
        }

        //End waiting for rides
        private void cancelButtonClicked(object sender, EventArgs e)
        {
            //this.goButton.IsVisible = true;
            //this.setSearchingForRidesInvisible();
            this.DriverState = 0;
        }

        private void setSearchingForRidesInvisible()
        {
            //this.goButton.IsVisible = true;

            this.internal_relative.IsVisible = false;
            this.searching.IsVisible = false;
            this.cancel.IsVisible = false;
            this.activity.IsVisible = false;

            this.activity.IsRunning = false;
            this.currentlyGo = false;
        }

        private void setSearchingForRidesVisible()
        {
            this.goButton.IsVisible = false;

            this.internal_relative.IsVisible = true;
            this.searching.IsVisible = true;
            this.cancel.IsVisible = true;
            this.activity.IsVisible = true;

            this.activity.IsRunning = true;
            this.currentlyGo = true;
        }
    }
}
