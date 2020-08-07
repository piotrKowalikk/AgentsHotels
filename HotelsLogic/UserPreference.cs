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

        public UserPreference WithAirConditioning(FiltersChoice choice)
        {
            AirConditioning = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithFreeCancelation(FiltersChoice choice)
        {
            FreeCancelation = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithWifi(FiltersChoice choice)
        {
            Wifi = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithBar(FiltersChoice choice)
        {
            Bar = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithPool(FiltersChoice choice)
        {
            Pool = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithFridge(FiltersChoice choice)
        {
            Fridge = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithMicrowave(FiltersChoice choice)
        {
            Microwave = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithSafe(FiltersChoice choice)
        {
            Safe = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithTv(FiltersChoice choice)
        {
            Tv = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithMassage(FiltersChoice choice)
        {
            Massage = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithSauna(FiltersChoice choice)
        {
            Sauna = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithGym(FiltersChoice choice)
        {
            Gym = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithSpa(FiltersChoice choice)
        {
            Spa = choice == FiltersChoice.Yes;
            return this;
        }

        public UserPreference WithStars(StarsChoice choice)
        {
            Stars = (int)choice;
            return this;
        }
    }
}
