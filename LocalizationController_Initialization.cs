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

        #endregion
    }
}