using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Media.Abstractions;

namespace FlealessMarket
{
    public partial class ItemUpload : ContentPage
    {
        MediaFile file;
        GenericItem item;
        public ItemUpload()
        {
            InitializeComponent();

            //this.item = item;

            this.Title = "Item Upload";

            //Setup other UI components
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            var textColor = Xamarin.Forms.Color.White;
            var textColorFree = Xamarin.Forms.Color.White;

            var subtractionValue = height * 0.2;

            this.title.WidthRequest = width * 0.9;
            this.title.TranslationY = height * 0.25 - subtractionValue;
            this.title.TranslationX = width * 0.05;
            this.title.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.title.FontSize = height * 0.075;
            this.title.TextColor = textColorFree;
            this.title.FontFamily = "Open Sans";

            this.description.WidthRequest = width * 0.9;
            this.description.TranslationX = width * 0.05;
            this.description.TranslationY = height * 0.35 - subtractionValue;
            this.description.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.description.FontSize = height * 0.03;
            this.description.TextColor = textColorFree;
            this.description.FontFamily = "Hwt-Artz";
            this.description.FontAttributes = FontAttributes.Italic;

            this.photo.WidthRequest = width * 0.75;
            this.photo.HeightRequest = width * 0.75;
            this.photo.TranslationX = width * 0.125;
            this.photo.TranslationY = height * 0.275;
            this.photo.BackgroundColor = Xamarin.Forms.Color.White;
            this.photo.BorderWidth = 1;
            this.photo.BorderColor = Xamarin.Forms.Color.Black;

            this.add_photo.WidthRequest = width * 0.15;
            this.add_photo.HeightRequest = width * 0.15;
            this.add_photo.TranslationX = this.photo.TranslationX + this.photo.WidthRequest - (this.add_photo.WidthRequest / 2);
            this.add_photo.TranslationY = this.photo.TranslationY + this.photo.HeightRequest - (this.add_photo.HeightRequest / 2);
            this.add_photo.BackgroundColor = Xamarin.Forms.Color.Transparent;

            this.upload.WidthRequest = width * 0.64;
            this.upload.HeightRequest = height * 0.075;
            this.upload.TranslationX = width * 0.18;
            this.upload.TranslationY = this.photo.TranslationY + this.photo.HeightRequest + (height * 0.05);
            this.upload.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.upload.CornerRadius = (int)(this.upload.HeightRequest * 0.5);
            this.upload.FontSize = height * 0.033;
            this.upload.TextColor = textColor;

            /*
            this.logo.WidthRequest = height * 0.125;
            this.logo.HeightRequest = height * 0.125;
            this.logo.TranslationX = (width / 2) - (this.logo.WidthRequest / 2);
            this.logo.TranslationY = height * 0.01;
            this.logo.Source = "RecycleDollar";*/

            this.main.LowerChild(this.background);
        }

        private async void Upload(object sender, EventArgs e)
        {

        }

        private async void Camera_OnClicked(object sender, EventArgs e)
        {
            String[] options = { "Take Photo", "Upload Photo" };
            String option = await DisplayActionSheet(null, "Cancel", null, options);

            if (option.Equals(options[0]))
            {
                this.takePhoto(sender, e);
            }
            else if (option.Equals(options[1]))
            {
                this.uploadPhoto(sender, e);
            }
        }

        private async void takePhoto(object sender, EventArgs e)
        {
            var newPhoto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync
                (new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    AllowCropping = true
                });
            if (newPhoto != null)
            {
                this.file = newPhoto;
                this.photo.Source = ImageSource
                    .FromStream(() => { return newPhoto.GetStreamWithImageRotatedForExternalStorage(); });
            }
        }

        //Pick photo from photos
        private async void uploadPhoto(object sender, EventArgs e)
        {
            var newPhoto = await Plugin.Media.CrossMedia.Current.PickPhotoAsync
                (new Plugin.Media.Abstractions.PickMediaOptions() { });
            if (newPhoto != null)
            {
                this.file = newPhoto;
                this.photo.Source = ImageSource
                    .FromStream(() => { return newPhoto.GetStreamWithImageRotatedForExternalStorage(); });
            }
        }
    }
}
