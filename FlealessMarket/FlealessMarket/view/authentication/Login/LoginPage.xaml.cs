using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;
using Firebase.Database;
using System.Threading.Tasks;

namespace FlealessMarket
{
    public partial class LoginPage : ContentPage
    {
        private RelativeLayout loginBox;

        public LoginPage()
        {
            InitializeComponent();

            this.background.Source = "BluePurple";
            this.main.LowerChild(this.background);

            //Setup other UI components
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            var textColor = Xamarin.Forms.Color.White;//.FromHex("73A8A5");
            var textColorFree = Xamarin.Forms.Color.White;

            var subtractionValue = height * 0.05;

            this.icon.WidthRequest = height * 0.15;
            this.icon.HeightRequest = height * 0.15;
            this.icon.TranslationX = (width / 2) - (this.icon.WidthRequest / 2);
            this.icon.TranslationY = height * 0.1 - subtractionValue;
            this.icon.Source = "RecycleDollar";

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

            /*
            this.user.WidthRequest = width * 0.64;
            this.user.HeightRequest = height * 0.075;
            this.user.TranslationY = height * 0.52;
            this.user.TranslationX = width * 0.18;
            //this.signup.BorderWidth = 1;
            //this.signup.BorderColor = Xamarin.Forms.Color.Black;
            this.user.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.user.CornerRadius = (int)(this.user.HeightRequest * 0.5);
            //this.signup.TextColor = Xamarin.Forms.Color.Black;
            this.user.FontSize = height * 0.033;
            this.user.TextColor = textColor;*/

            this.email.Placeholder = "Email";
            this.password.Placeholder = "Password";

            this.email.WidthRequest = width * 0.75;
            this.email.HeightRequest = height * 0.075;
            this.email.TranslationX = width * 0.125;
            this.email.TranslationY = height * 0.45 - subtractionValue;

            this.password.WidthRequest = width * 0.75;
            this.password.HeightRequest = height * 0.075;
            this.password.TranslationX = width * 0.125;
            this.password.TranslationY = height * 0.55 - subtractionValue;

            //this.stack.WidthRequest = width * 0.5;
            //this.stack.HeightRequest = height * 0.05;
            //this.stack.TranslationX = width * 0.15;
            //this.stack.TranslationY = height * 0.63 - subtractionValue;

            //this.new_user.WidthRequest = width * 0.3;
            //this.new_user.HeightRequest = height * 0.05;
            //this.new_user.TranslationX = width * 0.15;
            //this.new_user.TranslationY = height * 0.65 - subtractionValue;
            //this.new_user.VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;
            //this.new_user.FontSize = height * 0.02;
            //this.signup.FontSize = this.new_user.FontSize;

            this.forgot.WidthRequest = width * 0.45;
            this.forgot.HeightRequest = height * 0.05;
            this.forgot.TranslationX = width * 0.47;
            this.forgot.TranslationY = height * 0.64 - subtractionValue;
            this.forgot.VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;
            this.forgot.BackgroundColor = Xamarin.Forms.Color.Transparent;
            this.forgot.FontSize = height * 0.02;

            this.key.WidthRequest = width * 0.64;
            this.key.HeightRequest = height * 0.075;
            this.key.TranslationX = width * 0.18;
            this.key.TranslationY = height * 0.67;
            this.key.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            //this.key.Text = "Sign In";
            this.key.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.key.CornerRadius = (int)(this.key.HeightRequest * 0.5);
            this.key.FontSize = height * 0.033;
            this.key.TextColor = textColor;
            //this.key.Source = "key";lo

            this.signup.WidthRequest = width * 0.3;
            this.signup.HeightRequest = height * 0.05;
            this.signup.TranslationX = width * 0.1;
            this.signup.TranslationY = height * 0.76;// - subtractionValue;
            this.signup.VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;
            this.signup.BackgroundColor = Xamarin.Forms.Color.Transparent;
            this.signup.FontSize = height * 0.02;

            /*
            this.forgot.WidthRequest = width * 0.3;
            this.forgot.HeightRequest = height * 0.05;
            this.forgot.TranslationX = width * 0.6;
            this.forgot.TranslationY = height * 0.65;*/

            /*
            this.or.WidthRequest = width * 0.1;
            this.or.HeightRequest = height * 0.1;
            this.or.TranslationY = height * 0.695;
            this.or.TranslationX = width * 0.45;
            this.or.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.or.VerticalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.or.FontSize = height * 0.033;
            this.or.TextColor = textColorFree;

            this.driver.WidthRequest = width * 0.64;
            this.driver.HeightRequest = height * 0.075;
            this.driver.TranslationY = height * 0.78;
            this.driver.TranslationX = width * 0.18;
            //this.login.BorderWidth = 1;
            //this.login.BorderColor = Xamarin.Forms.Color.Black;
            this.driver.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.driver.CornerRadius = (int)(this.driver.HeightRequest * 0.5);
            //this.login.TextColor = Xamarin.Forms.Color.Black;
            this.driver.FontSize = height * 0.033;
            this.driver.TextColor = textColor;*/

            /*
            this.or1.WidthRequest = width * 0.1;
            this.or1.HeightRequest = height * 0.1;
            this.or1.TranslationY = height * 0.74;
            this.or1.TranslationX = width * 0.45;
            this.or1.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.or1.VerticalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.or1.FontSize = height * 0.033;
            this.or1.TextColor = textColorFree;*/

            /*
            this.consignment.WidthRequest = width * 0.64;
            this.consignment.HeightRequest = height * 0.075;
            this.consignment.TranslationY = height * 0.88;
            this.consignment.TranslationX = width * 0.18;
            //this.login.BorderWidth = 1;
            //this.login.BorderColor = Xamarin.Forms.Color.Black;
            this.consignment.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.consignment.CornerRadius = (int)(this.driver.HeightRequest * 0.5);
            //this.login.TextColor = Xamarin.Forms.Color.Black;
            this.consignment.FontSize = height * 0.033;
            this.consignment.TextColor = textColor;*/

            /*
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
                Constraint.Constant(3));*/
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

        /*
        //Go back to login screen
        private void Login_OnClick(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }*/

        private void Back_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Signup_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupUser();
        }

