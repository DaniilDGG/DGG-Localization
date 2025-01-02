//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using DGGLocalization.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DGGLocalization.Editor.Windows
{
    public class XlsxImportWindow : EditorWindow
    {
        #region Fields
        
        private VisualElement _content;
        
        private Toggle _replaceLocalizationInFile;
        private readonly List<Toggle> _languagesToggles = new();

        private ParametersImport _parameters;
        private Localization _localization;

        #endregion
        
        #region Unity Methods

        protected void OnEnable()
        {
            _content = new ScrollView()
            {
                style =
                {
                    flexGrow = 1, 
                },
                horizontalScrollerVisibility = ScrollerVisibility.Hidden,
                verticalScrollerVisibility = ScrollerVisibility.Auto
            };
            
            _content.Add(new Label("Languages import:"));
            
            rootVisualElement.Add(new Label("Import XLSX parameters"));

            _replaceLocalizationInFile = new Toggle("Replace only those localizations that are in the file")
            {
                value = true
            };

            rootVisualElement.Add(_replaceLocalizationInFile);
            
            rootVisualElement.Add(_content);
            
            var buttonImport = new Button
            {
                text = "Import"
            };
            buttonImport.clicked += Import;
            rootVisualElement.Add(buttonImport);
        }

        private void OnGUI()
        {
            if (_localization is { Languages: not null })
            {
                for (var index = 0; index < _localization.Languages.Length; index++)
                {
                    if (_languagesToggles.Count > index) continue;
                    
                    var toggle = new Toggle(_localization.Languages[index].LanguageCode);
                    _content.Add(toggle);
                    _languagesToggles.Add(toggle);
                }
            }

            LanguagesVisible();
        }

        #endregion

        // ReSharper disable PossibleLossOfFraction
        public static (ParametersImport parameters, Localization target)? Open()
        {
            var localization = LocalizationSelectWindow.Open();

            if (localization == null) return null;
            
            var window = CreateInstance<XlsxImportWindow>();

            window._localization = localization;
            
            window.titleContent = new GUIContent("Import parameters");
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 250);
            
            window.ShowModalUtility();

            if (window._parameters == null) return null;
            
            return (window._parameters, window._localization);
        }

        private void LanguagesVisible() => _content.visible = _replaceLocalizationInFile.value;
        
        private void Import()
        {
            var languages = new List<string>();

            for (var index = 0; index < _languagesToggles.Count; index++)
            {
                if (!_languagesToggles[index].value) continue;
                
                languages.Add(_localization.Languages[index]);
            }

            if (languages.Count == 0 && _replaceLocalizationInFile.value) return;

            _parameters = new ParametersImport()
            {
                ReplaceLocalizationInFile = _replaceLocalizationInFile.value,
                Languages = languages
            };

            Close();
        }
    }
    
    public class ParametersImport
    {
        #region Fields

        public bool ReplaceLocalizationInFile;
        public List<string> Languages;

        #endregion
    }
}
