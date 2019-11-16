using System;
using System.Collections.Generic;
using Firebase.Database;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Net;

namespace FlealessMarket
{
    public partial class Login : ContentPage
    {
        private RelativeLayout loginBox;
        private Button login;
        private UnknownUser user;

        //Type explained: 0 - user, 1 - consignment store, 2 - driver

        public Login(UnknownUser user)
        {
            InitializeComponent();

            this.user = user;

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

            this.key_desc.WidthRequest = width * 0.75;
            this.key_desc.TranslationX = width * 0.125;
            this.key_desc.TranslationY = height * 0.45 - subtractionValue;
            this.key_desc.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.key_desc.FontSize = height * 0.03;
            this.key_desc.TextColor = textColorFree;

            this.key.WidthRequest = width * 0.64;
            this.key.HeightRequest = height * 0.075;
            this.key.TranslationX = width * 0.18;
            this.key.TranslationY = height * 0.5 - subtractionValue;
            this.key.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            //this.key.Text = "Sign In";
            this.key.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.key.CornerRadius = (int)(this.key.HeightRequest * 0.5);
            this.key.FontSize = height * 0.033;
            this.key.TextColor = textColor;

            this.driver_desc.WidthRequest = width * 0.75;
            this.driver_desc.TranslationX = width * 0.125;
            this.driver_desc.TranslationY = height * 0.62 - subtractionValue;
            this.driver_desc.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.driver_desc.FontSize = height * 0.03;
            this.driver_desc.TextColor = textColorFree;

            this.driver.WidthRequest = width * 0.64;
            this.driver.HeightRequest = height * 0.075;
            this.driver.TranslationY = height * 0.67 - subtractionValue;
            this.driver.TranslationX = width * 0.18;
            //this.login.BorderWidth = 1;
            //this.login.BorderColor = Xamarin.Forms.Color.Black;
            this.driver.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.driver.CornerRadius = (int)(this.driver.HeightRequest * 0.5);
            //this.login.TextColor = Xamarin.Forms.Color.Black;
            this.driver.FontSize = height * 0.033;
            this.driver.TextColor = textColor;

            this.consignment_desc.WidthRequest = width * 0.75;
            this.consignment_desc.TranslationX = width * 0.125;
            this.consignment_desc.TranslationY = height * 0.79 - subtractionValue;
            this.consignment_desc.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.consignment_desc.FontSize = height * 0.03;
            this.consignment_desc.TextColor = textColorFree;

            this.consignment.WidthRequest = width * 0.64;
            this.consignment.HeightRequest = height * 0.075;
            this.consignment.TranslationY = height * 0.84 - subtractionValue;
            this.consignment.TranslationX = width * 0.18;
            //this.login.BorderWidth = 1;
            //this.login.BorderColor = Xamarin.Forms.Color.Black;
            this.consignment.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.consignment.CornerRadius = (int)(this.driver.HeightRequest * 0.5);
            //this.login.TextColor = Xamarin.Forms.Color.Black;
            this.consignment.FontSize = height * 0.033;
            this.consignment.TextColor = textColor;
            /*
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
                Constraint.Constant(3));*/
        }

        private void Back_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupUser();
        }

        //TODO -- Add to db
        private void User_Signup(object sender, EventArgs e)
        {
            this.user.type = 0;
            this.Add_Firebase();
            FirebaseApi.LoginStatus = 1;
        }

        //TODO -- Add to db
        private void Driver_Signup(object sender, EventArgs e)
        {
            this.user.type = 2;
            Application.Current.MainPage = new DriverUpload(user);
            //this.Add_Firebase();
            //FirebaseApi.LoginStatus = 3;
        }

        //TODO -- Add to db
        private void Consignment_Signup(object sender, EventArgs e)
        {
            this.user.type = 1;
            Application.Current.MainPage = new ConsignInfo(user);
            //this.Add_Firebase();
            //FirebaseApi.LoginStatus = 2;
        }

        private void Add_Firebase()
        {
            try {
                //Insert into firebase
                //Add some kind of delay wheel, this sometimes takes time to process

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                Debug.WriteLine("Json currently looks like: " + json.ToString());
                var request = WebRequest.CreateHttp(FirebaseApi.url + "user/.json");
                request.Method = "POST";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                var response = request.GetResponse();
                json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                Debug.WriteLine(json);

                FirebaseApi.LoginStatus = 1;
            } catch (FirebaseException fireExc)
            {
                Debug.WriteLine(fireExc.Message);
            } catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }
        /*
        private void Signup_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupSelector();
        }

        //Gets basic info from Firebase
        private void Login_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (this.driver.IsToggled)
                {
                    this.driverLogin();
                } else if (this.consignment.IsToggled)
                {
                    this.consignmentLogin();
                }
                else
                {
                    this.userLogin();
                }
            } catch (FirebaseException fireExc)
            {
                Debug.WriteLine(fireExc.Message);
            } catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        private void driverLogin()
        {
            var driversFirebase = Task.Run(async () => FirebaseApi.firebaseClient.Child("drivers").OnceAsync<Driver>());

            //Attempt login
            if (this.username.Text != null && this.password.Text != null)
            {
                foreach (FirebaseObject<Driver> currUser in driversFirebase.Result.Result)
                {
                    if (currUser.Object.username.ToLower().Equals(this.username.Text.ToLower()))
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

        private void userLogin()
        {
            var usersFirebase = Task.Run(async () => FirebaseApi.firebaseClient.Child("users").OnceAsync<User>());

            //Attempt login
            if (this.username.Text != null && this.password.Text != null)
            {
                foreach (FirebaseObject<User> currUser in usersFirebase.Result.Result)
                {
                    if (currUser.Object.username.ToLower().Equals(this.username.Text.ToLower()))
                    {
                        if (currUser.Object.password.ToLower().Equals(this.password.Text.ToLower()))
                        {
                            Debug.WriteLine("User exists, logging in");
                            FirebaseApi.LoginStatus = 1;
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

        private void consignmentLogin()
        {
            var usersFirebase = Task.Run(async () => FirebaseApi.firebaseClient.Child("consignment").OnceAsync<User>());

            //Attempt login
            if (this.username.Text != null && this.password.Text != null)
            {
                foreach (FirebaseObject<User> currUser in usersFirebase.Result.Result)
                {
                    if (currUser.Object.username.ToLower().Equals(this.username.Text.ToLower()))
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

        //Toggle handlers, ensure that one toggle selected at a time
        private void Driver_Toggle(object sender, EventArgs e)
        {
            if (this.consignment.IsToggled)
            {
                this.consignment.IsToggled = false;
            }
        }

        private void Consignment_Toggle(object sender, EventArgs e)
        {
            if (this.driver.IsToggled)
            {
                this.driver.IsToggled = false;
            }
        }*/
    }
}
