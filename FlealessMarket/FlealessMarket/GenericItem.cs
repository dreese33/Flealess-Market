using System;
using Xamarin.Forms;

namespace FlealessMarket
{
    public class GenericItem
    {
		public String imageSource;
		public String description;
		public String title;
        public int[] categories;

        public GenericItem(String imageSource, String title, String description, double price, int[] categories)
        {
			this.imageSource = imageSource;
			this.description = description;
			this.title = title;

            if (Array.IndexOf(categories, 0) < 0) {
                Array.Resize(ref categories, categories.Length + 1);
                categories[categories.Length - 1] = 0;
            }

            this.categories = categories;
        }
    }
}
