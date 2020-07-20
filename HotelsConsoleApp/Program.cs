using HotelsLogic;
using HotelsLogic.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Text.Json;

namespace HotelsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserPreference userPreference = new UserPreference() { City = "Warsaw", DateTo = DateTime.Now, DateFrom = DateTime.Now, NumberOfAdults = 3 };
            SearchService searchService = SearchService.SearchServiceInstance;
            SearchResult s = new SearchResult()
            {
                Filters = new HotelsLogic.Results.Filters()
                {
                    AirConditioning = SearchedFilterResult.NotSearched,
                    Bar = SearchedFilterResult.NotSearched,
                    FreeCancelation = SearchedFilterResult.NotSearched,
                    Fridge = SearchedFilterResult.NotSearched,
                    Gym = SearchedFilterResult.NotSearched,
                    Massage = SearchedFilterResult.NotSearched,
                    Microwave = SearchedFilterResult.NotSearched,
                    Pool = SearchedFilterResult.NotSearched,
                    Safe = SearchedFilterResult.NotSearched,
                    Sauna = SearchedFilterResult.NotSearched,
                    Spa = SearchedFilterResult.NotSearched,
                    Stars = SearchedFilterResult.NotSearched,
                    Tv = SearchedFilterResult.NotSearched,
                    Wifi = SearchedFilterResult.NotSearched,
                },
                FromCompany = "book ing",
                HotelsList = new System.Collections.Generic.List<HotelsLogic.Results.SearchedHotel>()
                {
                    new SearchedHotel(){HotelName="a",Url="b" },
                    new SearchedHotel(){HotelName="c",Url="c" },
                }

            };
            File.WriteAllText("test.txt", JsonConvert.SerializeObject(s, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
            searchService.Search(userPreference);
        }
    }
}
