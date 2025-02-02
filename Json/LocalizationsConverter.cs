using System;
using System.Collections.Generic;
using DGGLocalization.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Scripting;

namespace DGGLocalization.Json
{
    [Preserve]
    public class LocalizationsConverter : JsonConverter<List<LocalizationData>>
    {
        [Preserve]
        public override void WriteJson(JsonWriter writer, List<LocalizationData> value, JsonSerializer serializer)
        {
            var obj = new JObject();
            foreach (var locData in value)
            {
                var dataObj = new JObject();
                foreach (var langData in locData.Data)
                {
                    var languageCode = langData.Language.LanguageCode;
                    dataObj[languageCode] = langData.Localization;
                }
                obj[locData.LocalizationCode] = dataObj;
            }
            obj.WriteTo(writer);
        }

        [Preserve]
        public override List<LocalizationData> ReadJson(JsonReader reader, Type objectType, List<LocalizationData> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var localizations = new List<LocalizationData>();

            switch (token.Type)
            {
                case JTokenType.Array:
                {
                    foreach (var item in token)
                    {
                        var languageDates = new List<LanguageData>();

                        foreach (var langEntry in item["_data"]!)
                        {
                            var language = langEntry["_language"]!.ToObject<LanguageShort>(serializer);
                            var localization = langEntry["_localization"]!.ToString();
                            var langData = new LanguageData(language, localization);
                        
                            languageDates.Add(langData);
                        }

                        var key = item["_localizationCode"]!.ToString();
                        var locData = new LocalizationData(key, languageDates);
                    
                        localizations.Add(locData);
                    }

                    break;
                }
                case JTokenType.Object:
                {
                    foreach (var prop in ((JObject)token).Properties())
                    {
                        var languageDates = new List<LanguageData>();
                    
                        foreach (var langProp in ((JObject)prop.Value).Properties())
                        {
                            var langData = new LanguageData(new LanguageShort(langProp.Name), langProp.Value.ToString());
                            languageDates.Add(langData);
                        }

                        var locData = new LocalizationData(prop.Name, languageDates);
                        localizations.Add(locData);
                    }

                    break;
                }
                default:
                    throw new JsonSerializationException("Unexpected token type for _localizations.");
            }

            return localizations;
        }
        
        // ReSharper disable once EmptyConstructor
        // Without this, there may be a post-build error when deserializing JSON.
        [JsonConstructor, Preserve]
        public LocalizationsConverter() {}
    }
}