using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class DonateItem : ContentPage
    {
        private Button takePhoto;
        private Button uploadPhoto;

        public DonateItem()
        {
            InitializeComponent();

            this.takePhoto = this.FindByName("take_photo") as Button;
            this.uploadPhoto = this.FindByName("upload_photo") as Button;
            
            this.formatButton(this.takePhoto, 150);
            this.formatButton(this.uploadPhoto, 150);

            Title = "New Listing";
        }

        private void formatButton(Button button, int widthRequest = 100)
        {
            button.BorderColor = Xamarin.Forms.Color.White;
            button.BorderWidth = 1;
            button.WidthRequest = widthRequest;
            button.BackgroundColor = Xamarin.Forms.Color.LightBlue;
        }
    }
}
