using System;
namespace FlealessMarket
{
    public class PostItem
    {
        public int[] categories;
        public String description;
        public String imageSource;
        public String title;
        public PostItem(GenericItem item)
        {
            this.categories = item.categories;
            this.description = item.description;
            this.imageSource = item.imageSource;
            this.title = item.title;
        }
    }
}
