using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;
using System.IO;

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

            itemDescription.Text = item.description;
            itemTitle.Text = item.title;

            /*
            if (item.path == "")
            {
                itemImage.Source = item.imageSource;
            } else
            {
                itemImage.Source = ImageSource.FromFile(item.path);
            }*/
            if (item.imageBytes == null)
            {
                itemImage.Source = item.imageSource;
            } else
            {
                Stream stream = new MemoryStream(item.imageBytes);
                itemImage.Source = ImageSource.FromStream(() => stream);
            }
            

            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplay.Width / mainDisplay.Density;
            var height = mainDisplay.Height / mainDisplay.Density;
            this.item_image.WidthRequest = width;
            this.item_image.HeightRequest = width;
        }
    }
}
