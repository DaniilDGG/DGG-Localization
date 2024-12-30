using System;
using System.Linq;
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
        private static void LanguagesSetting() => LanguagesWindow.ShowWindow().OnSaveLanguages += _localizationProfile.SetLanguages;

        [MenuItem("Localization/Localization settings")]
        private static void HandleLocalizationSetting() => LocalizationSetting();
        [MenuItem("Localization/Words Count")]
        private static void GetWordsCount()
        {
            Init();

            var stats = LocalizationController.Languages.Select(t => new StatsInLanguage() { Language = t }).ToList();

            foreach (var data in _localizationProfile.LocalizationDates)
            {
                foreach (var languageData in data.Data)
                {
                    var stat = stats.Find(language => language.Language.LanguageCode == languageData.Language);

                    stat.Chars += languageData.Localization.Length;
                    stat.Words += languageData.Localization.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }

            foreach (var stat in stats)
            {
                Debug.Log($"{(string)stat.Language}, chars: {stat.Chars}, words: {stat.Words}");
            }
        }

        #endregion

        #region Set

        private static LocalizationWindow LocalizationSetting()
        {
            var localizationWindow = LocalizationWindow.ShowWindow();
            localizationWindow.OnSaveLocalization += _localizationProfile.SetLocalization;
            return localizationWindow;
        }

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