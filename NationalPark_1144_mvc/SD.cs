namespace NationalPark_1144_mvc
{
    public class SD
    {
        public static string APIBaseUrl
        {
            get
            {
                return "https://localhost:7122/";
            }
        }
        public static string NationalParkAPIPath
        {
            get
            {
                return APIBaseUrl + "api/NationalPark";
            }
        }
        public static string TrailsAPIPath
        {
            get
            {
                return APIBaseUrl + "api/trail";
            }
        }
    }
}
