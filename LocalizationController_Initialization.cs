//Copyright 2024 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using DGGLocalization.Config;
using DGGLocalization.Data;
using DGGLocalization.Loaders;
using UnityEngine;

namespace DGGLocalization
{
    public static partial class LocalizationController
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() => LoadLocalizations();

        internal static void LoadLocalizations()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && typeof(ILocalizationLoader).IsAssignableFrom(t) && t.GetCustomAttributes(typeof(LocalizationLoaderAttribute), true).Any());

            _localizations.Clear();
            
            foreach (var type in types)
            {
                var loader = (ILocalizationLoader)Activator.CreateInstance(type);
                var localizations = loader.GetLocalizationDates();

                foreach (var localization in localizations) AddLocalization(localization);
            }
            
            SwitchLanguage(0);
        }

        public static void AddLocalization(Localization localization)
        {
            var languages = _languages.ToList();
            
            foreach (var language in localization.Languages)
            {
                if (languages.Find(l => l == language) != null) continue;
                        
                languages.Add(language);
            }

            foreach (var data in localization.Localizations)
            {
                _localizations[data.LocalizationCode] = data.Data;
            }
            
            _languages = languages.ToArray();
        }
        
        public static LocalizationProfile GetProfile()
        {
            var profiles = Resources.LoadAll<LocalizationProfile>("");
                
            return profiles.Length > 0 ? profiles[0] : null;
        }
    }
}