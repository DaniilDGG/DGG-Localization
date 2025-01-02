//Copyright 2024 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using DGGLocalization.Data;

namespace DGGLocalization
{
    public static partial class LocalizationController
    {
        #region Fields

        private static Language[] _languages = Array.Empty<Language>();
        private static Language _currentLanguage;

        #region Propeties

        /// <summary>
        /// Get languages loaded.
        /// </summary>
        public static Language[] Languages => _languages;

        #endregion

        /// <summary>
        /// Called when the language is changed.
        /// </summary>
        public static event Action<Language> OnLanguageSwitch;

        #endregion
        
        /// <summary>
        /// Set language by index.
        /// </summary>
        public static void SwitchLanguage(int index)
        {
            _currentLanguage = _languages[index];
            OnLanguageSwitch?.Invoke(_currentLanguage);
        }

        /// <summary>
        /// Set language by code.
        /// </summary>
        public static void SwitchLanguage(string code)
        {
            var language = GetLanguageByCode(code);
            
            _currentLanguage = language;
            OnLanguageSwitch?.Invoke(_currentLanguage);
        }

        /// <summary>
        /// Get current language.
        /// </summary>
        public static Language GetCurrentLanguage() => _currentLanguage;

        /// <summary>
        /// Get current language index.
        /// </summary>
        public static int GetCurrentLanguageIndex()
        {
            for (var index = 0; index < _languages.Length; index++)
            {
                if (_languages[index] == _currentLanguage) return index;
            }
            
            return -1;
        }
        
        /// <summary>
        /// Get language by code.
        /// </summary>
        public static Language GetLanguageByCode(string code)
        {
            for (var index = 0; index < _languages.Length; index++)
            {
                if (_languages[index].LanguageCode == code)
                {
                    return _languages[index];
                }
            }

            return null;
        }
    }
}