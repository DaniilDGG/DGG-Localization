//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.IO;
using UnityEngine;

namespace DGGLocalization.Config
{
    [CreateAssetMenu(fileName = "Localization Profile", menuName = "Localization/LocalizationProfile", order = 1)]
    public class LocalizationProfile : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _localizationFile = "localization.loc";
        
        #region Properties

        private string MainPath { get; } = Application.streamingAssetsPath;
        
        public string LocalizationFile => _localizationFile;
        public string LocalizationPath => Path.Combine(MainPath, _localizationFile);

        #endregion

        #endregion
    }
}
