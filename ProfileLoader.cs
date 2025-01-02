using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DGGLocalization.Config;
using DGGLocalization.Data;
using DGGLocalization.Loaders;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace DGGLocalization
{
    [LocalizationLoader]
    public class ProfileLoader : ILocalizationLoader
    {
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
                    
                    WriteLocalizationData(localization, path);
                    
                    continue;
                }
                
                var json = File.ReadAllText(path);
                var data = JsonConvert.DeserializeObject<Localization>(json);
                
                if (data != null) dates.Add((data, $"[P]{profile.name}"));
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
                var localization = JsonConvert.DeserializeObject<Localization>(json);
                
                if (localization == null || localization.GUID != data.GUID) continue;

                WriteLocalizationData(data, path);

                return true;
            }

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