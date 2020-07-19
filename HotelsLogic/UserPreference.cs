using System;

namespace HotelsLogic
{
    public class UserPreference
    {
        public string City { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int NumberOfAdults { get; set; }
        public int Delay { get; set; }
        public int NumberOfReturnOffers { get; set; }
        //filtry
        public bool AirConditioning { get; set; }
        public bool FreeCancelation { get; set; }
        public bool Wifi { get; set; }
        public bool Bar { get; set; }
        public bool Pool { get; set; }
        public bool Fridge { get; set; }
        public bool Microwave { get; set; }
        public bool Safe { get; set; }
        public bool Tv { get; set; }
        public bool Massage { get; set; }
        public bool Sauna { get; set; }
        public bool Gym { get; set; }
        public bool Spa { get; set; }
        public bool Stars { get; set; }
    }
}
