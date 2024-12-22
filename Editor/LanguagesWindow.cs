//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using DGGLocalization.Data;
using DGGLocalization.Editor.Helpers;
using UnityEngine.UIElements;

namespace DGGLocalization.Editor
{
    public class LanguagesWindow : EditorCustomWindow<LanguagesWindow>
    {
        #region Fields

        private VisualElement _content;
        
        private readonly List<TextField> _codes = new();
        private readonly List<TextField> _names = new();

        #region Events

        public event Action<List<Language>> OnSaveLanguages;

        #endregion
        
        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            
            var label = new Label("Current Languages:");
            
            Root.Add(label);

            _content = new VisualElement();
            
            Root.Add(_content);
        }

        private void CreateGUI()
        {
            foreach (var language in LocalizationController.Languages) Add(language.LanguageCode, language.LanguageName);
            
            var buttonAdd = new Button
            {
                text = "Add"
            };
            buttonAdd.clicked += delegate{Add("_", "_");};
            rootVisualElement.Add(buttonAdd);
            
            var buttonRemove = new Button
            {
                text = "Remove"
            };
            buttonRemove.clicked += Remove;
            rootVisualElement.Add(buttonRemove);
            
            var button = new Button
            {
                text = "Save"
            };
            button.clicked += Save;
            rootVisualElement.Add(button);
        }

        #endregion

        private void Remove()
        {
            var index = _codes.Count - 1;
            
            _content.Remove(_codes[index]);
            _content.Remove(_names[index]);
            
            _codes.RemoveAt(index);
            _names.RemoveAt(index);
        }
        
        private void Add(string languageCode, string languageName)
        {
            _codes.Add(TextInputHelper.CreateTextInput(languageCode, "language code: ", _content));
            _names.Add(TextInputHelper.CreateTextInput(languageName, "language name: ", _content));
        }
        
        private void Save()
        {
            var languages = new List<Language>();

            for (var index = 0; index < _codes.Count; index++)
            {
                languages.Add(new Language(_codes[index].text, _names[index].text));
            }
            
            OnSaveLanguages?.Invoke(languages);
        }
    }
}
