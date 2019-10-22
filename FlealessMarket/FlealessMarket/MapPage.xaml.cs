using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            //Set locations
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            this.header.WidthRequest = width;
            this.footer.WidthRequest = width;
            this.map.WidthRequest = width;

            this.header.HeightRequest = 0.15 * width;
            this.footer.HeightRequest = 0.15 * width;
        }
    }
}
