using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace FlealessMarket
{
    public class UIController
    {
        //Verify entries have text
        public static Entry verifyAttributes(Entry[] entries)
        {
            foreach (Entry entry in entries)
            {
                if (entry.Text == null)
                {
                    Debug.WriteLine("Entry empty");
                    return entry;
                }
            }

            return null;
        }
    }
}
