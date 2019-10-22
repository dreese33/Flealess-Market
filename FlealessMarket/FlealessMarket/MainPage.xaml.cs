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
