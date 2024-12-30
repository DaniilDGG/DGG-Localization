//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using DGGLocalization.Config;
using DGGLocalization.Data;
using DGGLocalization.Loaders;
using UnityEditor;
using UnityEngine;

namespace DGGLocalization.Editor
{
    public static partial class LocalizationEditor
    {
        #region Fields

        private static List<Localization> _dates = new();
        
        private static LocalizationProfile _localizationProfile;
        
        #region Propeties

        public static LocalizationProfile LocalizationProfile => _localizationProfile;
        public static List<Localization> Localizations => _dates;

        #endregion
        
        #endregion

        public static void Init()
        {
            Load();

            if (_dates.Count != 0) return;
            
            const string assetPath = "Assets/Resources/LocalizationProfile.asset";
            
            _localizationProfile = ScriptableObject.CreateInstance<LocalizationProfile>();
                    
            var folder = System.IO.Path.GetDirectoryName(assetPath);
                
            if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(_localizationProfile, assetPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = _localizationProfile;

            Load();
            
            return;
            
            void Load()
            {
                LocalizationController.LoadLocalizations();
                _dates = Loader.LoadLocalizations();
            }
        }
    }
}
