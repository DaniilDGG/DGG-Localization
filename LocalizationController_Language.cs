//Copyright 2024 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using DGGLocalization.Data;

namespace DGGLocalization
{
    public static partial class LocalizationController
    {
        #region Fields

        private static Language[] _languages;
        private static Language _currentLanguage;

        #region Propeties

        public static Language[] Languages => _languages;

        #endregion

        public static event Action<Language> OnLanguageSwitch;

        #endregion
        
        public static void SwitchLanguage(int index)
        {
            _currentLanguage = _languages[index];
            OnLanguageSwitch?.Invoke(_currentLanguage);
        }

        public static Language GetCurrentLanguage() => _currentLanguage;

        public static int GetCurrentLanguageIndex()
        {
            for (var index = 0; index < _languages.Length; index++)
            {
                if (_languages[index] == _currentLanguage) return index;
            }
            
            return -1;
        }
        
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