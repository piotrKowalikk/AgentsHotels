﻿using System.IO;

namespace HotelsLogic
{
    public class BookingSearchService : ISearchService
    {
        readonly public static string DirectoryName = "BookingSearch";
        public void Search(UserPreference userPreference)
        {
            Directory.CreateDirectory("./" + DirectoryName);
            File.WriteAllText("./" + DirectoryName + "/bookingSearch.txt", PrepareUserPreferenceForBookingBot(userPreference));
        }
        private string PrepareUserPreferenceForBookingBot(UserPreference userPreference)
        {
            return new BookingUserPreference(userPreference).GetSerializedUserPreference();
        }

        public void CleanServiceFolder()
        {
            try
            {
                string[] fileNames = Directory.GetFiles("./" + DirectoryName);

                foreach (string file in fileNames)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
    }
}
