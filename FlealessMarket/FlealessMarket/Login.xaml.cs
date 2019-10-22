using System;
using System.Collections.Generic;
using Firebase.Database;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Diagnostics;

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

        //Gets basic info from Firebase
        private void Login_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (this.driver.IsToggled)
                {
                    this.driverLogin();
                } else
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
    }
}
