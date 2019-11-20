using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;

namespace FlealessMarket
{
    public partial class UserHome : ContentPage
    {
        private String longDescription = "We are a green and eco friendly app! Do you have any furniture you would like to get rid of? " +
        "Why create more waste when you can help someone from your community reuse your furniture! " +
        "With Flealess Market you can sell your furniture to the closest consignment store and you won't" +
        " need to deal with any pesky transportation. That's right! We are offering a service to transport your used furniture items" +
            " to our local consignment partners. It is fast, reliable, and helps reduce waste. " +
       "Simply press the add button at the bottom of the screen, and put you furniture up for all local consignment stores to see!" +
" Let's work together to create less waste. " +
"Happy Earth Day! Because it is everyday.." +
" Create less, Reuse more! Welcome to Flealess Market!";

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

            this.back.Source = "BackButton";
            this.back.WidthRequest = height * 0.05;
            this.back.HeightRequest = height * 0.05;
            this.back.TranslationX = width * 0.03;
            this.back.TranslationY = height * 0.05;
            this.back.BackgroundColor = Xamarin.Forms.Color.Transparent;
        }

        //Throw item out
        private void Throw_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonateItem());
        }

        private void Back_OnClicked(object sender, EventArgs e)
        {
            FirebaseApi.LoginStatus = -1;
        }
    }
}
