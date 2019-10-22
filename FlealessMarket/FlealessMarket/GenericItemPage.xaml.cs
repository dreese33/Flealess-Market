using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class GenericItemPage : ContentPage
    {
        public GenericItemPage(GenericItem item)
        {
            InitializeComponent();

            //Get XAML components
            Image itemImage = this.FindByName("item_image") as Image;
            Label itemDescription = this.FindByName("item_description") as Label;
            Label itemTitle = this.FindByName("item_title") as Label; 

            //Assign XAML components properly
            itemImage.Source = item.imageSource;
            itemDescription.Text = item.description;
            itemTitle.Text = item.title;

            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplay.Width / mainDisplay.Density;
            var height = mainDisplay.Height / mainDisplay.Density;
            this.item_image.WidthRequest = width;
            this.item_image.HeightRequest = width;
        }
    }
}
