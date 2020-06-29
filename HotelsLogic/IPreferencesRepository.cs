using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HotelsLogic
{
    public interface IPreferencesRepository
    {
        public void Add(SavedPreference pref);
        public bool Delete(SavedPreference pref);
        public IEnumerable<SavedPreference> GetAll();

    }
}
