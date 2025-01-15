//Copyright 2024 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DGGLocalization.Data;
using DGGLocalization.Loaders;
using UnityEngine;

[assembly: InternalsVisibleTo("AD_LocalizationEditor"), InternalsVisibleTo("AD_LocalizationEditorXLSX")]
namespace DGGLocalization
{
    public static partial class LocalizationController
    {
        #region Initialization

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() => Initialize(Loader.LoadLocalizations());

        internal static void Initialize(List<(Localization data, string displayName)> dates)
        {
            _localizations.Clear();
            
            foreach (var localization in dates) AddLocalization(localization.data);
            
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
                if (!_localizations.ContainsKey(data.LocalizationCode))
                {
                    _localizations[data.LocalizationCode] = data.Data;

                    continue;
                }
                
                var target = _localizations[data.LocalizationCode];

                foreach (var languageData in data.Data)
                {
                    if (target.FindIndex(t => t.Language == languageData.Language) != -1)
                    {
                        Debug.LogWarning($"Duplicate! {languageData.Language}:{languageData.Localization} ignored!");
                        
                        continue;
                    }
                    
                    target.Add(languageData);
                }
            }
            
            _languages = languages.ToArray();
        }
        
        /// <summary>
        /// Remove an additional localization package. WARNING: High performance cost, due to the need to enumerate all keys.
        /// </summary>
        public static void RemoveLocalization(Localization localization)
        {
            foreach (var data in localization.Localizations)
            {
                if (!_localizations.TryGetValue(data.LocalizationCode, out var target)) continue;

                foreach (var languageData in data.Data)
                {
                    var indexToRemove = target.FindIndex(t => t.Language == languageData.Language);
                    
                    if (indexToRemove != -1)
                    {
                        if (target[indexToRemove].Localization != languageData.Localization) continue;
                        
                        target.RemoveAt(indexToRemove);
                    }
                }
                
                if (target.Count == 0) _localizations.Remove(data.LocalizationCode);
            }
            
            var languages = _languages.ToList();

            foreach (var language in localization.Languages)
            {
                if (_localizations.Values.All(localizationData => localizationData.All(data => data.Language != language)))
                {
                    languages.Remove(language);
                }
            }

            _languages = languages.ToArray();
            
            if (!languages.Contains(_currentLanguage) && languages.Count > 0) SwitchLanguage(0);
        }

        #endregion
    }
}