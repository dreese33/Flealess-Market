using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;

namespace FlealessMarket
{
    public partial class Home : ContentPage
    {
        private Grid homeGrid;
        private Picker homePicker;
        private SearchBar searchBar;
        private RelativeLayout relativeLayout;
        private ScrollView scrollView;

        private int currColumn = 0;
        private int currRow = 0;
        private readonly int generalNumberOfColumns = 3;

        //Actual number of rows present
        private int actualRows = 1;

        //Row definition for newly added rows
        private RowDefinition rowDefinition;

        private bool loading = true;

        private List<String> pulledItemKeys = new List<String>();
        
        /*
        private GenericItem[] backingArray = { new GenericItem("bed", "New bed!", "Beautiful bed, great condition. Barely used, needs picked up in the next 5 days! Please let me know immediately if this product is something you want!", 124.99, new int[]{0, 4}),
            new GenericItem("chair", "Chair", "Beautiful chair, great condition.", 124.99, new int[]{0, 1}),
            new GenericItem("closet", "Closet", "Beautiful closet, great condition.", 124.99, new int[]{0}),
            new GenericItem("computer_chair", "Computer Chair", "Beautiful computer chair, great condition.", 124.99, new int[]{0, 1}),
            new GenericItem("couch", "Beautiful Couch!", "Beautiful couch, great condition.", 124.99, new int[]{0, 2}),
            new GenericItem("singular_couch", "Couch", "Beautiful couch, great condition.", 124.99, new int[]{0, 2}),
            new GenericItem("sofa", "Sofa", "Beautiful sofa, great condition.", 124.99, new int[]{0, 2}),
            new GenericItem("strolly", "Stroller", "Beautiful stroller, great condition.", 124.99, new int[]{0, 5}),
            new GenericItem("table", "Table", "Beautiful table, great condition.", 124.99, new int[]{0, 3}),
            new GenericItem("table_set", "Table Set", "Beautiful table set, great condition.", 124.99, new int[]{0, 3}),
            new GenericItem("yard_chair", "Yard Chair", "Beautiful yard chair, great condition.", 124.99, new int[]{0, 1}),
            new GenericItem("yard_set", "Yard Set", "Beautiful yard set, great condition.", 124.99, new int[]{0, 1, 3})};*/
        private List<GenericItem> backingArray = new List<GenericItem>();

        //Create an arraylist for the currently backing array, in addition to the overall backing array pulled from the database
        //This arraylist will represent the current children contained allowing us to get information about them
        private List<GenericItem> currentArray = new List<GenericItem>();

        public static void formatButton(Button button, int widthRequest = 100)
        {
            button.BorderColor = Xamarin.Forms.Color.White;
            button.BorderWidth = 1;
            button.WidthRequest = widthRequest;
            button.BackgroundColor = Xamarin.Forms.Color.LightBlue;
        }
        
        public Home()
        {
            InitializeComponent();

            this.homeGrid = this.FindByName("home_grid") as Grid;

            this.homePicker = this.FindByName("picker") as Picker;
            this.relativeLayout = this.FindByName("relative") as RelativeLayout;

            this.scrollView = this.FindByName("scroll") as ScrollView;

            //Change grid based on category changed
            this.homePicker.SelectedIndexChanged += selectGrid;

            //Square frame for grid elements
            var mainDisplay = DeviceDisplay.MainDisplayInfo;
            double dimension = ((mainDisplay.Width / mainDisplay.Density) / 3) - 20; //Math.Abs((Application.Current.MainPage.Width / 3) - 20);

            ColumnDefinition columnDefinition = new ColumnDefinition();
            Debug.WriteLine("What is wrong with this: " + dimension);
            columnDefinition.Width = dimension;
            for (int i = 0; i < 3; i++)
            {
                this.homeGrid.ColumnDefinitions.Add(columnDefinition);
            }

            rowDefinition = new RowDefinition();
            rowDefinition.Height = dimension;
            this.homeGrid.RowDefinitions.Add(rowDefinition);

            /*
            for (int i = 0; i < this.backingArray.Count; i++)
            {
                this.addItem(backingArray[i]);
            }*/

            //Searching logic
            this.searchBar = this.FindByName("search_bar") as SearchBar;

            this.Title = "Home";

            //Pull items from database
            var items = Task.Run(async () => FirebaseApi.firebaseClient.Child("items").OnceAsync<GenericItem>());
            Debug.WriteLine("Number: " + items.Result.Result.Count);

            loading = true;

            foreach (FirebaseObject<GenericItem> item in items.Result.Result)
            {
                Debug.WriteLine(item.Object.description);
                try
                {
                    Debug.WriteLine("Attempting to add new object");
                    this.pulledItemKeys.Add(item.Key);
                    this.addItem(item.Object, item.Key);
                }
                catch (FirebaseException e)
                {
                    Debug.WriteLine("Exception occurred getting image from firebase storage: " + e.Message);
                }
            }

            loading = false;

            //this.currentArray.AddRange(backingArray);

            //Handle subscriptions
            FirebaseApi.firebaseClient.Child("items").AsObservable<GenericItem>().Subscribe(newItem => this.handleSubscriptions(newItem));
        }

