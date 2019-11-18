using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class ConsignInfo : ContentPage
    {
        private ConsignUser user;
        public ConsignInfo(UnknownUser user)
        {
            InitializeComponent();

            this.user = new ConsignUser();
            this.user.name = user.name;
            this.user.email = user.email;
            this.user.password = user.password;
            this.user.phoneNumber = user.phoneNumber;
            this.user.type = user.type;

            if (user is ConsignUser)
            {
                ConsignUser consigner = (ConsignUser) user;
                this.store.Text = consigner.storeName;
                this.user.storeName = consigner.storeName;
            }

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

            this.store.Placeholder = "Store Name";

            this.store.WidthRequest = width * 0.75;
            this.store.HeightRequest = height * 0.075;
            this.store.TranslationX = width * 0.125;
            this.store.TranslationY = height * 0.44 - subtractionValue;

            this.signup.WidthRequest = width * 0.64;
            this.signup.HeightRequest = height * 0.075;
            this.signup.TranslationX = width * 0.18;
            this.signup.TranslationY = height * 0.52;
            this.signup.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
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
            Application.Current.MainPage = new SignupSelector(this.user);
        }

        private async void Continue(object sender, EventArgs e)
        {
            if (this.store.Text != null)
            {
                this.user.storeName = this.store.Text;
                Application.Current.MainPage = new PhoneNumberEntryPage(this.user);
            } else
            {
                await DisplayAlert("Missing Store Name", "Please enter your store name below", "Got it!");
            }
        }
    }
}
