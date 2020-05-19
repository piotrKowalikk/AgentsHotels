using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HotelsLogic
{
    public class BookingSearchService
    {
        static string DirectoryName = "BookingSearch";
        public void StartSearch(UserPreference userPreference)
        {
            Directory.CreateDirectory("./" + DirectoryName);
            File.WriteAllText("./" + DirectoryName + "bookingSearch.txt", PrepareUserPreferenceForBookingBot(userPreference));
        }
        private string PrepareUserPreferenceForBookingBot(UserPreference userPreference)
        {
            return new BookingUserPreference(userPreference).GetDeserializedUserPreference();
        }
    }
}
