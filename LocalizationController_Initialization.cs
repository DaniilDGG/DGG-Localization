//Copyright 2024 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using DGGLocalization.Data;
using DGGLocalization.Loaders;
using UnityEngine;

[assembly: InternalsVisibleTo("AD_LocalizationEditor")]
namespace DGGLocalization
{
    public static partial class LocalizationController
    {
        #region Initialization

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

            if (_languages.Length == 0) return;
            
            SwitchLanguage(0);
        }

        #endregion

        #region Add

        /// <summary>
        /// Add an additional localization package.
        /// </summary>
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

        #endregion
    }
}