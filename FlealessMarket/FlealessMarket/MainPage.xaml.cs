using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Diagnostics;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Essentials;

namespace FlealessMarket
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static Configuration config;
        private RelativeLayout loginBox;

        public MainPage()
        {
            InitializeComponent();

            //Initialize firebase
            new FirebaseApi();

            loginBox = this.FindByName("login_box") as RelativeLayout;

            this.Content.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;

            var verticalBoxView = new BoxView
            {
                BackgroundColor = Xamarin.Forms.Color.White,
            };

            var horizontalBoxView = new BoxView
            {
                BackgroundColor = Xamarin.Forms.Color.White,
            };

            //Add right side border
            /*
            loginBox.Children.Add(verticalBoxView,
                Constraint.RelativeToParent((parent) => {
                return parent.Width - 3;
                }),
                Constraint.Constant(0),
                Constraint.Constant(3), Constraint.RelativeToParent((parent) => {
                    return parent.Height;
                }));

            //Added bottom border
            loginBox.Children.Add(horizontalBoxView,
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height - 3;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.Constant(3));*/

            //Setup background image UI
            this.background.Source = "BluePurple";
            this.main.LowerChild(this.background);

            //Setup other UI components
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            var textColor = Xamarin.Forms.Color.White;//.FromHex("73A8A5");
            var textColorFree = Xamarin.Forms.Color.White;
            //var textColor = Xamarin.Forms.Color.FromHex("73A8A5");

            this.title.WidthRequest = width * 0.9;
            this.title.TranslationY = height * 0.1;
            this.title.TranslationX = width * 0.05;
            this.title.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.title.FontSize = height * 0.075;
            this.title.TextColor = textColorFree;
            this.title.FontFamily = "Open Sans";

            this.description.WidthRequest = width * 0.75;
            this.description.TranslationX = width * 0.125;
            this.description.TranslationY = height * 0.2;
            this.description.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.description.FontSize = height * 0.03;
            this.description.TextColor = textColorFree;
            this.description.FontFamily = "Hwt-Artz";
            this.description.FontAttributes = FontAttributes.Italic;

            this.icon.WidthRequest = height * 0.25;
            this.icon.HeightRequest = height * 0.25;
            this.icon.TranslationX = (width / 2) - (this.icon.WidthRequest / 2);
            this.icon.TranslationY = height * 0.3;
            this.icon.Source = "RecycleDollar";

            this.signup.WidthRequest = width * 0.5;
            this.signup.HeightRequest = height * 0.075;
            this.signup.TranslationY = height * 0.6;
            this.signup.TranslationX = width * 0.25;
            //this.signup.BorderWidth = 1;
            //this.signup.BorderColor = Xamarin.Forms.Color.Black;
            this.signup.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.signup.CornerRadius = (int)(this.signup.HeightRequest * 0.5);
            //this.signup.TextColor = Xamarin.Forms.Color.Black;
            this.signup.FontSize = height * 0.033;
            this.signup.TextColor = textColor;

            this.or.WidthRequest = width * 0.1;
            this.or.HeightRequest = height * 0.1;
            this.or.TranslationY = height * 0.6825;
            this.or.TranslationX = width * 0.45;
            this.or.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.or.VerticalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.or.FontSize = height * 0.033;
            this.or.TextColor = textColor;

            this.login.WidthRequest = width * 0.5;
            this.login.HeightRequest = height * 0.075;
            this.login.TranslationY = height * 0.8;
            this.login.TranslationX = width * 0.25;
            //this.login.BorderWidth = 1;
            //this.login.BorderColor = Xamarin.Forms.Color.Black;
            this.login.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.login.CornerRadius = (int)(this.login.HeightRequest * 0.5);
            //this.login.TextColor = Xamarin.Forms.Color.Black;
            this.login.FontSize = height * 0.033;
            this.login.TextColor = textColor;

            this.bottom_label.WidthRequest = width * 0.75;
            this.bottom_label.TranslationX = width * 0.125;
            this.bottom_label.TranslationY = height * 0.9;
            this.bottom_label.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.bottom_label.FontSize = height * 0.02;
            this.bottom_label.TextColor = textColorFree;
            this.bottom_label.FontFamily = "Hwt-Artz";
        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();//new NavigationPage(new Home());
        }

        private void Signup_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupSelector();
        }
    }
}
