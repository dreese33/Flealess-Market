using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

using Firebase.Database;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using FlealessMarket.controller;

namespace FlealessMarket
{
    public partial class Home : ContentPage
    {
        private int currColumn = 0;
        private int currRow = 0;
        private readonly int generalNumberOfColumns = 3;

        //Actual number of rows present
        private int actualRows = 1;

        //Row definition for newly added rows
        private RowDefinition rowDefinition;

        private bool loading = true;

        private List<String> pulledItemKeys = new List<String>();

        private List<GenericItem> backingArray = new List<GenericItem>();

        //Create an arraylist for the currently backing array, in addition to the overall backing array pulled from the database
        //This arraylist will represent the current children contained allowing us to get information about them
        private List<GenericItem> currentArray = new List<GenericItem>();
        
        public Home()
        {
            InitializeComponent();

            //UI
            //Setup other UI components
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            var height = mainDisplay.Height / mainDisplay.Density;
            var width = mainDisplay.Width / mainDisplay.Density;

            this.category_label.HeightRequest = height * 0.05;
            this.category_label.WidthRequest = width * 0.4;
            this.category_label.TranslationX = width / 2 - this.category_label.WidthRequest / 2;
            this.category_label.TranslationY = height * 0.05;
            this.category_label.HorizontalTextAlignment = Xamarin.Forms.TextAlignment.Center;

            this.picker.HeightRequest = height * 0.05;
            this.picker.WidthRequest = width * 0.4;
            this.picker.TranslationY = height * 0.12;
            this.picker.TranslationX = width / 2 - this.picker.WidthRequest / 2;
            this.picker.HorizontalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;

            this.scroll.HeightRequest = height * 0.65;
            this.scroll.WidthRequest = width - 20;
            this.scroll.TranslationX = 0;
            this.scroll.TranslationY = this.picker.HeightRequest + this.picker.TranslationY + 0.05 * height;
            this.scroll.BackgroundColor = Xamarin.Forms.Color.White;//Xamarin.Forms.Color.FromHex("40b5bc");

            this.home_grid.HorizontalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;

            this.background_image.WidthRequest = width;
            this.background_image.HeightRequest = height + 20;
            this.background_image.TranslationX = 0;
            this.background_image.TranslationY = -50;

            this.main.LowerChild(this.background_image);

            //Change grid based on category changed
            this.picker.SelectedIndexChanged += selectGrid;

            //Square frame for grid elements
            double dimension = (width / 3) - 30; //Math.Abs((Application.Current.MainPage.Width / 3) - 20);

            ColumnDefinition columnDefinition = new ColumnDefinition();
            Debug.WriteLine("What is wrong with this: " + dimension);
            columnDefinition.Width = dimension;
            for (int i = 0; i < 3; i++)
            {
                this.home_grid.ColumnDefinitions.Add(columnDefinition);
            }

            rowDefinition = new RowDefinition();
            rowDefinition.Height = dimension;
            this.home_grid.RowDefinitions.Add(rowDefinition);

            this.Title = "Home";

            //Pull items from database
            var items = Task.Run(async () => AppClient.firebaseClient.Child("items").OnceAsync<GenericItem>());

            loading = true;

            foreach (FirebaseObject<GenericItem> item in items.Result.Result)
            {
                Debug.WriteLine(item.Object.description);
                try
                {
                    this.pulledItemKeys.Add(item.Key);
                    this.addItem(item.Object, item.Key);
                }
                catch (FirebaseException e)
                {
                    Debug.WriteLine("Exception occurred getting image from firebase storage: " + e.Message);
                }
            }

            loading = false;

            //Handle subscriptions
            AppClient.firebaseClient.Child("items").AsObservable<GenericItem>().Subscribe(newItem => this.handleSubscriptions(newItem));
        }

        //Handles subscriptions to items added to database
        private async void handleSubscriptions(Firebase.Database.Streaming.FirebaseEvent<GenericItem> newItem)
        {
            if (!this.pulledItemKeys.Contains(newItem.Key))
            {
                await Task.Delay(10000);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    this.addItem(newItem.Object, newItem.Key, true);
                });

