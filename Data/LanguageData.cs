using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace DGGLocalization.Data
{
    [Serializable]
    public struct LanguageData
    {
        #region Fields

        [SerializeField, JsonProperty] private LanguageShort _language;
        [SerializeField, JsonProperty] private string _localization;

        #region Propeties

        /// <summary>
        /// Language Identifier.
        /// </summary>
        [JsonIgnore] public LanguageShort Language => _language;
        /// <summary>
        /// Content.
        /// </summary>
        [JsonIgnore] public string Localization => _localization;

        #endregion
        
        #endregion

        /// <summary>
        /// Create a new instance of LanguageData.
        /// </summary>
        /// <param name="language">Language Identifier.</param>
        /// <param name="localization">Localization string content.</param>
        public LanguageData(LanguageShort language, string localization)
        {
            _language = language;
            _localization = localization;
        }
        
        /// <summary>
        /// Merge strings LanguageData.
        /// </summary>
        /// <param name="a">First part.</param>
        /// <param name="b">Second part.</param>
        /// <returns>Result</returns>
        public static LanguageData operator +(LanguageData a, LanguageData b)
        {
            a._localization += b._localization;

            return a;
        }
        
        /// <summary>
        /// Add a string part to the localization for the language.
        /// </summary>
        /// <param name="a">First part.</param>
        /// <param name="b">Second part.</param>
        /// <returns>Result</returns>
        public static LanguageData operator +(LanguageData a, string b)
        {
            a._localization += b;

            return a;
        }
    }
}