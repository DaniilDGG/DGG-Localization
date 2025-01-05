//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using UnityEngine;

namespace DGGLocalization.Data
{
    [Serializable]
    public struct LocalizationString
    {
        #region Fields

        [SerializeField] private string _value;

        #region Properties

        /// <summary>
        /// Localization Key.
        /// </summary>
        public string Value => _value;

        #endregion
        
        #endregion
        
        /// <summary>
        /// Create a new instance of LocalizationString. 
        /// </summary>
        /// <param name="value">Key</param>
        public LocalizationString(string value) => _value = value;

        /// <summary>
        /// Get string from LocalizationString.
        /// </summary>
        public static implicit operator string(LocalizationString key) => key._value;
        /// <summary>
        /// Get LocalizationString from string.
        /// </summary>
        public static implicit operator LocalizationString(string key) => new(key);
    }
}