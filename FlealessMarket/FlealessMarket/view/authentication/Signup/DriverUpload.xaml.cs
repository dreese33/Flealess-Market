using System;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;
using Plugin.Media.Abstractions;

namespace FlealessMarket
{
    public partial class DriverUpload : ContentPage
    {
        //Were photos uploaded?
        private bool uploadedPhoto1 = false;
        private bool uploadedPhoto2 = false;

        private byte[] photo1;
        private byte[] photo2;

        private DriverUser driver;

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

            this.driver = new DriverUser();
            this.driver.name = driver.name;
            this.driver.email = driver.email;
            this.driver.password = driver.password;
            this.driver.phoneNumber = driver.phoneNumber;
            this.driver.type = driver.type;

            this.background.Source = "BluePurple";
            this.main.LowerChild(this.background);

            //Setup other UI components
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            var textColor = Xamarin.Forms.Color.White;
            var textColorFree = Xamarin.Forms.Color.White;

            var subtractionValue = height * 0.13;
            
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
            this.@continue.BackgroundColor = Xamarin.Forms.Color.FromHex("483df6");
            this.@continue.CornerRadius = (int)(this.@continue.HeightRequest * 0.5);
            this.@continue.FontSize = height * 0.033;
            this.@continue.TextColor = textColor;

            this.skip.WidthRequest = width * 0.3;
            this.skip.HeightRequest = height * 0.05;
            this.skip.TranslationX = width * 0.35;
            this.skip.TranslationY = this.@continue.TranslationY + this.@continue.HeightRequest + (0.02 * height);
            this.skip.FontSize = height * 0.03;
        }

        private void Skip(object sender, EventArgs e)
        {
            Application.Current.MainPage = new PhoneNumberEntryPage(this.driver);
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
                    Application.Current.MainPage = new PhoneNumberEntryPage(this.driver, this.photo1, this.photo2);
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
                (new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    AllowCropping = true
                });
            if (photo != null)
            {
                this.photo.Source = ImageSource
                    .FromStream(() => { return photo.GetStreamWithImageRotatedForExternalStorage(); });

                this.updateState(photo);
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
                    .FromStream(() => { return photo.GetStreamWithImageRotatedForExternalStorage(); });
            }

            this.updateState(photo);
        }

        //Updates state for photos
        private void updateState(MediaFile newPhoto)
        {
            if (this.State == 0)
            {
                this.uploadedPhoto1 = true;
                this.photo1 = UIController.streamToByteArray(newPhoto.GetStreamWithImageRotatedForExternalStorage());
                this.driver.driverRegistration = Path.GetFileName(newPhoto.Path);
            }
            else
            {
                this.uploadedPhoto2 = true;
                this.photo2 = UIController.streamToByteArray(newPhoto.GetStreamWithImageRotatedForExternalStorage());
                this.driver.driverLicense = Path.GetFileName(newPhoto.Path);
            }
        }
    }
}
