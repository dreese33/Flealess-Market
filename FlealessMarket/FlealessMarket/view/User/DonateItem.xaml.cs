using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Firebase.Database;
using Xamarin.Essentials;
using Plugin.Media;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Plugin.Media.Abstractions;
using System.Drawing.Imaging;
using System.Net;
using System.Text;
using Xamarin.Forms;
using FlealessMarket.controller;

namespace FlealessMarket
{
    public partial class DonateItem : ContentPage
    {
        MediaFile file;
        GenericItem item;
        public DonateItem(GenericItem item = null)
        {
            InitializeComponent();

            if (item != null)
            {
                this.item = item;
            } else
            {
                this.item = new GenericItem("", "", "", null);
            }

            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplay.Width / mainDisplay.Density;
            var height = mainDisplay.Height / mainDisplay.Density;

            this.map.HeightRequest = width;
            this.map.WidthRequest = width;
            
            Title = "New Listing";

            this.bottom_bar.BackgroundColor = Color.FromHex("2E81A1");

            //Background
            this.background.TranslationX = 0;
            this.background.HeightRequest = height;
            this.background.TranslationY = 0;
            this.background.WidthRequest = width;
            this.background.Source = "BluePurple";
            this.background.Aspect = Aspect.AspectFill;

            this.main.LowerChild(this.background);

            this.scroll.HeightRequest = height * 0.775;
            this.scroll.WidthRequest = width;
            this.scroll.TranslationX = 0;
            this.scroll.TranslationY = 0;

            this.address.TextColor = Color.Black;

            var nextDimension = height * 0.1 - 14;
            this.next.HeightRequest = nextDimension;
            this.next.WidthRequest = nextDimension;
            this.next.TranslationX = width - nextDimension - 5;
            this.next.TranslationY = 4;
            this.next.BackgroundColor = Color.Transparent;
        }

        private void nextPressed(object sender, EventArgs e)
        {
            int[] categories = { -1 };
            this.item = new GenericItem("", this.item_name.Text, this.item_description.Text, categories);
            Navigation.PushAsync(new ItemUpload(this.item));
        }
    }
}
