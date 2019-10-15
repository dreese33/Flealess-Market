using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class GenericItemPage : ContentPage
    {
        public GenericItemPage(GenericItem item)
        {
            InitializeComponent();

            //Get XAML components
            ImageButton itemImage = this.FindByName("item_image") as ImageButton;
            Label itemDescription = this.FindByName("item_description") as Label;
            Label itemTitle = this.FindByName("item_title") as Label;
            Label itemPrice = this.FindByName("item_price") as Label;

            //Assign XAML components properly
            itemImage.Source = item.imageSource;
            itemDescription.Text = item.description;
            itemPrice.Text = item.price.ToString();
            itemTitle.Text = item.title;
        }

        private void onButtonClick(object sender, EventArgs e)
        {
            ImageButton image = sender as ImageButton;
            Navigation.PushAsync(new ImagePage(image));
        }
    }
}
