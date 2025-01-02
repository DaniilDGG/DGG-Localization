using System;
using System.Collections.Generic;
using DGGLocalization.Data;
using DGGLocalization.Editor.Windows;
using UnityEditor;
using UnityEngine;

namespace DGGLocalization.Editor
{
    public static partial class LocalizationEditor
    {
        public static void OpenLocalizationSetting(string code)
        {
            var localizationWindow = LocalizationSetting();
            localizationWindow.OpenCodeWindow(code);
        }

        #region Items

        [MenuItem("Localization/Language settings")]
        private static void LanguagesSetting() => LanguagesWindow.ShowWindow();

        [MenuItem("Localization/Localization settings")]
        private static void HandleLocalizationSetting() => LocalizationSetting();
        [MenuItem("Localization/Words Count")]
        private static void GetWordsCount()
        {
            foreach (var container in Localizations)
            {
                var localization = container.data;
                
                var stats = new Dictionary<string, StatsInLanguage>();
                
                foreach (var language in localization.Languages)
                {
                    if (!stats.ContainsKey(language.LanguageCode))
                    {
                        stats[language.LanguageCode] = new StatsInLanguage
                        {
                            Language = language,
                            Chars = 0,
                            Words = 0
                        };
                    }
                }

                foreach (var localizationData in localization.Localizations)
                {
                    foreach (var languageData in localizationData.Data)
                    {
                        if (!stats.TryGetValue(languageData.Language.LanguageCode, out var stat)) continue;
                        
                        stat.Chars += languageData.Localization.Length;
                        stat.Words += languageData.Localization.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length;
                    }
                }
                
                foreach (var stat in stats.Values)
                {
                    Debug.Log($"({container.displayName})-({stat.Language.LanguageCode}): chars: {stat.Chars}, words: {stat.Words}");
                }
            }
        }

        #endregion

        #region Set

        private static LocalizationWindow LocalizationSetting() => LocalizationWindow.ShowWindow();

        #endregion

        #region Data

        private class StatsInLanguage
        {
            public int Chars;
            public int Words;
            
            public LanguageShort Language;
        }

        #endregion
    }
}