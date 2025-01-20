//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using DGGLocalization.Data;
using UnityEngine.Scripting;

namespace DGGLocalization.Loaders
{
    [Preserve]
    public interface ILocalizationLoader
    {
        /// <summary>
        /// Get localization dates
        /// </summary>
        public List<(Localization data, string displayName)> GetLocalizationDates();
        /// <summary>
        /// Set new localization dates
        /// </summary>
        public bool SetLocalizationData(Localization data);
    }
}