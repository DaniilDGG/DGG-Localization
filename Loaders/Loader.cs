//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DGGLocalization.Data;

namespace DGGLocalization.Loaders
{
    internal static class Loader
    {
        #region Fields

        private static readonly List<ILocalizationLoader> RuntimeLoaders = new();
        private static readonly List<ILocalizationLoader> EditorLoaders = new();

        #endregion
        
        #region Events

        public static event Action<Localization> OnSet;

        #endregion
        
        public static List<(Localization data, string displayName)> LoadLocalizations(bool isEditor = false)
        {
            var loaders = GetLoaders(isEditor);
            var dates = new List<(Localization, string)>();
            
            foreach (var loader in loaders)
            {
                var localizations = loader.GetLocalizationDates();

                foreach (var localization in localizations) dates.Add(localization);
            }

            return dates;
        }

        public static void SetLocalization(Localization localization, bool isRuntime = false)
        {
            var loaders = GetLoaders(!isRuntime);
            
            foreach (var loader in loaders)
            {
                if (loader.SetLocalizationData(localization)) return;
            }
            
            OnSet?.Invoke(localization);
        }
        
        private static IReadOnlyList<ILocalizationLoader> GetLoaders(bool isEditor) => isEditor ? EditorLoaders : RuntimeLoaders;

        static Loader()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && typeof(ILocalizationLoader).IsAssignableFrom(t) && t.GetCustomAttributes(typeof(LocalizationLoaderAttribute), true).Any());
            
            foreach (var type in types)
            {
                var loader = (ILocalizationLoader)Activator.CreateInstance(type);
                var attribute = type.GetCustomAttribute<LocalizationLoaderAttribute>();
                
                if (attribute.RuntimeSupported) RuntimeLoaders.Add(loader);
                if (attribute.EditorSupported) EditorLoaders.Add(loader);
            }
        }
    }
}