using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class Login : ContentPage
    {
        private RelativeLayout loginBox;
        private Button login;

        public Login()
        {
            InitializeComponent();

            this.loginBox = this.FindByName("login_box") as RelativeLayout;
            this.login = this.FindByName("login_button") as Button;

            this.login.BorderColor = Xamarin.Forms.Color.White;
            this.login.BorderWidth = 1;
            this.login.WidthRequest = 100;
            this.login.BackgroundColor = Xamarin.Forms.Color.LightBlue;

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
                Constraint.Constant(3));
        }

        private void Signup_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupSelector();
        }

        private void Login_OnClick(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Home());
        }
    }
}
