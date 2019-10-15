using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class SignupUser : ContentPage
    {

        private RelativeLayout loginBox;
        private Button signup;
        private Button cancelButton;

        public SignupUser()
        {
            InitializeComponent();

            this.loginBox = this.FindByName("login_box") as RelativeLayout;
            this.signup = this.FindByName("signup_button") as Button;
            this.cancelButton = this.FindByName("cancel") as Button;

            this.cancelButton.BorderColor = Xamarin.Forms.Color.White;
            this.cancelButton.BorderWidth = 1;
            this.cancelButton.WidthRequest = 100;
            this.cancelButton.BackgroundColor = Xamarin.Forms.Color.LightBlue;

            this.signup.BorderColor = Xamarin.Forms.Color.White;
            this.signup.BorderWidth = 1;
            this.signup.WidthRequest = 100;
            this.signup.BackgroundColor = Xamarin.Forms.Color.LightBlue;

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
            Application.Current.MainPage = new NavigationPage(new Home());
        }

        private void Cancel_Pressed(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
