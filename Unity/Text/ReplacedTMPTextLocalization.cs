//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using DGGLocalization.Unity.Base;
using UnityEngine;

namespace DGGLocalization.Unity.Text
{
    [RequireComponent(typeof(LocalizationInfo))]
    public sealed class ReplacedTMPTextLocalization : AbstractTextLocalization
    {
        #region Fields

        [SerializeField] private ReplacedElement[] _replacedElements;

        #endregion
        
        protected override string GetText(string original)
        {
            for (var index = 0; index < _replacedElements.Length; index++)
            {
                original = original.Replace(_replacedElements[index].Original, _replacedElements[index].Replacement);
            }

            return original;
        }

        [Serializable]
        public struct ReplacedElement
        {
            #region Fields

            [SerializeField] private string _original;
            [SerializeField] private string _replacement;

            #region Propeties

            public string Original => _original;
            public string Replacement => _replacement;

            #endregion

            #endregion
        }
    }
}