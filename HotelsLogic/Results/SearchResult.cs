using HotelsLogic.Results;
using System.Collections;
using System.Collections.Generic;

namespace HotelsLogic
{
    public class SearchResult
    {
        public string FromCompany { get; set; }
        public Filters Filters { get; set; }
        public List<SearchedHotel> HotelsList { get; set; }

        public void CalculateHotelsScore()
        {
            int score = 0;
            int NotFoundValue = 1, NotSearchedValue = 3, PossibleValue = 4, SatisfiedValue = 6;

            List<SearchedFilterResult> filtersResults = Filters.ToList();
            foreach (var filter in filtersResults)
            {
                switch (filter)
                {
                    case SearchedFilterResult.NotFound:
                        score += NotFoundValue;
                        break;
                    case SearchedFilterResult.NotSearched:
                        score += NotSearchedValue;
                        break;
                    case SearchedFilterResult.Possible:
                        score += PossibleValue;
                        break;
                    case SearchedFilterResult.Satisfied:
                        score += SatisfiedValue;
                        break;
                }
            }

            for (int i = 0; i < HotelsList.Count; i++)
            {
                HotelsList[i].Score = score;
            }
        }

        public void FormatHotelNames()
        {
            if (FromCompany == "booking")
            {
                foreach (var hotel in HotelsList)
                {
                    hotel.HotelName = hotel.HotelName.Remove(0, 2);
                    hotel.HotelName = hotel.HotelName.Remove(hotel.HotelName.Length - 2, 2);
                }
            }
            else
            {
                foreach (var hotel in HotelsList)
                {
                    hotel.HotelName = "\r\n" + hotel.HotelName + "\r\n";
                }
            }
        }
    }
}
