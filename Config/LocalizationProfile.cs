//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.IO;
using DGGLocalization.Data;
using UnityEngine;

namespace DGGLocalization.Config
{
    [CreateAssetMenu(fileName = "Localization Profile", menuName = "Localization/LocalizationProfile", order = 1)]
    public class LocalizationProfile : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _localizationFile = "localization.loc";
        
        #region Propeties

        private string MainPath { get; } = Application.streamingAssetsPath;
        
        public string LocalizationFile => _localizationFile;
        public string LocalizationPath => Path.Combine(MainPath, _localizationFile);
        //TODO refactor
        public LocalizationData[] LocalizationDates => null;

        #endregion

        #endregion

        /// <summary>
        /// Set localization changes and save.
        /// </summary>
        /// <param name="localizationCode">localization code, is a unique localization identifier.</param>
        /// <param name="languageData">data for localization.</param>
        public void SetLocalization(string localizationCode, LanguageData[] languageData)
        {
            // TODO set
        }

        public void SetLocalizations(List<LocalizationData> localizationDates)
        {
            // TODO set
        }

        /// <summary>
        /// Set language changes and save.
        /// </summary>
        /// <param name="languages">New languages.</param>
        public void SetLanguages(List<Language> languages)
        {
            SetLanguagesWithoutSave(languages);
        }

        /// <summary>
        /// Set language changes without save.
        /// </summary>
        /// <param name="languages">New languages.</param>
        public void SetLanguagesWithoutSave(List<Language> languages) => throw new NotImplementedException(); //TODO set
    }
}
