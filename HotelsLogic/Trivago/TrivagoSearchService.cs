using System.IO;

namespace HotelsLogic
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
            return new TrivagoUserPreference(userPreference).GetSerializedUserPreference();
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
