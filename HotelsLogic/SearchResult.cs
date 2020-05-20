namespace HotelsLogic
{
    public class SearchResult
    {
        public string FromCompany { get; private set; }
        public string HotelName { get; private set; }
        public string Url { get; private set; }
        public SearchResult(string fromCompany, string url, string hotelName)
        {
            FromCompany = fromCompany;
            HotelName = hotelName;
            Url = url;
        }
    }
}
