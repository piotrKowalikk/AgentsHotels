using System;
using System.Collections.Generic;
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

        public BookingUserPreference(UserPreference userPreference)
        {
            dateFrom = userPreference.DateFrom.Year + "-" + userPreference.DateFrom.Month + "-" + userPreference.DateFrom.Day;
            dateTo = userPreference.DateTo.Year + "-" + userPreference.DateTo.Month + "-" + userPreference.DateTo.Day;
            numberOfAdults = userPreference.NumberOfAdults;
            numberOfChildren = 2;
            numberOfRooms = 2;//TODO
            fromPrice = 300;
            toPrice = 500;
            city = userPreference.City;
            //helpers
            dateFromMonthsToSkip = userPreference.DateFrom.Month - DateTime.Now.Month;
            dateToMonthsToSkip = userPreference.DateTo.Month - DateTime.Now.Month;
        }

        public string GetDeserializedUserPreference()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
