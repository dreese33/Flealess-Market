using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using Firebase.Database;
using Xamarin.Essentials;
using Plugin.Media;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Plugin.Media.Abstractions;
using System.Drawing.Imaging;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class DonateItem : ContentPage
    {
        MediaFile file;
        public DonateItem()
        {
            InitializeComponent();
            
            Home.formatButton(this.take_photo, 150);
            Home.formatButton(this.upload_photo, 150);

            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            double width = mainDisplay.Width / mainDisplay.Density;
            this.map.HeightRequest = width;
            this.map.WidthRequest = width;

            Title = "New Listing";
        }

        //Take photo
        private async void takePhoto(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync
                (new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            if (photo != null)
            {
                this.file = photo;
                this.item_image.Source = ImageSource
                    .FromStream(() => { return photo.GetStream(); });
            }
        }

        //Pick photo from photos
        private async void uploadPhoto(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync
                (new Plugin.Media.Abstractions.PickMediaOptions() { });
            if (photo != null)
            {
                this.file = photo;
                this.item_image.Source = ImageSource
                    .FromStream(() => { return photo.GetStream(); });
            }
        }

        //Add items to database
        private void addPressed(object sender, EventArgs e)
        {
            //Convert image to stream
            //var imageSource = (StreamImageSource)this.item_image.Source;
            //System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            //var stream = imageSource.Stream(cancellationToken).Result;

            //Debug.WriteLine("Starting attempt " + this.item_image.Source.ToString());
           // String url = this.UploadFile(stream, ).Result;

            //this.item_image
            //GenericItem itemToSend = new GenericItem();

            if (this.file != null)
            {
                try
                {
                    Debug.WriteLine("Attempting");

                    //ImageSource source = ImageSource.FromFile(file.Path);
                    //var image = System.Drawing.Image.FromFile(file.Path);
                    //image.Save(file.GetStream(), ImageFormat.Jpeg);


                    //https://stackoverflow.com/questions/1669850/high-quality-jpeg-compression-with-c-sharp
                    //System.Drawing.Bitmap bm = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(this.file.Path);
                    //ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                    //ImageCodecInfo ici = null;

                    //foreach (ImageCodecInfo codec in codecs)
                    //{ 
                    //    if (codec.MimeType == "image/jpeg") 
                    //    ici = codec; 
                    //} 

                    //EncoderParameters ep = new EncoderParameters(); 
                    //ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)25);
                    //bm.Save(file.GetStream(), ici, ep);
                    //bm.Save("C:\\quality" + x.ToString() + ".jpg", ici, ep);

                    //String url = this.UploadFile(file.GetStream(), Path.GetFileName(file.Path)).Result;
                    Task.Run(async () => this.UploadFile(file.GetStream(), Path.GetFileName(file.Path)));

                    //Debug.WriteLine("Url created");

                    int[] categories = { -1 };
                    GenericItem newItem = new GenericItem(Path.GetFileName(file.Path), this.item_name.Text, this.item_description.Text, categories);
                    PostItem post = new PostItem(newItem);

                    Debug.WriteLine("Item created");

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(post);
                    var request = WebRequest.CreateHttp(FirebaseApi.url + "items/.json");
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    var buffer = Encoding.UTF8.GetBytes(json);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                    var response = request.GetResponse();
                    json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
                    Debug.WriteLine(json);

                    this.display();
                    Debug.WriteLine("Succeeded");
                } catch (Exception exc)
                {
                    Debug.WriteLine("Exception occurred adding image to firebase " + exc.Message);
                }
            }

            Application.Current.MainPage = new NavigationPage(new UserHome());
        }

        private async Task display()
        {
            await DisplayAlert("Success", "Added to database", "Done");
        }

        private async Task failDisplay()
        {
            await DisplayAlert("Failure", "Failed to add to database", "Done");
        }

        private async Task UploadFile(Stream fileStream, string fileName)
        {
            await FirebaseApi.firebaseStorage
                .Child("images")
                .Child(fileName)
                .PutAsync(fileStream);

        }
    }
}
