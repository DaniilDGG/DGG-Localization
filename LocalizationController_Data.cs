//Copyright 2024 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using DGGLocalization.Data;

namespace DGGLocalization
{
    [Serializable]
    public static partial class LocalizationController
    {
        #region Fields

        private static Dictionary<string, List<LanguageData>> _localizations = new();
        
        #endregion

        /// <summary>
        /// Checks if a localization exists for the given code.
        /// </summary>
        /// <param name="localizationCode">Unique localization identifier.</param>
        /// <returns>True if localization exists</returns>
        public static bool HasLocalization(string localizationCode)
        {
            var localization = GetLocalization(localizationCode);
            
            if (localization == null) return false;

            var language = GetCurrentLanguage();
            var currentData = localization.GetTargetLocalization(language);
            
            return currentData.Language == language;
        }
            
        /// <summary>
        /// Obtain localization when a localization code is available.
        /// </summary>
        /// <param name="localizationCode">localization code, is a unique localization identifier.</param>
        /// <returns>data for localization.</returns>
        public static LocalizationData GetLocalization(string localizationCode)
        {
            return _localizations.TryGetValue(localizationCode, out var value) ? new LocalizationData(localizationCode, value) : null;
        }
        
        /// <summary>
        /// Obtain localization when a localization code is available.
        /// </summary>
        /// <param name="localizationCode">localization code, is a unique localization identifier.</param>
        /// <returns>data for localization.</returns>
        public static LanguageData GetCurrentLocalization(string localizationCode)
        {
            var localization = GetLocalization(localizationCode);

            return localization?.GetTargetLocalization(GetCurrentLanguage()) ?? new LanguageData(new LanguageShort(""),"Localization is null!");
        }
    }
}
