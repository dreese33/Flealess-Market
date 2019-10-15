using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class DriverUpload : ContentPage
    {

        private RelativeLayout loginBox;
        private Button signup;
        private Button cancelButton;

        private Button uploadPhoto1;
        private Button uploadPhoto2;
        private Button takePhoto1;
        private Button takePhoto2;

        public DriverUpload()
        {
            InitializeComponent();

            this.loginBox = this.FindByName("login_box") as RelativeLayout;
            this.signup = this.FindByName("signup_button") as Button;
            this.cancelButton = this.FindByName("cancel") as Button;

            this.uploadPhoto1 = this.FindByName("upload_photo1") as Button;
            this.uploadPhoto2 = this.FindByName("upload_photo2") as Button;
            this.takePhoto1 = this.FindByName("take_photo1") as Button;
            this.takePhoto2 = this.FindByName("take_photo2") as Button;

            this.formatButton(this.cancelButton);
            this.formatButton(this.signup);
            this.formatButton(this.uploadPhoto1, 150);
            this.formatButton(this.uploadPhoto2, 150);
            this.formatButton(this.takePhoto1, 150);
            this.formatButton(this.takePhoto2, 150);

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

        private void formatButton(Button button, int widthRequest = 100)
        {
            button.BorderColor = Xamarin.Forms.Color.White;
            button.BorderWidth = 1;
            button.WidthRequest = widthRequest;
            button.BackgroundColor = Xamarin.Forms.Color.LightBlue;
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
