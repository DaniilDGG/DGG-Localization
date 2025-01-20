//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using DGGLocalization.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DGGLocalization.Json
{
    public class LanguageConverter : JsonConverter<LanguageShort>
    {
        public override void WriteJson(JsonWriter writer, LanguageShort value, JsonSerializer serializer) => writer.WriteValue(value.LanguageCode);

        public override LanguageShort ReadJson(JsonReader reader, Type objectType, LanguageShort existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                {
                    var languageCode = reader.Value?.ToString();
                    return new LanguageShort(languageCode);
                }
                case JsonToken.StartObject:
                {
                    var jsonObject = JObject.Load(reader);
                    var languageCode = jsonObject["_languageCode"]?.ToString();
                    if (languageCode == null)
                    {
                        throw new JsonSerializationException("Missing '_languageCode' property in JSON object.");
                    }
                    return new LanguageShort(languageCode);
                }
                default:
                    throw new JsonSerializationException("Unexpected token type for LanguageShort.");
            }
        }
        
        // ReSharper disable once EmptyConstructor
        // Without this, there may be a post-build error when deserializing JSON.
        [JsonConstructor]
        public LanguageConverter() {}
    }
}
