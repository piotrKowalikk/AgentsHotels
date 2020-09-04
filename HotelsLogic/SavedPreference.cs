namespace HotelsLogic
{
    public class SavedPreference : UserPreference
    {
        public string PreferenceName { get; set; }
        public override string ToString() => PreferenceName;

        public SavedPreference() { }
        public SavedPreference(string name, UserPreference userPreference)
        {
            this.PreferenceName = name;
            City = userPreference.City;
            DateFrom = userPreference.DateFrom;
            DateTo = userPreference.DateTo;
            NumberOfAdults = userPreference.NumberOfAdults;
            NumberOfChildren = userPreference.NumberOfChildren;
            NumberOfRooms = userPreference.NumberOfRooms;
            Delay = userPreference.Delay;
            NumberOfReturnOffers = userPreference.NumberOfReturnOffers;
            AirConditioning = userPreference.AirConditioning;
            FreeCancelation = userPreference.FreeCancelation;
            Wifi = userPreference.Wifi;
            Bar = userPreference.Bar;
            Pool = userPreference.Pool;
            Fridge = userPreference.Fridge;
            Microwave = userPreference.Microwave;
            Safe = userPreference.Safe;
            Tv = userPreference.Tv;
            Massage = userPreference.Massage;
            Sauna = userPreference.Sauna;
            Gym = userPreference.Gym;
            Spa = userPreference.Spa;
            Stars = userPreference.Stars;
        }
    }
}
