using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace HotelsLogic
{
    public class PreferencesRepository : IPreferencesRepository
    {
        public string PreferencesPath => "./Preferences";
        public static PreferencesRepository PreferencesRepositoryInstance { get; private set; } = new PreferencesRepository();

        private PreferencesRepository()
        {
            if (!Directory.Exists(PreferencesPath))
            {
                Directory.CreateDirectory(PreferencesPath);
            }
        }

        public void Add(SavedPreference pref)
        {
            string jsonString = JsonConvert.SerializeObject(pref);
            File.WriteAllText($"{PreferencesPath}/{pref.PreferenceName}", jsonString);
        }

        public bool Delete(SavedPreference pref)
        {
            try
            {
                File.Delete($"{PreferencesPath}/{pref.PreferenceName}");
            }
            catch (System.Exception)
            {

                return false;
            }

            return true;
        }

        public IEnumerable<SavedPreference> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
