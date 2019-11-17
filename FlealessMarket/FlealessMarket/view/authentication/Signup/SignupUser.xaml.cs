using System;
using System.Collections.Generic;
using Firebase.Database;
using System.Diagnostics;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Text;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class SignupUser : ContentPage
    {

        private RelativeLayout loginBox;
        private Button cancelButton;

        public SignupUser()
        {
            InitializeComponent();

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

            this.name.Placeholder = "Full Name";
            this.email.Placeholder = "Email";
            this.password.Placeholder = "Password";

            this.name.WidthRequest = width * 0.75;
            this.name.HeightRequest = height * 0.075;
            this.name.TranslationX = width * 0.125;
            this.name.TranslationY = height * 0.45 - subtractionValue;

            this.email.WidthRequest = width * 0.75;
            this.email.HeightRequest = height * 0.075;
            this.email.TranslationX = width * 0.125;
            this.email.TranslationY = height * 0.55 - subtractionValue;

            this.password.WidthRequest = width * 0.75;
            this.password.HeightRequest = height * 0.075;
            this.password.TranslationX = width * 0.125;
            this.password.TranslationY = height * 0.65 - subtractionValue;

            this.login.WidthRequest = width * 0.3;
            this.login.HeightRequest = height * 0.05;
            this.login.TranslationX = width * 0.1;
            this.login.TranslationY = height * 0.73 - subtractionValue;
            this.login.VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;
            this.login.BackgroundColor = Xamarin.Forms.Color.Transparent;
            this.login.FontSize = height * 0.02;

            this.signup.WidthRequest = width * 0.64;
            this.signup.HeightRequest = height * 0.075;
            this.signup.TranslationX = width * 0.18;
            this.signup.TranslationY = height * 0.74;
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
        }

        private void Signup_Clicked(object sender, EventArgs e)
        {
            if (this.name.Text != null)
            {
                if (this.email.Text != null)
                {
                    if (this.password.Text != null)
                    {
                        //Create account here
                        UnknownUser user = new UnknownUser();
                        user.name = this.name.Text;
                        user.email = this.email.Text;
                        user.password = this.password.Text;
                        Application.Current.MainPage = new SignupSelector(user);
                    } else
                    {
                        Debug.WriteLine("Password empty");
                    }
                } else
                {
                    Debug.WriteLine("Email empty");
                }
            } else
            {
                Debug.WriteLine("Name empty");
            }           
        }

        private void Back_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}
