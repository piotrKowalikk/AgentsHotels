using HotelsLogic.Results;
using System.Collections.Generic;

namespace HotelsLogic
{
    public class SearchResult
    {
        public string FromCompany { get;  set; }
        public Filters Filters { get; set; }
        public List<SearchedHotel> HotelsList { get; set; }
    }
}
