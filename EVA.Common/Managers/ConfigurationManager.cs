using EVA.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace EVA.Common.Managers
{
    public class ConfigurationManager<T> where T : ConfigurationBase
    {
        public static ConfigurationManager<T> Instance => _instance ?? (_instance = new ConfigurationManager<T>());
        private static ConfigurationManager<T> _instance;

        public ConfigurationManager()
        {
            Config = Activator.CreateInstance<T>();
        }

        public void Save()
        {
            ConfigurationBase.Serialize<T>(Config);
        }

        public bool TrySave()
        {
            try
            {
                Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Load()
        {
            Config = ConfigurationBase.Deserialize<T>(Config.Path) ?? Activator.CreateInstance<T>();
        }

        public T Config;
    }
}