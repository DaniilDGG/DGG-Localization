//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine;

namespace DGGLocalization.Data
{
    [Serializable]
    public class Localization
    {
        #region Fields

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(typeof(Guid), "00000000-0000-0000-0000-000000000000")]
        private Guid _guid = Guid.NewGuid();
        
        [JsonProperty] private List<LocalizationData> _localizations = new();
        [JsonProperty] private Language[] _languages = {new("en", "english")};

        #region Properties

        [JsonIgnore] public Guid GUID => _guid;

        [JsonIgnore] public LocalizationData[] Localizations => _localizations.ToArray();
        [JsonIgnore] public Language[] Languages => _languages;

        #endregion

        #endregion

        #region Get

        /// <summary>
        /// Obtain localization when a localization code is available.
        /// </summary>
        /// <param name="localizationCode">localization code, is a unique localization identifier.</param>
        /// <returns>data for localization.</returns>
        public LocalizationData GetLocalization(string localizationCode)
        {
            for (var index = 0; index < _localizations.Count; index++)
            {
                if (_localizations[index].LocalizationCode == localizationCode) return _localizations[index];
            }

            return null;
        }

        #endregion

        #region Set

        /// <summary>
        /// Set localization changes and save.
        /// </summary>
        /// <param name="localizationCode">Localization code, is a unique localization identifier.</param>
        /// <param name="languageData">Data for localization.</param>
        public void SetLocalization(string localizationCode, List<LanguageData> languageData)
        {
            var index = _localizations.FindIndex(data => data.LocalizationCode == localizationCode);

            if (index == -1)
            {
                _localizations.Add(new LocalizationData(localizationCode, languageData));
                return;
            }

            _localizations[index] = new LocalizationData(localizationCode, languageData);
        }
        
        /// <summary>
        /// Set localizations changes and save.
        /// </summary>
        /// <param name="localizationDates">New localizations.</param>
        public void SetLocalization(List<LocalizationData> localizationDates)
        {
            _localizations = localizationDates;
            
            FixLocalizations();
        }

        /// <summary>
        /// Set language changes and save.
        /// </summary>
        /// <param name="languages">New languages.</param>
        public void SetLocalization(Language[] languages)
        {
            if (languages.Length == 0)
            {
                Debug.LogError("Languages == 0! Logic aborted...");
                return;
            }
            
            _languages = languages;
            
            FixLocalizations();
        }

        #endregion
        
        private void FixLocalizations()
        {
            for (var index = 0; index < _localizations.Count; index++)
            {
                _localizations[index].SetData(FixLocalization(_localizations[index]));
            }
        }

        private List<LanguageData> FixLocalization(LocalizationData localizationData)
        {
            var languageDates = new List<LanguageData>(localizationData.Data);
            var languages = new List<Language>(_languages);

            for (var index = languageDates.Count - 1; index >= 0; index--)
            {
                if (languages.Find(language => language.LanguageCode == languageDates[index].Language.LanguageCode) == null)
                {
                    languageDates.RemoveAt(index);
                }
            }

            for (int index = 0; index < languages.Count; index++)
            {
                if (languageDates.FindIndex(language => language.Language.LanguageCode == languages[index].LanguageCode) == -1)
                {
                    languageDates.Add(new LanguageData(languages[index], "empty"));
                }
            }

            return languageDates;
        }
    }
}