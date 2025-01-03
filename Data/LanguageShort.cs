//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace DGGLocalization.Data
{
    [Serializable]
    public class LanguageShort
    {
        #region Fields

        [SerializeField, JsonProperty] private string _languageCode;

        #region Propeties

        /// <summary>
        /// Language identifier.
        /// </summary>
        [JsonIgnore] public string LanguageCode => _languageCode;

        #endregion
        
        #endregion
        
        /// <summary>
        /// Create a new instance of LanguageShort.
        /// </summary>
        public LanguageShort(string code) => _languageCode = code;
        
        /// <summary>
        /// Returns the identifier of the language.
        /// </summary>
        public static implicit operator string(LanguageShort language) => language?._languageCode;
        
        public static bool operator ==(LanguageShort a, LanguageShort b) => a?.LanguageCode == b?.LanguageCode;
        public static bool operator !=(LanguageShort a, LanguageShort b) => !(a == b);
        
        protected bool Equals(LanguageShort other) => _languageCode == other._languageCode;

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            
            return obj.GetType() == GetType() && Equals((LanguageShort)obj);
        }

        public override int GetHashCode() => LanguageCode?.GetHashCode() ?? 0;
    }
}