using System;
using System.Collections.Generic;
using System.Linq;
using DGGLocalization.Data;

namespace DGGLocalization.Loaders
{
    internal static class Loader
    {
        #region Events

        public static event Action<Localization> OnSet;

        #endregion
        
        public static List<(Localization data, string displayName)> LoadLocalizations()
        {
            var types = GetLoaders();
            var dates = new List<(Localization, string)>();
            
            foreach (var type in types)
            {
                var loader = (ILocalizationLoader)Activator.CreateInstance(type);
                var localizations = loader.GetLocalizationDates();

                foreach (var localization in localizations) dates.Add(localization);
            }

            return dates;
        }

        public static void SetLocalization(Localization localization)
        {
            var types = GetLoaders();
            
            foreach (var type in types)
            {
                var loader = (ILocalizationLoader)Activator.CreateInstance(type);

                if (loader.SetLocalizationData(localization)) return;
            }
            
            OnSet?.Invoke(localization);
        }
        
        private static IEnumerable<Type> GetLoaders() => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && typeof(ILocalizationLoader).IsAssignableFrom(t) && t.GetCustomAttributes(typeof(LocalizationLoaderAttribute), true).Any());
    }
}