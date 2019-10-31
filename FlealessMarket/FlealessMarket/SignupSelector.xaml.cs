using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class SignupSelector : ContentPage
    {
        private RelativeLayout loginBox;

        public SignupSelector()
        {
            InitializeComponent();

            this.loginBox = this.FindByName("login_box") as RelativeLayout;

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

        private void Driver_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupDriver();
        }

        private void User_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupUser();
        }

        private void Consignment_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupUser();
        }

        //Load information (explanation about drivers/users)
        private void Info_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Home());
        }

        //Go back to login screen
        private void Login_OnClick(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }
    }
}
