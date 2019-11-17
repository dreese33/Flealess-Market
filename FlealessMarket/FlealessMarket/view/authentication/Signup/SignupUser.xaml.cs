using System;
using System.Collections.Generic;
using Firebase.Database;
using System.Diagnostics;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using System.Text;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class SignupUser : ContentPage
    {

        private RelativeLayout loginBox;
        private Button cancelButton;

        public SignupUser()
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

            this.name.Placeholder = "Full Name";
            this.email.Placeholder = "Email";
            this.password.Placeholder = "Password";

            this.name.WidthRequest = width * 0.75;
            this.name.HeightRequest = height * 0.075;
            this.name.TranslationX = width * 0.125;
            this.name.TranslationY = height * 0.45 - subtractionValue;

            this.email.WidthRequest = width * 0.75;
            this.email.HeightRequest = height * 0.075;
            this.email.TranslationX = width * 0.125;
            this.email.TranslationY = height * 0.55 - subtractionValue;

            this.password.WidthRequest = width * 0.75;
            this.password.HeightRequest = height * 0.075;
            this.password.TranslationX = width * 0.125;
            this.password.TranslationY = height * 0.65 - subtractionValue;

            this.login.WidthRequest = width * 0.3;
            this.login.HeightRequest = height * 0.05;
            this.login.TranslationX = width * 0.1;
            this.login.TranslationY = height * 0.73 - subtractionValue;
            this.login.VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;
            this.login.BackgroundColor = Xamarin.Forms.Color.Transparent;
            this.login.FontSize = height * 0.02;
            //this.loginBox = this.FindByName("login_box") as RelativeLayout;
            //this.signup = this.FindByName("signup_button") as Button;
            //this.cancelButton = this.FindByName("cancel") as Button;

            this.key.WidthRequest = width * 0.64;
            this.key.HeightRequest = height * 0.075;
            this.key.TranslationX = width * 0.18;
            this.key.TranslationY = height * 0.74;
            this.key.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            this.key.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.key.CornerRadius = (int)(this.key.HeightRequest * 0.5);
            this.key.FontSize = height * 0.033;
            this.key.TextColor = textColor;

            this.bottom_label.WidthRequest = width * 0.75;
            this.bottom_label.TranslationX = width * 0.125;
            this.bottom_label.TranslationY = height * 0.9;
            this.bottom_label.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.bottom_label.FontSize = height * 0.02;
            this.bottom_label.TextColor = textColorFree;
            /*
            this.cancelButton.BorderColor = Xamarin.Forms.Color.White;
            this.cancelButton.BorderWidth = 1;
            this.cancelButton.WidthRequest = 100;
            this.cancelButton.BackgroundColor = Xamarin.Forms.Color.LightBlue;

            this.signup.BorderColor = Xamarin.Forms.Color.White;
            this.signup.BorderWidth = 1;
            this.signup.WidthRequest = 100;
            this.signup.BackgroundColor = Xamarin.Forms.Color.LightBlue;*/

            //this.Content.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;

            /*
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

        private void Signup_Clicked(object sender, EventArgs e)
        {
            if (this.name.Text != null)
            {
                if (this.email.Text != null)
                {
                    if (this.password.Text != null)
                    {
                        //Create account here
                        UnknownUser user = new UnknownUser();
                        user.name = this.name.Text;
                        user.email = this.email.Text;
                        user.password = this.password.Text;
                        Application.Current.MainPage = new SignupSelector(user);
                    } else
                    {
                        Debug.WriteLine("Password empty");
                    }
                } else
                {
                    Debug.WriteLine("Email empty");
                }
            } else
            {
                Debug.WriteLine("Name empty");
            }
            /*
            try
            {
                //Attempt to insert user into the database
                Entry[] entries = { this.first, this.last, this.email, this.phone,
                                    this.address1, this.city, this.state, this.zip,
                                    this.username, this.password };
                if (SignupUser.verifyAttributes(entries))
                {
                    User user = new User();
                    user.first = this.first.Text;
                    user.last = this.last.Text;
                    user.email = this.email.Text;

                    //Verify phone number is integer
                    Int64 phoneNumber;
                    if (Int64.TryParse(this.phone.Text, out phoneNumber)) {
                        user.phoneNumber = phoneNumber;
                    } else
                    {
                        Debug.WriteLine("Phone number needs to be an integer");
                        return;
                    }

                    user.address1 = this.address1.Text;
                    user.city = this.city.Text;
                    user.state = this.state.Text;

                    //Verify zip code is integer
                    int zipCode;
                    if (int.TryParse(this.zip.Text, out zipCode))
                    {
                        user.zip = zipCode;
                    } else
                    {
                        Debug.WriteLine("Zip code needs to be an integer");
                        return;
                    }

                    user.username = this.username.Text;
                    user.password = this.password.Text;

                    //Check suite
                    int suiteNumber;
                    if (this.suite.Text != null)
                    {
                        if (int.TryParse(this.suite.Text, out suiteNumber))
                        {
                            user.suite = suiteNumber;
                        } else
                        {
                            Debug.WriteLine("Suite must be an integer");
                            return;
                        }
                    } else
                    {
                        //Default suite
                        user.suite = 1;
                    }

                    //Check address line 2
                    if (this.address2.Text != null)
                    {
                        user.address2 = this.address2.Text;
                    } else
                    {
                        user.address2 = "empty";
                    }

                    user.profile = "link";

                    //Insert into firebase
                    //Add some kind of delay wheel, this sometimes takes time to process
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                    Debug.WriteLine("Json currently looks like: " + json.ToString());
                    var request = WebRequest.CreateHttp(FirebaseApi.url + "users/.json");
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    var buffer = Encoding.UTF8.GetBytes(json);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                    var response = request.GetResponse();
                    json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                    Debug.WriteLine(json);

                    FirebaseApi.LoginStatus = 1;
                } else
                {
                    Debug.WriteLine("Missing some required elements");
                }
            } catch (FirebaseException fireExc)
            {
                Debug.WriteLine(fireExc.Message);
            } catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }*/
        }

        //Verify signup attributes
        public static bool verifyAttributes(Entry[] entries)
        {
            foreach (Entry entry in entries)
            {
                if (entry.Text == null)
                {
                    Debug.WriteLine("Entry empty");
                    return false;
                }
            }
            
            return true;
        }

        //Cancel sign up
        private void Cancel_Pressed(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Back_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Signup_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignupUser();
        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}
