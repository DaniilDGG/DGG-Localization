//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace DGGLocalization.Data
{
    [Serializable]
    public class LocalizationData
    {
        #region Fields

        [SerializeField, JsonProperty] private string _localizationCode;
        [SerializeField, JsonProperty] private List<LanguageData> _data;

        #region Propeties

        /// <summary>
        /// Localization Key.
        /// </summary>
        [JsonIgnore] public string LocalizationCode => _localizationCode;
        /// <summary>
        /// Localization Content.
        /// </summary>
        [JsonIgnore] public List<LanguageData> Data => _data;

        #endregion
        
        #endregion

        public LanguageData GetTargetLocalization(LanguageShort language)
        {
            var targetIndex = Data.FindIndex(data => data.Language == language);

            if (targetIndex != -1) return Data[targetIndex];

            return Data.Count == 0 ? new LanguageData(language, "Localization is null!") : Data[0];
        }
        
        /// <summary>
        /// Create a new instance of LocalizationData.
        /// </summary>
        [JsonConstructor]
        public LocalizationData(string localizationCode, List<LanguageData> languageData)
        {
            _localizationCode = localizationCode;
            _data = languageData;
        }

        /// <summary>
        /// Create a new instance of LocalizationData, setting all languages to the same value.
        /// </summary>
        public LocalizationData(string data)
        {
            _data = new List<LanguageData>();
            
            for (var index = 0; index < LocalizationController.Languages.Length; index++)
            {
                _data.Add(new LanguageData(LocalizationController.Languages[index], data));
            }
        }
        
        internal void SetData(List<LanguageData> data)
        {
            if (data?.Count == 0)
            {
                return;
            }
            
            _data = data;
        }

        /// <summary>
        /// Replace string with string.
        /// </summary>
        /// <param name="a">Old value.</param>
        /// <param name="b">New value.</param>
        /// <returns>Result</returns>
        public LocalizationData Replace(string a, string b)
        {
            var dates = new List<LanguageData>();
            var localizationData = new LocalizationData(_localizationCode, dates);

            for (var index = 0; index < _data.Count; index++)
            {
                var languageData = _data[index];
                var r = languageData.Localization.Replace(a, b);
                languageData = new LanguageData(languageData.Language, r);
                dates.Add(languageData);
            }

            return localizationData;
        }
        
        /// <summary>
        /// Get the default LocalizationData.
        /// </summary>
        public static LocalizationData GetDefaultData(string code)
        {
            List<LanguageData> languageDates = new();

            for (var index = 0; index < LocalizationController.Languages.Length; index++)
            {
                languageDates.Add(new LanguageData(LocalizationController.Languages[index], ""));
            }

            return new LocalizationData(code, languageDates);
        }
        
        /// <summary>
        /// Merge strings LocalizationData.
        /// </summary>
        /// <param name="a">First part.</param>
        /// <param name="b">Second part.</param>
        /// <returns>Result</returns>
        public static LocalizationData operator +(LocalizationData a, LocalizationData b)
        {
            var localizations = new List<LanguageData>();
            
            for (var index = 0; index < a._data.Count; index++)
            {
                var languageData = a._data[index];
                
                languageData += b._data.Find(data => data.Language.LanguageCode == languageData.Language.LanguageCode);

                localizations.Add(languageData);
            }

            return new LocalizationData(a._localizationCode, localizations);
        }
        
        /// <summary>
        /// Add string part to localizations for all languages.
        /// </summary>
        /// <param name="a">First part.</param>
        /// <param name="b">Second part.</param>
        /// <returns>Result</returns>
        public static LocalizationData operator +(LocalizationData a, string b)
        {
            var localizations = new List<LanguageData>();

            for (var index = 0; index < a._data.Count; index++)
            {
                var languageData = a._data[index];

                languageData += b;
                
                localizations.Add(languageData);
            }

            return new LocalizationData(a._localizationCode, localizations);
        }
    }
}