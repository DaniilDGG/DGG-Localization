//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DGGLocalization.Config;
using DGGLocalization.Data;
using DGGLocalization.Loaders;
using Newtonsoft.Json;
using UnityEngine;

namespace DGGLocalization
{
    [LocalizationLoader]
    public class ProfileLoader : ILocalizationLoader
    {
        #region Fields

        private readonly Dictionary<Guid, LocalizationProfile> _cached = new();

        #endregion
        
        public List<(Localization, string)> GetLocalizationDates()
        {
            var profiles = Resources.LoadAll<LocalizationProfile>("");

            var dates = new List<(Localization, string)>();
            
            foreach (var profile in profiles)
            {
                var path = profile.LocalizationPath;

                if (!File.Exists(path))
                {
                    Debug.LogWarning($"Not exist [{path}]");
                    
                    var localization = new Localization();
                    
                    localization.SetLocalization(new Language[]{new ("EN", "english")});
                    
                    dates.Add((localization, $"[P]{profile.name}"));
                    _cached[localization.GUID] = profile;
                    
                    WriteLocalizationData(localization, path);
                    
                    continue;
                }
                
                var json = File.ReadAllText(path);
                var data = JsonConvert.DeserializeObject<Localization>(json);

                if (data == null) continue;
                
                dates.Add((data, $"[P]{profile.name}"));
                _cached[data.GUID] = profile;
            }

            return dates;
        }

        public bool SetLocalizationData(Localization data)
        {
            var profiles = Resources.LoadAll<LocalizationProfile>("");

            if (FindInCache(data.GUID, out var cachedProfile))
            {
                WriteLocalizationData(data, cachedProfile.LocalizationPath);

                return true;
            }
            
            foreach (var profile in profiles)
            {
                var path = profile.LocalizationPath;

                if (!File.Exists(path)) continue;
                
                var json = File.ReadAllText(path);
                var localization = JsonConvert.DeserializeObject<Localization>(json);

                if (localization == null) continue;
                
                _cached[localization.GUID] = profile;
                
                if (localization.GUID != data.GUID) continue;

                WriteLocalizationData(data, path);

                return true;
            }

            return false;
        }

        private bool FindInCache(Guid guid, out LocalizationProfile profile)
        {
            if (!_cached.TryGetValue(guid, out profile)) return false;
            
            if (profile == null) return false;

            var path = profile.LocalizationPath;

            if (!File.Exists(path))
            {
                _cached[guid] = null;
                    
                return false;
            }
                
            var json = File.ReadAllText(path);
            var localization = JsonConvert.DeserializeObject<Localization>(json);

            if (localization.GUID == guid) return true;
                
            _cached[guid] = null;
            _cached[localization.GUID] = profile;
                    
            return false;
        }
        
        private static void WriteLocalizationData(Localization data, string path)
        {
            var text = JsonConvert.SerializeObject(data, Formatting.Indented);
            
            try
            {
                var folder = Path.GetDirectoryName(path);
                
                if (folder != null && !Directory.Exists(folder)) Directory.CreateDirectory(folder);
                
                using var fs = File.Create(path);
                
                var info = new UTF8Encoding(true).GetBytes(text);
                fs.Write(info, 0, info.Length);
                fs.Close();
                
                Debug.Log($"Localization {data.GUID} has been successfully written to {path}");
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}