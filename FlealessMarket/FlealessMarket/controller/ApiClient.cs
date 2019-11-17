using System;
using Firebase.Database;
using Firebase.Storage;
using System.Threading.Tasks;

namespace FlealessMarket.controller
{
    public class AppClient
    {
        //Firebase client
        public static FirebaseClient firebaseClient;
        public static String url = "https://flealess-market.firebaseio.com/";
        public static FirebaseStorage firebaseStorage;

        private String auth = "VbwAboESljcPR5t2O5VXCpsDyyHvMPqoPAUh3Dte";
        private String imageUrl = "flealess-market.appspot.com";

        public AppClient()
        {
            //Initialize firebase client
            AppClient.firebaseClient = new FirebaseClient(AppClient.url, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(this.auth)
            });

            //Initialize firebase storage
            AppClient.firebaseStorage = new FirebaseStorage(this.imageUrl);
        }
    }
}
