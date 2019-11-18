using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using FlealessMarket.controller;

namespace FlealessMarket
{
    public partial class GenericItemPage : ContentPage
    {
        private GenericItem item;
        public GenericItemPage(GenericItem item)
        {
            InitializeComponent();

            //Get XAML components
            Image itemImage = this.FindByName("item_image") as Image;
            Label itemDescription = this.FindByName("item_description") as Label;
            Label itemTitle = this.FindByName("item_title") as Label;

            itemDescription.Text = item.description;
            itemTitle.Text = item.title;
            this.item = item;

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

            this.bottom_bar.BackgroundColor = Xamarin.Forms.Color.FromHex("40b5bc");
            /*
            this.bottom_bar.WidthRequest = width;
            this.bottom_bar.HeightRequest = 0.2 * height;
            this.bottom_bar.TranslationX = 0;
            this.bottom_bar.TranslationY = 0.7 * height;*/

            this.scroll_view.HeightRequest = 0.8 * height;
            this.scroll_view.WidthRequest = width;
            this.scroll_view.TranslationX = 0;
            this.scroll_view.TranslationY = 0;

            /*
            this.get_item.WidthRequest = 0.25 * width;
            this.get_item.HeightRequest = 0.1 * height;
            this.get_item.TranslationY = 0.015 * height;
            this.get_item.TranslationX = 0.6 * width;
            this.get_item.BorderColor = Xamarin.Forms.Color.White;
            this.get_item.TextColor = Xamarin.Forms.Color.Black;
            this.get_item.BorderWidth = 2;
            this.get_item.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            //this.get_item.TranslationY = 0.865 * height;
            this.get_item.Text = "Get Item";*/
        }

        //Add location to locations
        private void addPressed(object sender, EventArgs e)
        {
            if (this.item.itemKey != "")
            {
                var currentTime = DateTime.Now;
                //String time = currentTime.ToLongDateString();
                //Debug.WriteLine("The current time is: " + time);

                String address = "Not important";
                String itemKey = this.item.itemKey;

                LocalLocation location = new LocalLocation();
                location.address = address;
                location.itemKey = itemKey;
                location.time = currentTime;

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(location);
                var request = WebRequest.CreateHttp(AppClient.url + "locations/.json");
                request.Method = "POST";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                var response = request.GetResponse();
                json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                Debug.WriteLine(json);

                this.display();
                Debug.WriteLine("Succeeded");

                FirebaseApi.Set_Consign_Home();
                //Application.Current.MainPage = new NavigationPage(new Home());
            }
        }

        private async Task display()
        {
            await DisplayAlert("You're all set!", "Drivers in the area have been notified", "Done");
        }
    }
}
