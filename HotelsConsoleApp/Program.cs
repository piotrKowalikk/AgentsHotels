using HotelsLogic;
using System;

namespace HotelsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserPreference userPreference = new UserPreference() { City = "Warsaw", DateTo = DateTime.Now, DateFrom = DateTime.Now, NumberOfAdults = 3 };
            SearchService searchService = SearchService.SearchServiceInstance;
            searchService.Search(userPreference);
        }
    }
}
