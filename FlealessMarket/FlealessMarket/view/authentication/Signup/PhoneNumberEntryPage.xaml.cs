using System;
using Xamarin.Essentials;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class PhoneNumberEntryPage : ContentPage
    {
        private UnknownUser user;
        public PhoneNumberEntryPage(UnknownUser user)
        {
            InitializeComponent();

            this.user = user;

            this.background.Source = "BluePurple";
            this.main.LowerChild(this.background);

            //Setup other UI components
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            var textColor = Xamarin.Forms.Color.White;
            var textColorFree = Xamarin.Forms.Color.White;

            var subtractionValue = height * 0.05;

            this.logo.WidthRequest = height * 0.15;
            this.logo.HeightRequest = height * 0.15;
            this.logo.TranslationX = (width / 2) - (this.logo.WidthRequest / 2);
            this.logo.TranslationY = height * 0.1 - subtractionValue;
            this.logo.Source = "RecycleDollar";

            this.title.WidthRequest = width * 0.9;
            this.title.TranslationY = height * 0.25 - subtractionValue;
            this.title.TranslationX = width * 0.05;
            this.title.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.title.FontSize = height * 0.075;
            this.title.TextColor = textColorFree;
            this.title.FontFamily = "Open Sans";

            this.description.WidthRequest = width * 0.75;
            this.description.TranslationX = width * 0.125;
            this.description.TranslationY = height * 0.35 - subtractionValue;
            this.description.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.description.FontSize = height * 0.03;
            this.description.TextColor = textColorFree;
            this.description.FontFamily = "Hwt-Artz";
            this.description.FontAttributes = FontAttributes.Italic;

            this.back.Source = "BackButton";
            this.back.WidthRequest = height * 0.05;
            this.back.HeightRequest = height * 0.05;
            this.back.TranslationX = width * 0.03;
            this.back.TranslationY = height * 0.05;
            this.back.BackgroundColor = Xamarin.Forms.Color.Transparent;

            this.phone.Placeholder = "Phone Number";

            this.phone.WidthRequest = width * 0.75;
            this.phone.HeightRequest = height * 0.075;
            this.phone.TranslationX = width * 0.125;
            this.phone.TranslationY = height * 0.44 - subtractionValue;

            this.signup.WidthRequest = width * 0.64;
            this.signup.HeightRequest = height * 0.075;
            this.signup.TranslationX = width * 0.18;
            this.signup.TranslationY = height * 0.52;
            this.signup.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            //this.key.Text = "Sign In";
            this.signup.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.signup.CornerRadius = (int)(this.signup.HeightRequest * 0.5);
            this.signup.FontSize = height * 0.033;
            this.signup.TextColor = textColor;

            this.bottom_label.WidthRequest = width * 0.75;
            this.bottom_label.TranslationX = width * 0.125;
            this.bottom_label.TranslationY = height * 0.9;
            this.bottom_label.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.bottom_label.FontSize = height * 0.02;
            this.bottom_label.TextColor = textColorFree;
            this.bottom_label.FontFamily = "Hwt-Artz";
        }

        private void Back_OnClicked(object sender, EventArgs e)
        {
            switch (this.user.type) {
                case (int)UserTypes.USER:
                    Application.Current.MainPage = new SignupSelector(this.user);
                    break;
                case (int)UserTypes.CONSIGNER:
                    Application.Current.MainPage = new ConsignInfo(this.user);
                    break;
                case (int)UserTypes.DRIVER:
                    Application.Current.MainPage = new DriverUpload(this.user);
                    break;
                default:
                    Application.Current.MainPage = new SignupSelector(this.user);
                    break;
            }
        }

        private void Signup_OnClicked(object sender, EventArgs e)
        {
            //Upload to firebase
        }
    }
}
