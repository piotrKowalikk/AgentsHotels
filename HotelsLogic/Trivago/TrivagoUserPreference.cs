using System.Text.Json;

namespace HotelsLogic
{
    class TrivagoUserPreference
    {
        private UserPreference userPreference;

        public TrivagoUserPreference(UserPreference userPreference)
        {
            this.userPreference = userPreference;
        }

        public string GetDeserializedUserPreference()
        {
            return JsonSerializer.Serialize(userPreference);
        }
    }
}
