using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Firebase.Database;
using Xamarin.Essentials;
using Plugin.Media;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class DonateItem : ContentPage
    {

        public DonateItem()
        {
            InitializeComponent();
            
            Home.formatButton(this.take_photo, 150);
            Home.formatButton(this.upload_photo, 150);

            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            double width = mainDisplay.Width / mainDisplay.Density;
            this.map.HeightRequest = width;
            this.map.WidthRequest = width;

            Title = "New Listing";
        }

        //Take photo
        private async void takePhoto(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync
                (new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            if (photo != null)
            {
                this.item_image.Source = ImageSource
                    .FromStream(() => { return photo.GetStream(); });
            }
        }

        //Pick photo from photos
        private async void uploadPhoto(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync
                (new Plugin.Media.Abstractions.PickMediaOptions() { });
            if (photo != null)
            {
                this.item_image.Source = ImageSource
                    .FromStream(() => { return photo.GetStream(); });
            }
        }

        //Add items to database
        private void addPressed(object sender, EventArgs e)
        {
            
        }
    }
}