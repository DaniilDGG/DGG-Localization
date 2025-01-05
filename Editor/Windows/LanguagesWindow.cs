// Copyright 2025 Daniil Glagolev
// Licensed under the Apache License, Version 2.0

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
        private ScrollView _contentScroll;

        private readonly List<TextField> _codes = new();
        private readonly List<TextField> _names = new();

        private Localization _currentLocalization;

        #endregion

        #region Properties

        public override string DisplayName => "Languages Settings";

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            minSize = new Vector2(400, 170);

            var headerLabel = new Label("Languages Configuration")
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 16,
                    marginBottom = 10
                }
            };
            Root.Add(headerLabel);

            _contentScroll = new ScrollView
            {
                style =
                {
                    flexGrow = 1,
                    flexDirection = FlexDirection.Column,
                    marginBottom = 40
                },
                horizontalScrollerVisibility = ScrollerVisibility.Hidden
            };
            _content = new VisualElement
            {
                style = { flexDirection = FlexDirection.Column }
            };
            _contentScroll.Add(_content);
            Root.Add(_contentScroll);

            CreateControlButtons();
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
        }

        private void CreateControlButtons()
        {
            var buttonContainer = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    justifyContent = Justify.SpaceBetween,
                    marginTop = 10,
                    marginBottom = 0,
                    position = Position.Absolute,
                    bottom = 0,
                    left = 0,
                    right = 0,
                    backgroundColor = new Color(0.15f, 0.15f, 0.15f, 1),
                    paddingLeft = 10,
                    paddingRight = 10,
                    height = 40
                }
            };

            var buttonAdd = new Button { text = "Add" };
            buttonAdd.clicked += delegate { Add("_", "_"); };
            buttonContainer.Add(buttonAdd);

            var buttonSave = new Button { text = "Save" };
            buttonSave.clicked += Save;
            buttonContainer.Add(buttonSave);

            rootVisualElement.Add(buttonContainer);
        }

        #endregion

        private void Add(string languageCode, string languageName)
        {
            var row = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    width = Length.Percent(100)
                }
            };

            var codeField = TextInputHelper.CreateTextInput(languageCode, "Key:", row);
            codeField.style.width = Length.Percent(40);
            
            var nameField = TextInputHelper.CreateTextInput(languageName, "Name:", row);
            nameField.style.width = Length.Percent(50);

            var removeButton = new Button { text = "-" };
            removeButton.clicked += () => RemoveRow(row, codeField, nameField);
            removeButton.style.width = Length.Percent(5);
            row.Add(removeButton);

            _codes.Add(codeField);
            _names.Add(nameField);

            _content.Add(row);
        }

        private void RemoveRow(VisualElement row, TextField codeField, TextField nameField)
        {
            _content.Remove(row);
            _codes.Remove(codeField);
            _names.Remove(nameField);
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
