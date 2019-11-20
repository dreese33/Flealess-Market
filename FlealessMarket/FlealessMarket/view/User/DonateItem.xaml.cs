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
        public DonateItem()
        {
            InitializeComponent();

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
            Navigation.PushAsync(new ItemUpload());
        }

        //Add items to database
        private void addPressed(object sender, EventArgs e)
        {
            if (this.file != null)
            {
                try
                {
                    Debug.WriteLine("Attempting");
                    Task.Run(async () => FirebaseApi.UploadImage(file.GetStream(), Path.GetFileName(file.Path)));

                    int[] categories = { -1 };
                    GenericItem newItem = new GenericItem(Path.GetFileName(file.Path), this.item_name.Text, this.item_description.Text, categories);
                    PostItem post = new PostItem(newItem);

                    Debug.WriteLine("Item created");

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(post);
                    var request = WebRequest.CreateHttp(AppClient.url + "items/.json");
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
                } catch (Exception exc)
                {
                    Debug.WriteLine("Exception occurred adding image to firebase " + exc.Message);
                }
            }

            Application.Current.MainPage = new NavigationPage(new UserHome());
        }

        private async Task display()
        {
            await DisplayAlert("Success", "Your item has been uploaded for consignment stores to see!", "Done");
        }

        private async Task failDisplay()
        {
            await DisplayAlert("Failure", "Failed to add to database", "Done");
        }
    }
}
