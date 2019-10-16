using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections;

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

        private GenericItem[] backingArray = { new GenericItem("bed", "New bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 4}),
            new GenericItem("chair", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 1}),
            new GenericItem("closet", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0}),
            new GenericItem("computer_chair", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 1}),
            new GenericItem("couch", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 2}),
            new GenericItem("singular_couch", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 2}),
            new GenericItem("sofa", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 2}),
            new GenericItem("strolly", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 5}),
            new GenericItem("table", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 3}),
            new GenericItem("table_set", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 3}),
            new GenericItem("yard_chair", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 1}),
            new GenericItem("yard_set", "not a bed!", "Beautiful bed, great condition.", 124.99, new int[]{0, 1, 3})};

        //Create an arraylist for the currently backing array, in addition to the overall backing array pulled from the database
        //This arraylist will represent the current children contained allowing us to get information about them
        private List<GenericItem> currentArray = new List<GenericItem>();

        public Home()
        {
            InitializeComponent();

            this.homeGrid = this.FindByName("home_grid") as Grid;

            this.homePicker = this.FindByName("picker") as Picker;
            this.relativeLayout = this.FindByName("relative") as RelativeLayout;

            this.scrollView = this.FindByName("scroll") as ScrollView;

            //Change grid based on category changed
            this.homePicker.SelectedIndexChanged += selectGrid;

            //Initialize currentArray with backingArray since General is always the first category selected
            this.currentArray.AddRange(backingArray);

            //Square frame for grid elements
            double dimension = (Application.Current.MainPage.Width / 3) - 20;

            ColumnDefinition columnDefinition = new ColumnDefinition();
            columnDefinition.Width = dimension;
            for (int i = 0; i < 3; i++)
            {
                this.homeGrid.ColumnDefinitions.Add(columnDefinition);
            }

            rowDefinition = new RowDefinition();
            rowDefinition.Height = dimension;
            this.homeGrid.RowDefinitions.Add(rowDefinition);

            for (int i = 0; i < this.backingArray.Length; i++)
            {
                this.addItem(backingArray[i]);
            }

            //Searching logic
            this.searchBar = this.FindByName("search_bar") as SearchBar;

            this.Title = "Home";
        }

        //Adds item to the Grid
        private void addItem(GenericItem current)
        {
            ImageButton newButton = new ImageButton
            {
                Source = current.imageSource,
                BackgroundColor = Xamarin.Forms.Color.White,
                Aspect = Aspect.Fill,
                HorizontalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand,
                VerticalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand
            };
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
            for (int i = 0; i < backingArray.Length; i++)
            {
                GenericItem current = backingArray[i];
                if (Array.IndexOf(current.categories, pickIndex) > -1)
                {
                    this.addItem(current);
                }
            }
        }
    }
}
