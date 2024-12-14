using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DGGLocalization.Config;
using DGGLocalization.Data;
using DGGLocalization.Loaders;
using UnityEngine;

namespace DGGLocalization
{
    [LocalizationLoader]
    public class ProfileLoader : ILocalizationLoader
    {
        public List<Localization> GetLocalizationDates()
        {
            var profiles = Resources.LoadAll<LocalizationProfile>("");

            var dates = new List<Localization>();
            
            foreach (var profile in profiles)
            {
                var path = profile.LocalizationPath;

                if (!File.Exists(path))
                {
                    Debug.LogWarning($"Not exist [{path}]");
                    
                    continue;
                }
                
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<Localization>(json);
                
                if (data != null) dates.Add(data);
            }

            return dates;
        }

        public bool SetLocalizationData(Localization data)
        {
            var profiles = Resources.LoadAll<LocalizationProfile>("");

            foreach (var profile in profiles)
            {
                var path = profile.LocalizationPath;

                if (!File.Exists(path)) continue;
                
                var json = File.ReadAllText(path);
                var localization = JsonUtility.FromJson<Localization>(json);
                
                if (localization == null || localization.Name != data.Name) continue;
                
                var text = JsonUtility.ToJson(localization, true);
            
                try
                {
                    var folder = Path.GetDirectoryName(path);
                
                    if (folder != null && !Directory.Exists(folder)) Directory.CreateDirectory(folder);
                
                    using var fs = File.Create(path);
                
                    var info = new UTF8Encoding(true).GetBytes(text);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    return true;
                }

                return true;
            }

            return false;
        }
    }
}