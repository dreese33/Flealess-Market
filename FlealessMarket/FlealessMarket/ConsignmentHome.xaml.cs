using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class ConsignmentHome : ContentPage
    {
        public ConsignmentHome()
        {
            InitializeComponent();
        }

        //Throw item out
        private void Throw_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonateItem());
        }

        private void Sell_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MapPage());
        }
    }
}
