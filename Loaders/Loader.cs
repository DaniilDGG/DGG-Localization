using System;
using System.Collections.Generic;
using System.Linq;
using DGGLocalization.Data;

namespace DGGLocalization.Loaders
{
    public static class Loader
    {
        public static List<Localization> LoadLocalizations()
        {
            var types = GetLoaders();
            var dates = new List<Localization>();
            
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
        }
        
        private static IEnumerable<Type> GetLoaders() => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && typeof(ILocalizationLoader).IsAssignableFrom(t) && t.GetCustomAttributes(typeof(LocalizationLoaderAttribute), true).Any());
    }
}