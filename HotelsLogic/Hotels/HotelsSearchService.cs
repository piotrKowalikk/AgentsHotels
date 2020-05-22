using System.IO;

namespace HotelsLogic
{
    public class HotelsSearchService : ISearchService
    {
        static string DirectoryName = "HotelsSearch";

        public void Search(UserPreference userPreference)
        {
            Directory.CreateDirectory("./" + DirectoryName);
            File.WriteAllText("./" + DirectoryName + "/hotelsSearch.txt", PrepareUserPreferenceForHotelsBot(userPreference));
        }

        private string PrepareUserPreferenceForHotelsBot(UserPreference userPreference)
        {
            return new HotelsUserPreference(userPreference).GetDeserializedUserPreference();
        }
    }
}
