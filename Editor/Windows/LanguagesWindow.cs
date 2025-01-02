using System.Collections.Generic;
using DGGLocalization.Data;
using DGGLocalization.Editor.Helpers;
using DGGLocalization.Loaders;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DGGLocalization.Editor.Windows
{
    public class LanguagesWindow : EditorCustomWindow<LanguagesWindow>
    {
        #region Fields

        private VisualElement _content;
        
        private readonly List<TextField> _codes = new();
        private readonly List<TextField> _names = new();

        private Localization _currentLocalization;
        
        #endregion

        #region Propeties
        
        public override string DisplayName => "Languages settings";

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            var label = new Label("Current Languages:");
            Root.Add(label);

            _content = new VisualElement();
            Root.Add(_content);
            
            SelectLocalization();
        }

        private void SelectLocalization()
        {
            var selected = LocalizationSelectWindow.Open();

            if (selected != null)
            {
                _currentLocalization = selected;
                RefreshUI();
            }
            else
            {
                Debug.LogWarning("Localization selection canceled or no localization available.");
                
                EditorApplication.delayCall += Close;
            }
        }

        private void RefreshUI()
        {
            _content.Clear();
            _codes.Clear();
            _names.Clear();

            foreach (var language in _currentLocalization.Languages)
            {
                Add(language.LanguageCode, language.LanguageName);
            }

            CreateControlButtons();
        }

        private void CreateControlButtons()
        {
            var buttonAdd = new Button { text = "Add" };
            buttonAdd.clicked += delegate { Add("_", "_"); };
            Root.Add(buttonAdd);

            var buttonRemove = new Button { text = "Remove" };
            buttonRemove.clicked += Remove;
            Root.Add(buttonRemove);

            var buttonSave = new Button { text = "Save" };
            buttonSave.clicked += Save;
            Root.Add(buttonSave);
        }

        #endregion

        private void Remove()
        {
            if (_codes.Count == 0) return;

            var index = _codes.Count - 1;

            _content.Remove(_codes[index]);
            _content.Remove(_names[index]);

            _codes.RemoveAt(index);
            _names.RemoveAt(index);
        }

        private void Add(string languageCode, string languageName)
        {
            _codes.Add(TextInputHelper.CreateTextInput(languageCode, "Language Code: ", _content));
            _names.Add(TextInputHelper.CreateTextInput(languageName, "Language Name: ", _content));
        }

        private void Save()
        {
            var languages = new List<Language>();

            for (var index = 0; index < _codes.Count; index++)
            {
                languages.Add(new Language(_codes[index].value, _names[index].value));
            }

            _currentLocalization.SetLocalization(languages.ToArray());

            Loader.SetLocalization(_currentLocalization);
        }
    }
}