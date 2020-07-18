using Newtonsoft.Json;
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
            numberOfChildren = 2;//TODO
            numberOfRooms = 2;//TODO
            fromPrice = 300;//TODO
            toPrice = 500;//TODO
            city = userPreference.City;
            //helpers
            dateFromMonthsToSkip = userPreference.DateFrom.Month - DateTime.Now.Month;
            dateToMonthsToSkip = userPreference.DateTo.Month - DateTime.Now.Month;
            outputPath = ResultService.ResultsPath;
            numberOfReturnOffers = 3;//TODO
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

        private static string MakeTwoPostions(int num)
        {
            if (num < 10)
                return "0" + num;
            return "" + num;
        }

        public string GetDeserializedUserPreference()
        {
            return  JsonConvert.SerializeObject(this);
        }
    }
}
