﻿using System.IO;

namespace HotelsLogic.Trivago
{
    public class TrivagoSearchService : ISearchService
    {
        static string DirectoryName = "TrivagoSearch";
        public void Search(UserPreference userPreference)
        {
            Directory.CreateDirectory("./" + DirectoryName);
            File.WriteAllText("./" + DirectoryName + "/trivagoSearch.txt", PrepareUserPreferenceForTrivagoBot(userPreference));
        }
        private string PrepareUserPreferenceForTrivagoBot(UserPreference userPreference)
        {
            return new BookingUserPreference(userPreference).GetDeserializedUserPreference();
        }
    }
}
