//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using DGGLocalization.Data;
using DGGLocalization.Editor.Helpers;
using DGGLocalization.Loaders;
using UnityEngine;
using UnityEngine.UIElements;

namespace DGGLocalization.Editor.Windows
{
    public class LocalizationWindow : EditorCustomWindow<LocalizationWindow>
    {
        #region Fields

        private VisualElement _content;

        private VisualElement _visualElementCode;
        private VisualElement _visualElementLocalizations;

        private TextField _codeField;

        private string _code;

        private readonly List<TextField> _localizationFields = new();

        private Localization _currentLocalization;

        #endregion
        
        #region Properties
        
        public override string DisplayName => "Localization settings";

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            var label = new Label("Localization");
            _content = new VisualElement();

            Root.Add(label);
            Root.Add(_content);

            _visualElementCode = new VisualElement();
            _visualElementLocalizations = new ScrollView
            {
                style =
                {
                    flexGrow = 1,
                },
                horizontalScrollerVisibility = ScrollerVisibility.Hidden,
                verticalScrollerVisibility = ScrollerVisibility.Auto
            };
        }

        private void CreateGUI()
        {
            _codeField = TextInputHelper.CreateTextInput("localizationCode", "Code: ", _visualElementCode);
            
            var continueButton = new Button
            {
                text = "Continue"
            };
            continueButton.clicked += delegate
            {
                _code = _codeField.value;

                Continue();
            };
            _visualElementCode.Add(continueButton);
            
            _content.Add(_visualElementCode);
        }

        #endregion

        private void Save()
        {
            if (_currentLocalization == null)
            {
                Debug.LogError("No localization selected for saving.");
                return;
            }

            var languageData = new List<LanguageData>();

            foreach (var field in _localizationFields)
            {
                var language = Array.Find(_currentLocalization.Languages, lang => lang.LanguageCode == field.label);
                if (language != null)
                {
                    languageData.Add(new LanguageData(language, field.value));
                }
            }

            _currentLocalization.SetLocalization(_code, languageData);

            Loader.SetLocalization(_currentLocalization);
        }

        private void Return()
        {
            _code = "";
            _codeField.value = _code;

            _content.Add(_visualElementCode);
            _content.Remove(_visualElementLocalizations);
        }

        private void Continue()
        {
            var localizations = FindLocalizationByCode(_code);

            switch (localizations.Count)
            {
                case 0:
                {
                    var selected = LocalizationSelectWindow.Open();

                    if (selected != null)
                    {
                        _currentLocalization = selected;
                        OpenCodeWindow();
                    }
                    else Debug.LogWarning("Selected null!");

                    break;
                }
                case 1:
                    _currentLocalization = localizations[0].data;
                    OpenCodeWindow();
                    break;
                default:
                {
                    var selected = LocalizationSelectWindow.Open(localizations);

                    if (selected != null)
                    {
                        _currentLocalization = selected;
                        OpenCodeWindow();
                    }
                    else Debug.LogWarning("Selected null!");

                    break;
                }
            }
        }

        private void OpenCodeWindow()
        {
            if (_content.Contains(_visualElementCode)) _content.Remove(_visualElementCode);
            if (!_content.Contains(_visualElementLocalizations)) _content.Add(_visualElementLocalizations);

            _localizationFields.Clear();
            _visualElementLocalizations.Clear();

            if (_currentLocalization == null)
            {
                Debug.LogError("No localization available to edit.");
                return;
            }

            var localizationData = Array.Find(_currentLocalization.Localizations, data => data.LocalizationCode == _code);

            foreach (var language in _currentLocalization.Languages)
            {
                var field = TextInputHelper.CreateTextInput(
                    language.LanguageCode,
                    language.LanguageCode,
                    _visualElementLocalizations,
                    true
                );

                if (localizationData != null)
                {
                    var languageData = localizationData.Data.Find(data => data.Language.LanguageCode == language.LanguageCode);
                    field.value = languageData.Localization ?? "empty";
                }
                else
                {
                    field.value = "empty";
                }

                _localizationFields.Add(field);
            }
            
            var save = new Button
            {
                text = "Save"
            };
            save.clicked += Save;
            _visualElementLocalizations.Add(save);

            var returnButton = new Button
            {
                text = "Return"
            };
            returnButton.clicked += Return;
            _visualElementLocalizations.Add(returnButton);
            
        }

        private List<(Localization data, string displayName)> FindLocalizationByCode(string code)
        {
            var localizations = new List<(Localization data, string displayName)>();
            
            foreach (var (localization, displayName) in LocalizationEditor.Localizations)
            {
                if (Array.Exists(localization.Localizations, data => data.LocalizationCode == code)) localizations.Add((localization, displayName));
            }

            return localizations;
        }

        public void OpenCodeWindow(string code)
        {
            _code = code;
            
            Continue();
        }
    }
}
