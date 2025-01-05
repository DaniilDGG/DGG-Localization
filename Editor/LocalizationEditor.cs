//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DGGLocalization.Config;
using DGGLocalization.Data;
using DGGLocalization.Loaders;
using UnityEditor;
using UnityEngine;

[assembly: InternalsVisibleTo("AD_LocalizationEditorGUI")]

namespace DGGLocalization.Editor
{
    public static partial class LocalizationEditor
    {
        #region Fields

        private static List<(Localization data, string displayName)> _dates = new();
        
        #region Properties

        public static List<(Localization data, string displayName)> Localizations => _dates;

        #endregion
        
        #endregion

        internal static void Reboot()
        {
            _dates = Loader.LoadLocalizations();
            LocalizationController.Initialize(_dates);
            
            Debug.Log($"Used: {_dates.Count} packages.");
        }
        
        private static void Reload(Localization newValue)
        {
            for (var index = 0; index < Localizations.Count; index++)
            {
                var container = Localizations[index];
                
                if (container.data.GUID != newValue.GUID) continue;

                Localizations[index] = (newValue, container.displayName);

                break;
            }
            
            LocalizationController.Initialize(_dates);
        }
        
        static LocalizationEditor()
        {
            Loader.OnSet += Reload;

            Reboot();

            if (_dates.Count != 0) return;
            
            Debug.Log("No localization data were found. Standard profile is generated.");
            
            const string assetPath = "Assets/Resources/LocalizationProfile.asset";
            
            var profile = ScriptableObject.CreateInstance<LocalizationProfile>();
                    
            var folder = System.IO.Path.GetDirectoryName(assetPath);
                
            if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);

            AssetDatabase.CreateAsset(profile, assetPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = profile;

            Reboot();
        }
    }
}
