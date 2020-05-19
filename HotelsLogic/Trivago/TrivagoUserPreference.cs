using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
    
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
