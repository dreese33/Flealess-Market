using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

using Firebase.Database;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using FlealessMarket.controller;
using System.Linq;
using System.Resources;

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

        public static bool loading = true;

        public static List<String> pulledItemKeys = new List<String>();

        public static List<GenericItem> backingArray = new List<GenericItem>();
        public static List<GenericItem> pullFromBytes = new List<GenericItem>();

        private bool firebaseLock = false;
        private bool secondLock = false;

        private Queue<GenericItem> imageQueue = new Queue<GenericItem>();

        private String[] backingFiles = { "closet.jpg", "computer_chair.jpg", "couch.jpg",
            "chair.jpg", "strolly.jpg", "bed.jpg", "singular_couch.jpg",
            "sofa.jpg", "table.jpg", "table_set.jpg", "yard_chair.jpg", "yard_set.jpg"
        }; 

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

            this.scroll.TranslationY = this.picker.HeightRequest + this.picker.TranslationY + 0.05 * height;
            this.scroll.HeightRequest = height * 0.675;
            this.scroll.WidthRequest = width;
            this.scroll.TranslationX = 0;

            this.home_grid.HorizontalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand;

            this.background_image.WidthRequest = width;
            this.background_image.HeightRequest = height + 20;
            this.background_image.TranslationX = 0;
            this.background_image.TranslationY = -50;

            this.main.LowerChild(this.background_image);

            //Change grid based on category changed
            this.picker.SelectedIndexChanged += selectGrid;

            //Square frame for grid elements
            double dimension = (width / 3) - 6.6667;

            ColumnDefinition columnDefinition = new ColumnDefinition();
            Debug.WriteLine("What is wrong with this: " + dimension);
            columnDefinition.Width = dimension;
            for (int i = 0; i < 3; i++)
            {
                this.home_grid.ColumnDefinitions.Add(columnDefinition);
            }

            //this.home_grid.HorizontalOptions = Xamarin.Forms.LayoutOptions.Center;

            rowDefinition = new RowDefinition();
            rowDefinition.Height = dimension;
            this.home_grid.RowDefinitions.Add(rowDefinition);

            this.Title = "Home";

            //Pull items from database
            if (Home.loading)
            {
                Debug.WriteLine("Begin loading");
                var items = Task.Run(async () => AppClient.firebaseClient.Child("items").OnceAsync<GenericItem>());

                foreach (FirebaseObject<GenericItem> item in items.Result.Result)
                {
                    Debug.WriteLine(item.Object.description);
                    try
                    {
                        Home.pulledItemKeys.Add(item.Key);

                        if (backingFiles.Contains(item.Object.imageSource))
                        {
                            this.addItem(item.Object, item.Key);
                        }
                        else
                        {
                            item.Object.itemKey = item.Key;
                            this.imageQueue.Enqueue(item.Object);
                        }
                    }
                    catch (FirebaseException e)
                    {
                        Debug.WriteLine("Exception occurred getting image from firebase storage: " + e.Message);
                    }
                }

                Home.loading = false;
            } else
            {
                //Load from backingArray
                Debug.WriteLine("Loading begins now");
                foreach (GenericItem item in Home.backingArray)
                {
                    if (item.imageBytes == null)
                    {
                        this.addItemAfterLoading(item);
                    } else
                    {
                        this.addItemAfterLoading(item, true);
                    }
                }
            }

            //Handle subscriptions
            AppClient.firebaseClient.Child("items").AsObservable<GenericItem>()
                .Subscribe(newItem => this.handleSubscriptions(newItem));

            //Begin queue removal
            Task.Run(async () => await this.removeQueue());
        }

        //Remove items form the queue in a background thread
        private async Task removeQueue()
        {
            while (this.imageQueue.Count != 0)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    GenericItem item = this.imageQueue.Dequeue();
                    this.addItem(item, item.itemKey, true);
                });

                await Task.Delay(5000);
            }
        }

        //Handles subscriptions to items added to database
        private async void handleSubscriptions(Firebase.Database.Streaming.FirebaseEvent<GenericItem> newItem)
        {
            if (!Home.pulledItemKeys.Contains(newItem.Key))
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    this.firebaseLock = true;
                    Debug.WriteLine("Getting it...");
                    Home.pulledItemKeys.Add(newItem.Key);
                    this.addItem(newItem.Object, newItem.Key, true);
                });

                await Task.Delay(10000);

                this.firebaseLock = false;
                this.secondLock = false;
            }
        }
        
        //After home page has loaded once
        private void addItemAfterLoading(GenericItem current, bool fromBytes = false)
        {
            ImageButton newButton = new ImageButton
            {
                Source = current.imageSource,
                BackgroundColor = Xamarin.Forms.Color.White,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand,
                VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand
            };

            if (!fromBytes)
            {
                newButton.Source = ImageSource.FromFile(current.imageSource);
            } else
            {
                MemoryStream ms = new MemoryStream(current.imageBytes);
                newButton.Source = ImageSource.FromStream(() => ms);
            }

            newButton.Clicked += genericItemClicked;

            Grid.SetRow(newButton, this.currRow);
            Grid.SetColumn(newButton, this.currColumn);

            this.cleanupGrid(newButton, current);
        }

        //Adds item to the Grid
        private void addItem(GenericItem current, String itemKey, bool firebaseObject = false)
        {
            //Add item to backingArray
            //if (Home.pulledItemKeys.Contains(itemKey))
            //{
            //    Debug.WriteLine("New key");
            //    return;
            //}

            if (this.secondLock)
            {
                Debug.WriteLine("Second lock active");
                return;
            }

            if (this.firebaseLock)
            {
                this.secondLock = true;
            }

            current.itemKey = itemKey;
            if (Home.loading)
            {
                //Loading items initially
                Home.backingArray.Add(current);
            }

            ImageButton newButton = new ImageButton
            {
                Source = current.imageSource,
                BackgroundColor = Xamarin.Forms.Color.White,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand,
                VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand
            };

            if (!firebaseObject)
            {
                ImageSource newSource = ImageSource.FromFile(current.imageSource);
                if (!newSource.IsEmpty)
                {
                    newButton.Source = ImageSource.FromFile(current.imageSource);
                }
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

                    if (!Home.loading)
                    {
                        Home.backingArray.Add(current);
                        //Home.pullFromBytes.Add(current);
                    }

                    //Use random address
                } catch (Exception e)
                {
                    Debug.WriteLine("Error occurred getting image from Firebase Storage " + e.Message);
                }
            }

            newButton.Clicked += genericItemClicked;

            Grid.SetRow(newButton, this.currRow);
            Grid.SetColumn(newButton, this.currColumn);

            this.cleanupGrid(newButton, current);
        }

        //Removes extra rows and columns
        private void cleanupGrid(ImageButton newButton, GenericItem current)
        {
            if (this.currColumn == this.generalNumberOfColumns - 1)
            {
                var rowDefs = this.home_grid.RowDefinitions;
                if (this.actualRows >= rowDefs.Count)
                {
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

            Debug.WriteLine("Should be readding items now " + Home.backingArray.Count);

            for (int i = 0; i < Home.backingArray.Count; i++)
            {
                Debug.WriteLine("Readding item");
                GenericItem current = Home.backingArray[i];
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
