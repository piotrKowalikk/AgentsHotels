using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HotelsLogic
{
    class BookingUserPreference
    {
        public string dateFrom { get; private set; }
        public int dateFromMonthsToSkip { get; private set; }
        public string dateTo { get; private set; }
        public int dateToMonthsToSkip { get; private set; }
        public int numberOfAdults { get; private set; }
        public int numberOfChildren { get; private set; }
        public int numberOfRooms { get; private set; }
        public int fromPrice { get; private set; }
        public int toPrice { get; private set; }
        public string city { get; private set; }
        public int numberOfReturnOffers { get; set; }
        public int delay { get; set; }
        //filtry
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
        public bool stars { get; private set; }

        public string outputPath { get; private set; }

        public BookingUserPreference(UserPreference userPreference)
        {
            dateFrom = userPreference.DateFrom.Year + "-" + MakeTwoPostions(userPreference.DateFrom.Month) + "-" + MakeTwoPostions(userPreference.DateFrom.Day);
            dateTo = userPreference.DateTo.Year + "-" + MakeTwoPostions(userPreference.DateTo.Month) + "-" + MakeTwoPostions(userPreference.DateTo.Day);
            numberOfAdults = userPreference.NumberOfAdults;
            numberOfChildren = 2;
            numberOfRooms = 2;//TODO
            fromPrice = 300;
            toPrice = 500;
            city = userPreference.City;
            //helpers
            dateFromMonthsToSkip = userPreference.DateFrom.Month - DateTime.Now.Month;
            dateToMonthsToSkip = userPreference.DateTo.Month - DateTime.Now.Month;
            outputPath = ResultService.ResultsPath;
            //filters
            airConditioning = false;
            freeCancelation = false;
            wifi = false;
            bar = false;
            pool = false;
            fridge = false;
            microwave = false;
            safe = false;
            tv = false;
            massage = false;
            sauna = false;
            gym = false;
            spa = false;
            stars = false;

        }
        private static string MakeTwoPostions(int num)
        {
            if (num < 10)
                return "0" + num;
            return "" + num;
        }

        public string GetDeserializedUserPreference()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
