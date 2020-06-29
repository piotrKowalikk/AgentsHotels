namespace HotelsLogic
{
    public class SavedPreference : UserPreference
    {
        public string PreferenceName { get; set; }
        public override string ToString() => PreferenceName;
    }
}
