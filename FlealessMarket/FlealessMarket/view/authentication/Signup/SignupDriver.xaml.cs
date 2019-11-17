using System;
using System.Diagnostics;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class SignupDriver : ContentPage
    {

        private RelativeLayout loginBox;
        private Button signup;
        private Button cancelButton;

        public SignupDriver()
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

        //Verify attributes are filled in, and create basic driver object
        private void Next_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Attempt to insert user into the database
                Entry[] entries = { this.first, this.last, this.email, this.phone,
                                    this.address1, this.city, this.state, this.zip,
                                    this.username, this.password };

                Entry badEntry = UIController.verifyAttributes(entries);
                if (badEntry == null)
                {
                    Driver driver = new Driver();
                    driver.first = this.first.Text;
                    driver.last = this.last.Text;
                    driver.email = this.email.Text;

                    //Verify phone number is integer
                    Int64 phoneNumber;
                    if (Int64.TryParse(this.phone.Text, out phoneNumber))
                    {
                        driver.phoneNumber = phoneNumber;
                    }
                    else
                    {
                        Debug.WriteLine("Phone number needs to be an integer");
                        return;
                    }

                    driver.address1 = this.address1.Text;
                    driver.city = this.city.Text;
                    driver.state = this.state.Text;

                    //Verify zip code is integer
                    int zipCode;
                    if (int.TryParse(this.zip.Text, out zipCode))
                    {
                        driver.zip = zipCode;
                    }
                    else
                    {
                        Debug.WriteLine("Zip code needs to be an integer");
                        return;
                    }

                    driver.username = this.username.Text;
                    driver.password = this.password.Text;

                    //Check suite
                    int suiteNumber;
                    if (this.suite.Text != null)
                    {
                        if (int.TryParse(this.suite.Text, out suiteNumber))
                        {
                            driver.suite = suiteNumber;
                        }
                        else
                        {
                            Debug.WriteLine("Suite must be an integer");
                            return;
                        }
                    }
                    else
                    {
                        //Default suite
                        driver.suite = 1;
                    }

                    //Check address line 2
                    if (this.address2.Text != null)
                    {
                        driver.address2 = this.address2.Text;
                    }
                    else
                    {
                        driver.address2 = "empty";
                    }

                    driver.profile = "link";

                    Application.Current.MainPage = new VehicleRegistration(driver);
                }
                else
                {
                    Debug.WriteLine("Missing some required elements");
                }
            } catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        private void Cancel_Pressed(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