                this.pulledItemKeys.Add(newItem.Key);
            }
        }

        //Adds item to the Grid
        private void addItem(GenericItem current, String itemKey, bool firebaseObject = false)
        {
            //Add item to backingArray
            current.itemKey = itemKey;
            if (this.loading)
            {
                //Loading items initially
                this.backingArray.Add(current);
            }

            ImageButton newButton = new ImageButton
            {
                Source = current.imageSource,
                BackgroundColor = Xamarin.Forms.Color.White,
                Aspect = Aspect.Fill,
                HorizontalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand,
                VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand
            };

            if (!firebaseObject)
            {
                newButton.Source = current.imageSource;
            } else
            {
                Debug.WriteLine("start");

                //Try to get image
                try
                {
                    String url = Task.Run(async () => await AppClient.firebaseStorage.Child("images").Child(current.imageSource).GetDownloadUrlAsync()).Result;

                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                    request.AllowReadStreamBuffering = true;

                    if (firebaseObject)
                    {
                        request.Timeout = 50000;
                    }
                    else
                    {
                        request.Timeout = 0;
                    }

                    System.Net.WebResponse response = request.GetResponse();
                    System.IO.Stream stream = response.GetResponseStream();
                    
                    MemoryStream ms = new MemoryStream();

                    stream.CopyTo(ms);
                    byte[] bytes = ms.ToArray();
                    current.imageBytes = bytes;

                    newButton.Source = ImageSource.FromStream(() => new MemoryStream(current.imageBytes));

                    //Use random address
                } catch (Exception e)
                {
                    Debug.WriteLine("Error occurred getting image from Firebase Storage " + e.Message);
                }
            }

            newButton.Clicked += genericItemClicked;

            Grid.SetRow(newButton, this.currRow);
            Grid.SetColumn(newButton, this.currColumn);

            if (this.currColumn == this.generalNumberOfColumns - 1)
            {
                var rowDefs = this.home_grid.RowDefinitions;
                if (this.actualRows >= rowDefs.Count) {
                    this.home_grid.RowDefinitions.Add(this.rowDefinition);
                }

                this.home_grid.Children.Add(newButton);
                this.currentArray.Add(current);

                this.currColumn = 0;
                this.currRow += 1;
                this.actualRows += 1;
            }
            else
            {
                this.home_grid.Children.Add(newButton);
                this.currentArray.Add(current);
                this.currColumn += 1;
            }
        }

        //Removes last item from the Grid
        private void popItem()
        {
            if (this.currColumn == 0)
            {
                if (this.currRow != 0)
                {
                    this.currRow -= 1;
                    this.actualRows -= 1;

                    var rowDefs = this.home_grid.RowDefinitions;

                    while (this.actualRows < rowDefs.Count && rowDefs.Count > 5)
                    {
                        rowDefs.RemoveAt(rowDefs.Count - 1);
                    }

                    this.currColumn = 0;
                    this.popGridCurrentArray();
                }
            } else
            {
                this.currColumn -= 1;
                this.popGridCurrentArray();
            }
        }

        //Removes last items from currentArray and homeGrid
        private void popGridCurrentArray()
        {
            this.currentArray.RemoveAt(this.currentArray.Count - 1);
            this.home_grid.Children.RemoveAt(this.home_grid.Children.Count - 1);
        }

        //Generic image object creation
        private void genericItemClicked(object sender, EventArgs e)
        {
            //0 should be replaced by the item row and item column
            ImageButton sent = sender as ImageButton;
            int index = this.home_grid.Children.IndexOf(sent);

            Navigation.PushAsync(new GenericItemPage(this.currentArray[index]));
        }

        //Throw item out
        private void Throw_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonateItem());
        }

        //This needs to be modified significantly for the functionality to work properly
        private void selectGrid(object sender, EventArgs e)
        {
            for (int i = 0; i < this.home_grid.Children.Count; i++)
            {
                this.popItem();
            }

            //Remove remaining children
            if (this.home_grid.Children.Count > 0 || this.currentArray.Count > 0)
            {
                this.home_grid.Children.Clear();
                this.currentArray.Clear();
            }

            //Add all items falling under the specified category
            Picker pick = sender as Picker;
            int pickIndex = pick.SelectedIndex;

            this.actualRows = 1;
            this.currRow = 0;
            this.currColumn = 0;

            Debug.WriteLine("Should be readding items now " + backingArray.Count);

            for (int i = 0; i < backingArray.Count; i++)
            {
                Debug.WriteLine("Readding item");
                GenericItem current = backingArray[i];
                if (Array.IndexOf(current.categories, pickIndex) > -1)
                {
                    //this.addItem(current);
                }
            }
        }

        private void Sell_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MapPage());
        }
    }
}
