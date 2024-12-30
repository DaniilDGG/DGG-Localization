//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using DGGLocalization.Editor.Helpers;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace DGGLocalization.Editor.Windows
{
    public class XlsxImportWindow : EditorCustomWindow<XlsxImportWindow>
    {
        #region Fields

        private VisualElement _content;
        
        private Toggle _replaceLocalizationInFile;
        private readonly List<Toggle> _languagesToggles = new();

        #region Events

        public event UnityAction<ParametersImport> OnImport; 
        
        #endregion

        #endregion
        
        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            
            LocalizationEditor.Init();
            
            _content = new VisualElement();
            
            Root.Add(new Label("Import XLSX parameters"));

            _replaceLocalizationInFile = new Toggle("Replace only those localizations that are in the file")
            {
                value = true
            };

            Root.Add(_replaceLocalizationInFile);
            Root.Add(new Label("Languages import:"));
            
            Root.Add(_content);
        }

        private void Update() => LanguagesVisible();

        private void CreateGUI()
        {
            for (var index = 0; index < LocalizationController.Languages.Length; index++)
            {
                var toggle = new Toggle(LocalizationController.Languages[index].LanguageCode);
                
                _content.Add(toggle);
                _languagesToggles.Add(toggle);
            }
            
            var buttonImport = new Button
            {
                text = "Import"
            };
            
            buttonImport.clicked += Import;
            rootVisualElement.Add(buttonImport);
        }

        #endregion

        private void LanguagesVisible() => _content.visible = _replaceLocalizationInFile.value;
        
        private void Import()
        {
            var languages = new List<string>();

            for (var index = 0; index < _languagesToggles.Count; index++)
            {
                if (!_languagesToggles[index].value) continue;
                
                languages.Add(LocalizationController.Languages[index]);
            }

            if (languages.Count == 0 && _replaceLocalizationInFile.value) return;

            var parameters = new ParametersImport()
            {
                ReplaceLocalizationInFile = _replaceLocalizationInFile.value,
                Languages = languages
            };
            
            OnImport?.Invoke(parameters);
            
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
