//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using DGGLocalization.Data;
using DGGLocalization.Editor.Helpers;
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

        #region Events

        public event Action<string, LanguageData[]> OnSaveLocalization;

        #endregion

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            
            LocalizationEditor.Init();
            
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
            var save = new Button
            {
                text = "Save"
            };
            save.clicked += Save;
            _visualElementLocalizations.Add(save);
            
            var returnButton = new Button
            {
                text = "return"
            };
            returnButton.clicked += Return;
            _visualElementLocalizations.Add(returnButton);

            var continueButton = new Button
            {
                text = "continue"
            };
            continueButton.clicked += Continue;
            _visualElementCode.Add(continueButton);

            _codeField = TextInputHelper.CreateTextInput("localizationCode", "code: " ,  _visualElementCode);

            for (var index = 0; index < LocalizationController.Languages.Length; index++)
            {
                var input = TextInputHelper.CreateTextInput("", LocalizationController.Languages[index].LanguageCode, _visualElementLocalizations, true);
                
                _localizationFields.Add(input);
            }
            
            _content.Add(_visualElementCode);
        }

        #endregion

        private void Save()
        {
            var languages = new LanguageData[_localizationFields.Count];

            for (var index = 0; index < languages.Length; index++)
            {
                languages[index] = new LanguageData(LocalizationController.GetLanguageByCode(_localizationFields[index].label), _localizationFields[index].text);
            }

            OnSaveLocalization?.Invoke(_code, languages);
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
            _code = _codeField.value;
            OpenCodeWindow();
        }

        private void OpenCodeWindow()
        {
            _content.Remove(_visualElementCode);
            _content.Add(_visualElementLocalizations);

            var localizationData = LocalizationController.GetLocalization(_code);

            if (localizationData == null)
            {
                for (var index = 0; index < _localizationFields.Count; index++)
                {
                    _localizationFields[index].value = "empty";
                }

                return;
            }
            
            for (var index = 0; index < localizationData.Data.Count; index++)
            {
                _localizationFields[index].value = localizationData.Data[index].Localization;
                _localizationFields[index].label = localizationData.Data[index].Language.LanguageCode;
            }
        }

        public void OpenCodeWindow(string code)
        {
            _code = code;
            OpenCodeWindow();
        }
    }
}
