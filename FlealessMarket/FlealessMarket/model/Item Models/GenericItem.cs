using System;
using Xamarin.Forms;
using System.IO;

namespace FlealessMarket
{
    public class GenericItem
    {
		public String imageSource;
		public String description;
		public String title;
        public int[] categories;

        public String path = "";
        public byte[] imageBytes = null;
        public String itemKey = "";

        public GenericItem(String imageSource, String title, String description, int[] categories, String path = "", byte[] bytes = null)
        {
			this.imageSource = imageSource;
			this.description = description;
			this.title = title;
            
            if (path != "")
            {
                this.path = path;
            }

            if (bytes != null)
            {
                this.imageBytes = bytes;
            }

            if (Array.IndexOf(categories, 0) < 0) {
                Array.Resize(ref categories, categories.Length + 1);
                categories[categories.Length - 1] = 0;
            }

            this.categories = categories;
        }
    }
}
