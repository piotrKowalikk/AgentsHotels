using System.Text.Json;

namespace HotelsLogic
{
    class TrivagoUserPreference
    {
        public string dateFrom { get; private set; }
        public string dateTo { get; private set; }
        public int numberOfAdults { get; private set; }
        public int numberOfChildren { get; private set; }
        public string city { get; private set; }
        public string outputPath { get; private set; }
        public int delay { get; private set; }
        public int numberOfReturnOffers { get; set; }
        // filters
        public bool airConditioning { get; private set; }
        public bool freeCancelation { get; private set; }
        public bool wifi { get; private set; }
        public bool bar { get; private set; }
        public bool pool { get; private set; }
        public bool fridge { get; private set; }
        public bool microwave { get; private set; }
        public bool safe { get; private set; }
        public bool tv { get; private set; }
        public bool massage { get; private set; }
        public bool sauna { get; private set; }
        public bool gym { get; private set; }
        public bool spa { get; private set; }
        public int stars { get; private set; }

        public TrivagoUserPreference(UserPreference userPreference)
        {
            dateFrom = $"{userPreference.DateFrom.Month}/{userPreference.DateFrom.Day}/{userPreference.DateFrom.Year}";
            dateTo = $"{userPreference.DateTo.Month}/{userPreference.DateTo.Day}/{userPreference.DateTo.Year}";
            numberOfAdults = userPreference.NumberOfAdults;
            numberOfChildren = userPreference.NumberOfChildren;
            city = userPreference.City;
            outputPath = ResultService.ResultsPath;
            delay = userPreference.Delay;
            //filters
            airConditioning = userPreference.AirConditioning;
            freeCancelation = userPreference.FreeCancelation;
            wifi = userPreference.Wifi;
            bar = userPreference.Bar;
            pool = userPreference.Pool;
            fridge = userPreference.Fridge;
            microwave = userPreference.Microwave;
            safe = userPreference.Safe;
            tv = userPreference.Tv;
            massage = userPreference.Massage;
            sauna = userPreference.Sauna;
            gym = userPreference.Gym;
            spa = userPreference.Spa;
            stars = userPreference.Stars;
        }

        public string GetSerializedUserPreference()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
