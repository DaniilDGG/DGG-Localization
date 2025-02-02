using System;
using System.Collections.Generic;
using DGGLocalization.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Scripting;

namespace DGGLocalization.Json
{
    [Preserve]
    public class LanguagesConverter : JsonConverter<Language[]>
    {
        [Preserve]
        public override void WriteJson(JsonWriter writer, Language[] languages, JsonSerializer serializer)
        {
            var obj = new JObject();
            foreach (var language in languages) obj[language.LanguageCode] = language.LanguageName;
            
            obj.WriteTo(writer);
        }

        [Preserve]
        public override Language[] ReadJson(JsonReader reader, Type objectType, Language[] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var languages = new List<Language>();

            switch (token.Type)
            {
                case JTokenType.Object:
                {
                    foreach (var property in ((JObject)token).Properties())
                    {
                        languages.Add(new Language(property.Name, property.Value.ToString()));
                    }

                    break;
                }
                case JTokenType.Array:
                {
                    foreach (var langObject in token)
                    {
                        var name = langObject["_languageName"]!.ToString();
                        var code = langObject["_languageCode"]!.ToString();
                        
                        languages.Add(new Language(code, name));
                    }

                    break;
                }
                default: throw new JsonSerializationException("Unexpected token type for _languages.");
            }

            return languages.ToArray();
        }
        
        // ReSharper disable once EmptyConstructor
        // Without this, there may be a post-build error when deserializing JSON.
        [JsonConstructor, Preserve]
        public LanguagesConverter() {}
    }
}