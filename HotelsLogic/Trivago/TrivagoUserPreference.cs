using System.Text.Json;

namespace HotelsLogic
{
    class TrivagoUserPreference
    {
        public string dateFrom { get; private set; }
        public string dateTo { get; private set; }
        public int numberOfAdults { get; private set; }
        public string city { get; private set; }
        public string outputPath { get; private set; }
        public int delay { get; private set; }

        public TrivagoUserPreference(UserPreference userPreference)
        {
            dateFrom = $"{userPreference.DateFrom.Month}/{userPreference.DateFrom.Day}/{userPreference.DateFrom.Year}";
            dateTo = $"{userPreference.DateTo.Month}/{userPreference.DateTo.Day}/{userPreference.DateTo.Year}";
            numberOfAdults = userPreference.NumberOfAdults;
            city = userPreference.City;
            outputPath = ResultService.ResultsPath;
            delay = userPreference.Delay;
        }

        public string GetSerializedUserPreference()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
