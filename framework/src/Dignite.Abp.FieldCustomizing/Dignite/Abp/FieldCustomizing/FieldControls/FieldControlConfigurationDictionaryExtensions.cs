﻿using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Dignite.Abp.FieldCustomizing.FieldControls
{
    public static class FieldControlConfigurationDictionaryExtensions
    {
        public static bool HasConfiguration(this FieldControlConfigurationDictionary source, string name)
        {
            return source.ContainsKey(name);
        }

        public static TConfiguration GetConfigurationOrDefault<TConfiguration>(this FieldControlConfigurationDictionary source, string name, TConfiguration defaultValue = default)
        {
            if (!source.HasConfiguration(name))
            {
                return defaultValue;
            }
            var configurationAsJson = source[name];
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<TConfiguration>(configurationAsJson,options);
        }

        public static void SetConfiguration<TConfiguration>(
            this FieldControlConfigurationDictionary source,
            string name,
            TConfiguration value)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            var configurationAsJson=JsonSerializer.Serialize(value, options);
            source[name]=configurationAsJson;
        }

        public static void RemoveConfiguration(this FieldControlConfigurationDictionary source, string name)
        {
            source.Remove(name);
        }
    }
}
