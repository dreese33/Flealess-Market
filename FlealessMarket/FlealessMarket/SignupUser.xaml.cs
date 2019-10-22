using System;
using System.Collections.Generic;
using Firebase.Database;
using System.Diagnostics;
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
            }
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
    }
}
