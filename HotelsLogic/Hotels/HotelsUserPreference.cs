using System.Text.Json;

namespace HotelsLogic
{
    class HotelsUserPreference
    {
        private UserPreference userPreference;

        public HotelsUserPreference(UserPreference userPreference)
        {
            this.userPreference = userPreference;
        }

        public string GetDeserializedUserPreference()
        {
            return JsonSerializer.Serialize(userPreference);
        }
    }
}