        //Handles subscriptions to items added to database
        private async void handleSubscriptions(Firebase.Database.Streaming.FirebaseEvent<GenericItem> newItem)
        {
            if (!this.pulledItemKeys.Contains(newItem.Key))
            {
                Debug.WriteLine("Pulling began");
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
                    var selectedImage = current.imageSource as FileImageSource;

                    //String url = Task.Run(async () => await FirebaseApi.firebaseStorage.Child("images").Child(current.imageSource).GetDownloadUrlAsync()).Result;
                    String url = Task.Run(async () => await FirebaseApi.firebaseStorage.Child("images").Child(selectedImage.File).GetDownloadUrlAsync()).Result;
                    Debug.WriteLine(url);

                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                    request.AllowReadStreamBuffering = true;

                    if (firebaseObject)
                    {
                        request.Timeout = 50000;
                        Debug.WriteLine("Timeout occurred");
                    }
                    else
                    {
                        request.Timeout = 0;
                        Debug.WriteLine("Timeout did not occur");
                    }

                    System.Net.WebResponse response = request.GetResponse();

                    System.IO.Stream stream = response.GetResponseStream();

                    //ImageSource newSource = ImageSource.FromStream(() => stream);
                    //newButton.Source = newSource;

                    //Generate random address
                    /*
                    String randomAddr = "";
                    int i = 0;
                    Random random = new Random();
                    while (i < 10)
                    {
                        randomAddr += random.Next();
                        i++;
                    }
                    current.path = randomAddr;*/

                    //byte[] b = null;

                    /*
                    int count = 0;
                    do
                    {
                        byte[] buf = new byte[1024];
                        count = stream.Read(buf, 0, 1024);
                        ms.Write(buf, 0, count);
                    } while (stream.CanRead && count > 0);*/

                    //b = ms.ToArray();
                    //current.imageBytes = b;

                    
                    MemoryStream ms = new MemoryStream();

                    stream.CopyTo(ms);
                    byte[] bytes = ms.ToArray();
                    current.imageBytes = bytes;

                    newButton.Source = ImageSource.FromStream(() => new MemoryStream(current.imageBytes));
                    /*
                    Debug.WriteLine("Began writing");
                    FileStream file = File.Create(randomAddr);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(file);
                    file.Close();
                    Debug.WriteLine("Completed writing to file");*/

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
                var rowDefs = this.homeGrid.RowDefinitions;
                if (this.actualRows >= rowDefs.Count) {
                    this.homeGrid.RowDefinitions.Add(this.rowDefinition);
                }

                this.homeGrid.Children.Add(newButton);

                Debug.WriteLine("Adding to current");

                this.currentArray.Add(current);

                this.currColumn = 0;
                this.currRow += 1;
                this.actualRows += 1;
            }
            else
            {
                this.homeGrid.Children.Add(newButton);
                this.currentArray.Add(current);
                this.currColumn += 1;
            }
        }

        /*
        private async Task writeToRandomFile(String randomAddr)
        {
            Debug.WriteLine("Writing to file");
            FileStream file = File.Create(randomAddr);
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(file);
            file.Close();
            Debug.WriteLine("Completed writing to file");

            current.imageSource = newSource;
        }*/

        //Removes last item from the Grid
        private void popItem()
        {
            if (this.currColumn == 0)
            {
                if (this.currRow != 0)
                {
                    this.currRow -= 1;
                    this.actualRows -= 1;

                    var rowDefs = this.homeGrid.RowDefinitions;

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
            this.homeGrid.Children.RemoveAt(this.homeGrid.Children.Count - 1);
        }

        //Generic image object creation
        private void genericItemClicked(object sender, EventArgs e)
        {
            //0 should be replaced by the item row and item column
            ImageButton sent = sender as ImageButton;
            int index = homeGrid.Children.IndexOf(sent);

            Debug.WriteLine("The index is: " + index);
            Debug.WriteLine("The total is: " + this.currentArray.Count);
            Debug.WriteLine("Item title: " + this.currentArray[index].title);

            Navigation.PushAsync(new GenericItemPage(this.currentArray[index]));
        }

        //Throw item out
        private void Throw_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonateItem());
        }

        private void selectGrid(object sender, EventArgs e)
        {
            for (int i = 0; i < this.homeGrid.Children.Count; i++)
            {
                this.popItem();
            }

            //Remove remaining children
            if (this.homeGrid.Children.Count > 0 || this.currentArray.Count > 0)
            {
                this.homeGrid.Children.Clear();
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
