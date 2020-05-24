using System.Text.Json;

namespace HotelsLogic
{
    class HotelsUserPreference
    {
        public string dateFrom { get; private set; }
        public string dateTo { get; private set; }
        public int numberOfAdults { get; private set; }
        public int numberOfChildren { get; private set; }
        public int numberOfRooms { get; private set; }
        public int fromPrice { get; private set; }
        public int toPrice { get; private set; }
        public string city { get; private set; }
        public string outputPath { get; private set; }

        public HotelsUserPreference(UserPreference userPreference)
        {
            dateFrom = userPreference.DateFrom.Day + "/" + userPreference.DateFrom.Month + "/" + userPreference.DateFrom.Year;
            dateTo = userPreference.DateTo.Day + "/" + userPreference.DateTo.Month + "/" + userPreference.DateTo.Year;
            numberOfAdults = userPreference.NumberOfAdults;
            numberOfChildren = 2;
            numberOfRooms = 2;
            fromPrice = 300;
            toPrice = 500;
            city = userPreference.City;
            outputPath = ResultService.ResultsPath;
        }

        public string GetDeserializedUserPreference()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
