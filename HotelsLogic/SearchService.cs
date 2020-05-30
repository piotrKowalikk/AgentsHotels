using System;
using System.Collections.Generic;
using System.Text;

namespace HotelsLogic
{
    public class SearchService
    {
        private List<ISearchService> searchServices;
        public static SearchService SearchServiceInstance { get; private set; } = new SearchService();

        private SearchService()
        {
            searchServices = new List<ISearchService>();
            searchServices.Add(new BookingSearchService());
            searchServices.Add(new TrivagoSearchService());
            searchServices.Add(new HotelsSearchService());
        }

        public void Search(UserPreference us)
        {
            foreach (var ss in searchServices)
            {
                ss.Search(us);
            }
        }

        public void CleanServiceFolder()
        {
            foreach (var ss in searchServices)
            {
                ss.CleanServiceFolder();
            }
        }
    }
}
