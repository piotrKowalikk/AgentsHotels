using HotelsLogic;
using System;

namespace HotelsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UserPreference userPreference = new UserPreference() { City="Warsaw", DateTo = DateTime.Now, DateFrom = DateTime.Now, NumberOfAdults=3};
            BookingSearchService bookingSearchService = new BookingSearchService();
            bookingSearchService.StartSearch(userPreference);
        }
    }
}
