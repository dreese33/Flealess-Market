using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class ImagePage : ContentPage
    {
        public ImagePage(ImageButton image)
        {
            InitializeComponent();

            Image imageVal = this.FindByName("image") as Image;
            imageVal.Source = image.Source;
        }
    }
}
