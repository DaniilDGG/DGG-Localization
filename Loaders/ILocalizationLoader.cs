using System.Collections.Generic;
using DGGLocalization.Data;

namespace DGGLocalization.Loaders
{
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