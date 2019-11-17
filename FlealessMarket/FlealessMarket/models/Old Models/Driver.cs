using System;
namespace FlealessMarket
{
    public class Driver
    {
        public String first { get; set; }
        public String last { get; set; }
        public String email { get; set; }
        public Int64 phoneNumber { get; set; }
        public String address1 { get; set; }
        public String address2 { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public int zip { get; set; }
        public int suite { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public Object profile { get; set; }

        //Driver specific attributes
        public Object registration { get; set; }
        public Object license { get; set; }
        public String vColor { get; set; }
        public String vMake { get; set; }
        public int vYear { get; set; }
        public String vModel { get; set; }
    }
}
