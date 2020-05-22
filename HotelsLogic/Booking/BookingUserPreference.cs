﻿using System.Text.Json;

namespace HotelsLogic
{
    class BookingUserPreference
    {
        private UserPreference userPreference;

        public BookingUserPreference(UserPreference userPreference)
        {
            this.userPreference = userPreference;
        }

        public string GetDeserializedUserPreference()
        {
            return JsonSerializer.Serialize(userPreference);
        }
    }
}
