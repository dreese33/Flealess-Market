using System;

using Xamarin.Forms;

namespace FlealessMarket.Items
{
    public class SingularCouch : ContentPage
    {
        public SingularCouch()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

