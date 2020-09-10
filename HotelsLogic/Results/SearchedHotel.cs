using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace HotelsLogic.Results
{
    public class SearchedHotel
    {
        public string HotelName { get;  set; }
        public string Url { get;  set; }
        public int? Score { get; set; }
        public static bool operator == (SearchedHotel h1, SearchedHotel h2)
        {
            // if similarity>50% then true
            string[] first = h1.HotelName.Split(' ');
            string[] second = h2.HotelName.Split(' ');
            double similarities = 0;
            double total = second.Length;

            foreach(var wordFromSecond in second)
            {
                string lowerWordFromSecond = wordFromSecond.ToLower();
                foreach(var wordFromFirst in first)
                {
                    if(wordFromFirst.ToLower() == lowerWordFromSecond)
                    {
                        similarities++;
                        break;
                    }
                }
            }

            if ((similarities / total) > 0.5)
                return true;

            return false;
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
