using System;
using System.Collections.Generic;
using FlealessMarket.Items;

using Xamarin.Forms;

namespace FlealessMarket
{
    public partial class Home : ContentPage
    {

        private Grid homeGrid;
        private Picker homePicker;

        public Home()
        {
            InitializeComponent();

            this.homeGrid = this.FindByName("home_grid") as Grid;
            this.homePicker = this.FindByName("picker") as Picker;
            this.homePicker.SelectedIndexChanged += selectGrid;

            this.Title = "Home";
        }

        private void couchButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Couch(sender as ImageButton));
        }

        private void bedButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Bed(sender as ImageButton));
        }

        private void chairButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Chair(sender as ImageButton));
        }

        private void closetButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Closet(sender as ImageButton));
        }

        private void computerChairButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ComputerChair(sender as ImageButton));
        }

        private void singularCouchButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SingularCouch(sender as ImageButton));
        }

        private void sofaButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Sofa(sender as ImageButton));
        }

        private void strollyButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Strolly(sender as ImageButton));
        }

        private void tableButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Table(sender as ImageButton));
        }

        private void tableSetButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TableSet(sender as ImageButton));
        }

        private void yardChairButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new YardChair(sender as ImageButton));
        }

        private void yardSetButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new YardSet(sender as ImageButton));
        }


        //Different grid construction
        private void setChairsGrid()
        {
            for (int j = this.homeGrid.Children.Count - 1; j >= 0; --j)
            {
                this.homeGrid.Children.RemoveAt(j);
            }

            var chair = new Image {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "chair"
            };
            //chair.Clicked += chairButtonClicked;

            var computerChair = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "computer_chair"
            };
            //computerChair.Clicked += computerChairButtonClicked;

            var yardChair = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "yard_chair"
            };
            //yardChair.Clicked += yardChairButtonClicked;

            this.homeGrid.Children.Add(chair, 0, 0);
            this.homeGrid.Children.Add(computerChair, 1, 0);
            this.homeGrid.Children.Add(yardChair, 2, 0);
        }

        private void setCouchesGrid()
        {
            for (int j = this.homeGrid.Children.Count - 1; j >= 0; --j)
            {
                this.homeGrid.Children.RemoveAt(j);
            }

            var couch = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "couch"
            };
            //couch.Clicked += couchButtonClicked;

            var singularCouch = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "singular_couch"
            };
            //singularCouch.Clicked += singularCouchButtonClicked;

            var sofa = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "sofa"
            };
            //sofa.Clicked += sofaButtonClicked;

            this.homeGrid.Children.Add(couch, 0, 0);
            this.homeGrid.Children.Add(singularCouch, 1, 0);
            this.homeGrid.Children.Add(sofa, 2, 0);
        }

        private void setTablesGrid()
        {
            for (int j = this.homeGrid.Children.Count - 1; j >= 0; --j)
            {
                this.homeGrid.Children.RemoveAt(j);
            }

            var table = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "table"
            };
            //table.Clicked += tableButtonClicked;

            var tableSet = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "table_set"
            };
            //tableSet.Clicked += tableSetButtonClicked;

            var yardSet = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "yard_set",
                Aspect = Aspect.AspectFit
            };
            //yardSet.Clicked += yardSetButtonClicked;

            this.homeGrid.Children.Add(table, 0, 0);
            this.homeGrid.Children.Add(tableSet, 1, 0);
            this.homeGrid.Children.Add(yardSet, 2, 0);
        }

        private void setBedsGrid()
        {
            for (int j = this.homeGrid.Children.Count - 1; j >= 0; --j)
            {
                this.homeGrid.Children.RemoveAt(j);
            }

            var bed = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "bed",
                HorizontalOptions = Xamarin.Forms.LayoutOptions.CenterAndExpand
            };
            //bed.Clicked += bedButtonClicked;
            this.homeGrid.Children.Add(bed, 0, 0);
        }

        private void setStrollersGrid()
        {
            for (int j = this.homeGrid.Children.Count - 1; j >= 0; --j)
            {
                this.homeGrid.Children.RemoveAt(j);
            }

            var strolly = new Image
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Source = "strolly"
            };
            //strolly.Clicked += strollyButtonClicked;

            this.homeGrid.Children.Add(strolly, 0, 0);
        }

        //Select grid
        private void selectGrid(object sender, EventArgs e)
        {
            Picker pick = sender as Picker;
            int pickIndex = pick.SelectedIndex;
            switch (pickIndex)
            {
                case 0:
                    setGeneralGrid();
                    break;
                case 1:
                    setChairsGrid();
                    break;
                case 2:
                    setCouchesGrid();
                    break;
                case 3:
                    setTablesGrid();
                    break;
                case 4:
                    setBedsGrid();
                    break;
                case 5:
                    setStrollersGrid();
                    break;
                default:
                    break;
            }
        }

        private void setGeneralGrid()
        {
            Application.Current.MainPage = new NavigationPage(new Home());
        }
    }
}
