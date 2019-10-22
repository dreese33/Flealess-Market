using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Firebase.Database;
using Xamarin.Essentials;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class DonateItem : ContentPage
    {
        private Button takePhoto;
        private Button uploadPhoto;

        public DonateItem()
        {
            InitializeComponent();

            this.takePhoto = this.FindByName("take_photo") as Button;
            this.uploadPhoto = this.FindByName("upload_photo") as Button;
            
            Home.formatButton(this.takePhoto, 150);
            Home.formatButton(this.uploadPhoto, 150);

            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            double width = mainDisplay.Width / mainDisplay.Density;
            this.map.HeightRequest = width;
            this.map.WidthRequest = width;

            Title = "New Listing";
        }

        

        private void addPressed(object sender, EventArgs e)
        {
            
        }
    }
}