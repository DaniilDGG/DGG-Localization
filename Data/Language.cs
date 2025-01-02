//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace DGGLocalization.Data
{
    [Serializable]
    public class Language : LanguageShort
    {
        #region Fields

        [SerializeField, JsonProperty] private string _languageName;

        #region Propeties

        /// <summary>
        /// The displayed language name.
        /// </summary>
        [JsonIgnore] public string LanguageName => _languageName;

        #endregion

        #endregion

        /// <summary>
        /// Create a new instance of Language.
        /// </summary>
        public Language(string code, string name) : base(code) => _languageName = name;
    }
}
