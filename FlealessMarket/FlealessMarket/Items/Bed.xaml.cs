﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket.Items
{
    public partial class Bed : ContentPage
    {
        public Bed(ImageButton itemClicked)
        {
            InitializeComponent();

            ImageButton item = this.FindByName("item_image") as ImageButton;
            item.Source = itemClicked.Source;
            item.BackgroundColor = Xamarin.Forms.Color.White;
        }

        private void onButtonClick(object sender, EventArgs e)
        {
            ImageButton image = sender as ImageButton;
            Navigation.PushAsync(new ImagePage(image));
        }
    }
}
