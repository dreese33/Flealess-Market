using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlealessMarket
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        private RelativeLayout loginBox;
        private ImageButton profilePicture;
        private Button loginButton;
        private Button signupButton;

        public MainPage()
        {
            InitializeComponent();

            loginBox = this.FindByName("login_box") as RelativeLayout;
            signupButton = this.FindByName("signup") as Button;
            loginButton = this.FindByName("login") as Button;
            profilePicture = this.FindByName("profile_picture") as ImageButton;

            this.Content.VerticalOptions = Xamarin.Forms.LayoutOptions.Center;
        }
    }
}
