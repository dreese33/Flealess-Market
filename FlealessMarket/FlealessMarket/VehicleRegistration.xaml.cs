using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class VehicleRegistration : ContentPage
    {

        private RelativeLayout loginBox;
        private Button next;
        private Button cancelButton;

        public VehicleRegistration()
        {
            InitializeComponent();

            this.loginBox = this.FindByName("login_box") as RelativeLayout;
            this.next = this.FindByName("next_button") as Button;
            this.cancelButton = this.FindByName("cancel") as Button;

            this.cancelButton.BorderColor = Xamarin.Forms.Color.White;
            this.cancelButton.BorderWidth = 1;
            this.cancelButton.WidthRequest = 100;
            this.cancelButton.BackgroundColor = Xamarin.Forms.Color.LightBlue;

            this.next.BorderColor = Xamarin.Forms.Color.White;
            this.next.BorderWidth = 1;
            this.next.WidthRequest = 100;
            this.next.BackgroundColor = Xamarin.Forms.Color.LightBlue;

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

        private void Next_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new DriverUpload();
        }

        private void Cancel_Pressed(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage(); //Replace with registration/drivers license upload page
        }
    }
}
