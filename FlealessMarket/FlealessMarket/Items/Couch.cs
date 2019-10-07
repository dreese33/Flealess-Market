using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class Couch : ContentPage
    {

        public Couch(ImageButton itemClicked)
        {
            InitializeComponent();

            ImageButton item = this.FindByName("item_image1") as ImageButton;
            item.Source = itemClicked.Source;
            item.BackgroundColor = Xamarin.Forms.Color.White;
        }

        private void pressed(object sender, EventArgs e)
        {
            ImageButton image = sender as ImageButton;
            Navigation.PushAsync(new ImagePage(image));
        }
    }
}