        private void Forgot_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ForgotPassword();
        }

        private void User_Signin(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new SignupUser();
            var usersFirebase = Task.Run(async () => FirebaseApi.firebaseClient.Child("user").OnceAsync<UnknownUser>());

            //Attempt login
            if (this.email.Text != null && this.password.Text != null)
            {
                foreach (FirebaseObject<UnknownUser> currUser in usersFirebase.Result.Result)
                {
                    var user = currUser.Object;
                    if (user.email.ToLower().Equals(this.email.Text.ToLower()))
                    {
                        if (user.password.ToLower().Equals(this.password.Text.ToLower()))
                        {
                            Debug.WriteLine("User exists, logging in");
                            switch (user.type)
                            {
                                case (int)UserTypes.USER:
                                    //User
                                    FirebaseApi.LoginStatus = 1;
                                    break;
                                case (int)UserTypes.CONSIGNER:
                                    //Consignment
                                    FirebaseApi.LoginStatus = 2;
                                    break;
                                case (int)UserTypes.DRIVER:
                                    //Driver
                                    FirebaseApi.LoginStatus = 3;
                                    break;
                                default:
                                    break;
                            }
                            //FirebaseApi.LoginStatus = 1;
                        }
                        else
                        {
                            //Incorrect password
                            Debug.WriteLine("Incorrect Password");
                        }
                    }
                    else
                    {
                        //User does not exist
                        Debug.WriteLine("User does not exist");
                    }
                }
            }
        }

        private void Driver_Signin(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new SignupUser();
            var driversFirebase = Task.Run(async () => FirebaseApi.firebaseClient.Child("drivers").OnceAsync<Driver>());

            //Attempt login
            if (this.email.Text != null && this.password.Text != null)
            {
                foreach (FirebaseObject<Driver> currUser in driversFirebase.Result.Result)
                {
                    if (currUser.Object.username.ToLower().Equals(this.email.Text.ToLower()))
                    {
                        if (currUser.Object.password.ToLower().Equals(this.password.Text.ToLower()))
                        {
                            Debug.WriteLine("Driver exists, logging in");
                            FirebaseApi.LoginStatus = 3;
                        }
                        else
                        {
                            //Incorrect password
                            Debug.WriteLine("Incorrect Password");
                        }
                    }
                    else
                    {
                        //User does not exist
                        Debug.WriteLine("User does not exist");
                    }
                }
            }
        }

        private void Consignment_Signin(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new SignupUser();
            var usersFirebase = Task.Run(async () => FirebaseApi.firebaseClient.Child("consignment").OnceAsync<User>());

            //Attempt login
            if (this.email.Text != null && this.password.Text != null)
            {
                foreach (FirebaseObject<User> currUser in usersFirebase.Result.Result)
                {
                    if (currUser.Object.username.ToLower().Equals(this.email.Text.ToLower()))
                    {
                        if (currUser.Object.password.ToLower().Equals(this.password.Text.ToLower()))
                        {
                            Debug.WriteLine("Consignment store exists, logging in");
                            FirebaseApi.LoginStatus = 2;
                        }
                        else
                        {
                            //Incorrect password
                            Debug.WriteLine("Incorrect Password");
                        }
                    }
                    else
                    {
                        //User does not exist
                        Debug.WriteLine("User does not exist");
                    }
                }
            }
        }
    }
}
