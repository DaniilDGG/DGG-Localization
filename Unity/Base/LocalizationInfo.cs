//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using DGGLocalization.Data;
using UnityEngine;
using UnityEngine.Events;

namespace DGGLocalization.Unity.Base
{
    public sealed class LocalizationInfo : MonoBehaviour
    {
        #region Fields

        [SerializeField] private string _localizationCode;
        
        private LocalizationData _localizationData;
        private LanguageShort _currentLanguageCode;

        private const string CustomCode = "customCode";
        
        public event UnityAction<string> OnSwitchLanguage;

        #region Properties

        public string LocalizationCode => _localizationCode;

        #endregion

        #endregion

        #region MonoBehavior

        private void Awake()
        {
            if (_localizationCode != CustomCode)
            {
                _localizationData = LocalizationController.GetLocalization(_localizationCode);
            }
            
            LocalizationController.OnLanguageSwitch += HandleOnLanguageSwitch;
            
            HandleOnLanguageSwitch(LocalizationController.GetCurrentLanguage());
        }

        private void OnDestroy() => LocalizationController.OnLanguageSwitch -= HandleOnLanguageSwitch;
        
        #endregion

        private void HandleOnLanguageSwitch(Language language)
        {
            _currentLanguageCode = language;
            OnSwitchLanguage?.Invoke(GetLocalization());
        }
        
        /// <summary>
        /// Get localization, by localization code, which is stored in LocalizationInfo.
        /// </summary>
        /// <returns>Localization for the current language.</returns>
        public string GetLocalization()
        {
            if (_localizationData == null || _localizationData.Data.Count == 0)
            {
                return "localization is null!";
            }

            return _localizationData.GetTargetLocalization(_currentLanguageCode).Localization;
        }

        /// <summary>
        /// Set localization by key.
        /// </summary>
        public void SetLocalization(string localizationCode)
        {
            _localizationCode = localizationCode;
            _localizationData = LocalizationController.GetLocalization(_localizationCode);
            
            OnSwitchLanguage?.Invoke(GetLocalization());
        }

        /// <summary>
        /// Set localization by LocalizationData.
        /// </summary>
        public void SetLocalization(LocalizationData localizationData)
        {
            _localizationCode = CustomCode;
            _localizationData = localizationData;
            OnSwitchLanguage?.Invoke(GetLocalization());
        }
    }
}
