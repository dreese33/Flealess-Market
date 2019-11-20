using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;

namespace FlealessMarket
{
    public partial class UserHome : ContentPage
    {
        private String longDescription = "We are a Green and Eco Friendly app Do you have any furniture you would want to get rid of?" +
        "Why create more waste when you can help someone from your community reuse your furniture?!" +
        "With Flealess Market you can donate your furniture to the closest thrift store and you won't" +
        " need to deal with the moving. That's right! We are offering a free moving service for your free" +
            " item to one of our donation spots.It is Fast, Reliable, and helps the world." +
       "Throw = Put you furniture up for grabs and a local store will send a driver to come pick it up." +
"Sell = Bring your furniture to your closest thrift shop and they will sell it for you giving you commission for it." +
"Happy Earth Day! Because it is everyday.." +
"Create less, Reuse more!";

        public UserHome()
        {
            InitializeComponent();

            //this.Title = "Welcome Home";

            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplay.Width / mainDisplay.Density;
            var height = mainDisplay.Height / mainDisplay.Density;

            //Background
            this.background.TranslationX = 0;
            this.background.HeightRequest = height;
            this.background.TranslationY = 0;
            this.background.WidthRequest = width;
            this.background.Source = "BluePurple";
            this.background.Aspect = Aspect.AspectFill;

            this.main.LowerChild(this.background);

            this.bottom_bar.BackgroundColor = Color.FromHex("2E81A1");
            this.main.RaiseChild(this.bottom_bar);

            var addDimension = height * 0.1 - 14;
            this.add.HeightRequest = addDimension;
            this.add.WidthRequest = addDimension;
            this.add.TranslationX = (width / 2) - (addDimension / 2);
            this.add.TranslationY = 2;

            this.main.RaiseChild(this.add);

            this.logo.WidthRequest = height * 0.15;
            this.logo.HeightRequest = height * 0.15;
            this.logo.Margin = new Thickness { Top = 20 };
            this.logo.Source = "RecycleDollar";

            this.title.FontFamily = "Open Sans";

            this.title.WidthRequest = width * 0.9;
            this.title.HorizontalTextAlignment = TextAlignment.Center;
            this.title.FontSize = height * 0.075;
            this.title.TextColor = Color.White;
            this.title.Margin = new Thickness{ Bottom = 20 };

            this.description.Text = this.longDescription;
            this.description.Margin = new Thickness{ Right = 10, Left = 10 };
            this.description.FontSize = 15;
        }

        //Throw item out
        private void Throw_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonateItem());
        }
    }
}
