using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (pref == null)
            {
                return;
            }

            string json = JsonConvert.SerializeObject(pref);
            File.WriteAllText($"{PreferencesPath}/{pref.PreferenceName}.txt", json);
        }

        public bool Delete(SavedPreference pref)
        {
            if(pref == null)
            {
                return false;
            }

            try
            {
                File.Delete($"{PreferencesPath}/{pref.PreferenceName}.txt");
            }
            catch
            {
                return false;
            }

            return true;
        }

        public IEnumerable<SavedPreference> GetAll()
        {
            List<SavedPreference> result = new List<SavedPreference>();
            try
            {
                string[] fileNames = Directory.GetFiles(PreferencesPath);

                foreach (string name in fileNames)
                {
                    string json = File.ReadAllText(name);
                    result.Add(JsonConvert.DeserializeObject<SavedPreference>(json));
                }
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
