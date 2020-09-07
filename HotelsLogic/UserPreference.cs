using System;

namespace HotelsLogic
{
    public class UserPreference
    {
        public string City { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfRooms { get; set; }
        public int Delay { get; set; }
        public int NumberOfReturnOffers { get; set; } = 1;
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
        public int Stars { get; set; }

        public UserPreference WithCity(string city)
        {
            City = city;
            return this;
        }

        public UserPreference WithDateFrom(DateTime date)
        {
            DateFrom = date;
            return this;
        }

        public UserPreference WithDateTo(DateTime date)
        {
            DateTo = date;
            return this;
        }

        public UserPreference WithNumberOfAdults(int adults)
        {
            NumberOfAdults = adults;
            return this;
        }

        public UserPreference WithNumberOfChildren(int adults)
        {
            NumberOfChildren = adults;
            return this;
        }

        public UserPreference WithNumberOfRooms(int rooms)
        {
            NumberOfRooms = rooms;
            return this;
        }

        public UserPreference WithDelay(int delay)
        {
            Delay = delay;
            return this;
        }

        public UserPreference WithNumberOfReturnOffers(int number)
        {
            NumberOfReturnOffers = number;
            return this;
        }

        public UserPreference WithAirConditioning(bool? choice)
        {
            AirConditioning = choice.Value;
            return this;
        }

        public UserPreference WithFreeCancelation(bool? choice)
        {
            FreeCancelation = choice.Value;
            return this;
        }

        public UserPreference WithWifi(bool? choice)
        {
            Wifi = choice.Value;
            return this;
        }

        public UserPreference WithBar(bool? choice)
        {
            Bar = choice.Value;
            return this;
        }

        public UserPreference WithPool(bool? choice)
        {
            Pool = choice.Value;
            return this;
        }

        public UserPreference WithFridge(bool? choice)
        {
            Fridge = choice.Value;
            return this;
        }

        public UserPreference WithMicrowave(bool? choice)
        {
            Microwave = choice.Value;
            return this;
        }

        public UserPreference WithSafe(bool? choice)
        {
            Safe = choice.Value;
            return this;
        }

        public UserPreference WithTv(bool? choice)
        {
            Tv = choice.Value;
            return this;
        }

        public UserPreference WithMassage(bool? choice)
        {
            Massage = choice.Value;
            return this;
        }

        public UserPreference WithSauna(bool? choice)
        {
            Sauna = choice.Value;
            return this;
        }

        public UserPreference WithGym(bool? choice)
        {
            Gym = choice.Value;
            return this;
        }

        public UserPreference WithSpa(bool? choice)
        {
            Spa = choice.Value;
            return this;
        }

        public UserPreference WithStars(StarsChoice choice)
        {
            Stars = (int)choice;
            return this;
        }
    }
}
