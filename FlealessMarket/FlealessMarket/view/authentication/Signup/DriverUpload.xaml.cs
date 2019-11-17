using System;
using System.Collections.Generic;
using Firebase.Database;
using System.Diagnostics;
using Xamarin.Forms;
using System.Text;
using System.IO;
using System.Net;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace FlealessMarket
{
    public partial class DriverUpload : ContentPage
    {
        /*
        private RelativeLayout loginBox;
        //private Button signup;
        private Button cancelButton;

        private Button uploadPhoto1;
        private Button uploadPhoto2;
        private Button takePhoto1;
        private Button takePhoto2;

        private Driver driver;*/

        //Were photos uploaded?
        private bool uploadedPhoto1 = false;
        private bool uploadedPhoto2 = false;

        private byte[] photo1;
        private byte[] photo2;

        private UnknownUser driver;

        String[] states = { "Upload vehicle registration below", "Upload driver license below" };

        private int _state;
        public int State
        {
            get { return _state; }
            set
            {
                _state = value;
                if (_state > -1 && _state < states.Length)
                {
                    this.description.Text = states[_state];
                } else
                {
                    _state = 0;
                }
            }
        }

        public DriverUpload(UnknownUser driver)
        {
            InitializeComponent();

            this.driver = driver;

            this.background.Source = "BluePurple";
            this.main.LowerChild(this.background);

            //Setup other UI components
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            var textColor = Xamarin.Forms.Color.White;//.FromHex("73A8A5");
            var textColorFree = Xamarin.Forms.Color.White;

            var subtractionValue = height * 0.13;

            /*
            this.icon.WidthRequest = height * 0.15;
            this.icon.HeightRequest = height * 0.15;
            this.icon.TranslationX = (width / 2) - (this.icon.WidthRequest / 2);
            this.icon.TranslationY = height * 0.1 - subtractionValue;
            this.icon.Source = "RecycleDollar";*/

            this.title.WidthRequest = width * 0.9;
            this.title.TranslationY = height * 0.25 - subtractionValue;
            this.title.TranslationX = width * 0.05;
            this.title.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.title.FontSize = height * 0.075;
            this.title.TextColor = textColorFree;
            this.title.FontFamily = "Open Sans";

            this.description.WidthRequest = width * 0.9;
            this.description.TranslationX = width * 0.05;
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

            this.photo.WidthRequest = width * 0.75;
            this.photo.HeightRequest = width * 0.75;
            this.photo.TranslationX = width * 0.125;
            this.photo.TranslationY = height * 0.3;
            this.photo.BackgroundColor = Xamarin.Forms.Color.White;
            this.photo.BorderWidth = 1;
            this.photo.BorderColor = Xamarin.Forms.Color.Black;

            this.add_photo.WidthRequest = width * 0.15;
            this.add_photo.HeightRequest = width * 0.15;
            this.add_photo.TranslationX = this.photo.TranslationX + this.photo.WidthRequest - (this.add_photo.WidthRequest / 2);
            this.add_photo.TranslationY = this.photo.TranslationY + this.photo.HeightRequest - (this.add_photo.HeightRequest / 2);
            this.add_photo.BackgroundColor = Xamarin.Forms.Color.Transparent;

            this.@continue.WidthRequest = width * 0.64;
            this.@continue.HeightRequest = height * 0.075;
            this.@continue.TranslationX = width * 0.18;
            this.@continue.TranslationY = this.photo.TranslationY + this.photo.HeightRequest + (height * 0.05);
            //this.@continue.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
            this.@continue.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.@continue.CornerRadius = (int)(this.@continue.HeightRequest * 0.5);
            this.@continue.FontSize = height * 0.033;
            this.@continue.TextColor = textColor;

            this.skip.WidthRequest = width * 0.3;
            this.skip.HeightRequest = height * 0.05;
            this.skip.TranslationX = width * 0.35;
            this.skip.TranslationY = this.@continue.TranslationY + this.@continue.HeightRequest + (0.02 * height);
            //this.skip.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;
            this.skip.FontSize = height * 0.03;

            //this.scroll.IsEnabled = true;
            //this.scroll.HeightRequest = height * 1.5;
            //this.scroll.VerticalOptions = Xamarin.Forms.LayoutOptions.FillAndExpand;
            /*
            this.driver = driver;

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
                Constraint.Constant(3));*/
        }

        private void Skip(object sender, EventArgs e)
        {
            Application.Current.MainPage = new PhoneNumberEntryPage(this.driver);
        }

        private void formatButton(Button button, int widthRequest = 100)
        {
            button.BorderColor = Xamarin.Forms.Color.White;
            button.BorderWidth = 1;
            button.WidthRequest = widthRequest;
            button.BackgroundColor = Xamarin.Forms.Color.LightBlue;
        }

        private void Back_OnClicked(object sender, EventArgs e)
        {
            if (State == 0)
            {
                Application.Current.MainPage = new SignupSelector(this.driver);
            } else
            {
                State = 0;

                MemoryStream ms = new MemoryStream(this.photo1);
                this.photo.Source = ImageSource.FromStream(() => ms);
            }
        }

        private async void Camera_OnClicked(object sender, EventArgs e)
        {
            String[] options = { "Take Photo", "Upload Photo" };
            String option = await DisplayActionSheet(null, "Cancel", null, options);

            if (option.Equals(options[0]))
            {
                this.takePhoto(sender, e);
            } else if (option.Equals(options[1]))
            {
                this.uploadPhoto(sender, e);
            }
        }

        private async void Continue(object sender, EventArgs e)
        {
            if (this.State == 0)
            {
                if (this.uploadedPhoto1)
                {
                    this.State = 1;
                    if (this.uploadedPhoto2)
                    {
                        MemoryStream ms = new MemoryStream(this.photo2);
                        this.photo.Source = ImageSource.FromStream(() => ms);
                    }
                    else
                    {
                        this.photo.Source = "camera";
                    }
                } else
                {
                    await DisplayAlert(null, "Hi there! You have not entered your vehicle registration yet. " +
                        "If you skip this part of registration, your account will not be complete.", "Got it!");
                }
            } else
            {
                if (this.uploadedPhoto1 && this.uploadedPhoto2)
                {
                    //Load next page, add photos to object passed through remaining classes
                    Application.Current.MainPage = new PhoneNumberEntryPage(this.driver);
                } else
                {
                    await DisplayAlert(null, "Hi there! You have not entered your driver license yet. " +
                        "If you skip this part of registration, your account will not be complete.", "Got it!");
                }
            }
        }

        //Take photo
        private async void takePhoto(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync
                (new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            if (photo != null)
            {
                this.photo.Source = ImageSource
                    .FromStream(() => { return photo.GetStream(); });

                this.updateState(photo.GetStream());
            }
        }

        //Pick photo from photos
        private async void uploadPhoto(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync
                (new Plugin.Media.Abstractions.PickMediaOptions() { });
            if (photo != null)
            {
                this.photo.Source = ImageSource
                    .FromStream(() => { return photo.GetStream(); });
            }

            this.updateState(photo.GetStream());
        }

        //Updates state for photos
        private void updateState(Stream photoStream)
        {
            if (this.State == 0)
            {
                this.uploadedPhoto1 = true;
                this.photo1 = DriverUpload.streamToByteArray(photoStream);
            }
            else
            {
                this.uploadedPhoto2 = true;
                this.photo2 = DriverUpload.streamToByteArray(photoStream);
            }
        }

        //Stream to bytearray
        public static byte[] streamToByteArray(Stream photoStream)
        {
            MemoryStream ms = new MemoryStream();

            photoStream.CopyTo(ms);
            return ms.ToArray();
        }

        //Firebase request to post driver object
        private void Signup_OnClicked(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NavigationPage(new Home());
            /*
            try
            {
                driver.registration = "link";
                driver.license = "link";

                //Insert into firebase
                //Add some kind of delay wheel, this sometimes takes time to process
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(driver);
                Debug.WriteLine("Json currently looks like: " + json.ToString());
                var request = WebRequest.CreateHttp(FirebaseApi.url + "drivers/.json");
                request.Method = "POST";
                request.ContentType = "application/json";
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                var response = request.GetResponse();
                json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                Debug.WriteLine(json);

                FirebaseApi.LoginStatus = 3;
            }
            catch (FirebaseException fireExc)
            {
                Debug.WriteLine(fireExc.Message);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }*/
        }

        private void Cancel_Pressed(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
