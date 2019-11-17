using System.IO;
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

        //Stream to bytearray
        public static byte[] streamToByteArray(Stream photoStream)
        {
            MemoryStream ms = new MemoryStream();

            photoStream.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
