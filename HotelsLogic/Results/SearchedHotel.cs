using System;
using System.Collections.Generic;
using System.Text;

namespace HotelsLogic.Results
{
    public class SearchedHotel
    {
        public string HotelName { get;  set; }
        public string Url { get;  set; }
        public static bool operator ==(SearchedHotel h1, SearchedHotel h2)
        {
            return h1.HotelName == h2.HotelName;
        }
        public static bool operator !=(SearchedHotel h1, SearchedHotel h2)
        {
            return !(h1.HotelName == h2.HotelName);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
