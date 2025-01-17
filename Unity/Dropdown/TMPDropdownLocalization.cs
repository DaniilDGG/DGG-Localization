//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using DGGLocalization.Data;
using TMPro;
using UnityEngine;

namespace DGGLocalization.Unity.Dropdown
{
    public class TMPDropdownLocalization : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private List<LocalizationString> _items = new();
        
        private LocalizationData[] _localizationDates;
        private Language _currentLanguage;
        
        private const string CustomCode = "customCode";

        #region Properties

        /// <summary>
        /// Dropdown Items. To change the items you need to use SetItems() method.
        /// </summary>
        public IReadOnlyList<string> StringItems => _items.ConvertAll(item => item.Value);
        
        /// <summary>
        /// Dropdown Items. To change the items you need to use SetItems() method.
        /// </summary>
        public IReadOnlyList<LocalizationString> Items => _items;

        #endregion

        #endregion

        #region MonoBehavior

        private void Awake()
        {
            LoadItems();
            
            LocalizationController.OnLanguageSwitch += HandleOnLanguageSwitch;
            
            HandleOnLanguageSwitch(LocalizationController.GetCurrentLanguage());
        }

        private void OnDestroy() => LocalizationController.OnLanguageSwitch -= HandleOnLanguageSwitch;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!_dropdown) _dropdown = GetComponent<TMP_Dropdown>();
            
            if (Application.isPlaying || !_dropdown) return;
            
            _dropdown.ClearOptions();
            _dropdown.AddOptions(_items.ConvertAll(item => item.Value));
        }
#endif

        #endregion

        #region Items

        private void LoadItems()
        {
            if (_items.Count == 0)
            {
                _localizationDates = Array.Empty<LocalizationData>();
                return;
            }
            
            _localizationDates = new LocalizationData[_items.Count];
            
            for (var index = 0; index < _items.Count; index++)
            {
                if (_items[index] == CustomCode) continue;
                
                _localizationDates[index] = LocalizationController.GetLocalization(_items[index]);
            }
        }
        
        private void HandleOnLanguageSwitch(Language getCurrentLanguage)
        {
            _currentLanguage = getCurrentLanguage;
            
            var localizations = new List<string>();

            if (_dropdown.options.Count > _localizationDates.Length)
            {
                var excessCount = _dropdown.options.Count - _localizationDates.Length;
                _dropdown.options.RemoveRange(_localizationDates.Length, excessCount);
            }
            
            for (var index = 0; index < _localizationDates.Length; index++)
            {
                var localizationData = _localizationDates[index];

                if (localizationData?.Data == null)
                {
                    localizations.Add("localization is null");
                    continue;
                }
                
                var indexData = localizationData.Data.FindIndex(data => data.Language.LanguageCode == _currentLanguage);

                localizations.Add(indexData != -1 ? localizationData.Data[indexData].Localization : "localization is null");
            }

            for (var index = 0; index < localizations.Count; index++)
            {
                if (index >= _dropdown.options.Count) _dropdown.AddOptions(new List<string>(1){localizations[index]});
                else _dropdown.options[index].text = localizations[index];
            }
            
            _dropdown.RefreshShownValue();
        }

        #endregion

        /// <summary>
        /// When invoked, the dropdown will be updated, with a new list of items.
        /// </summary>
        /// <param name="items"></param>
        public void SetItems(List<string> items)
        {
            _items = items.ConvertAll(item => new LocalizationString(item));
            
            LoadItems();
            HandleOnLanguageSwitch(_currentLanguage);
        }
    }
}
