using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueConverters
{
    public class CustomizedFieldsValueConverter : ValueConverter<CustomizeFieldDictionary, string>
    {
        public CustomizedFieldsValueConverter()
            : base(
                d => SerializeObject(d),
                s => DeserializeObject(s))
        {

        }

        private static string SerializeObject(CustomizeFieldDictionary extraFields)
        {
            var copyDictionary = new Dictionary<string, object>(extraFields);

            return JsonSerializer.Serialize(copyDictionary);
        }

        private static CustomizeFieldDictionary DeserializeObject(string extraFieldsAsJson)
        {
            if (extraFieldsAsJson.IsNullOrEmpty() || extraFieldsAsJson == "{}")
            {
                return new CustomizeFieldDictionary();
            }

            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());
            var dictionary = JsonSerializer.Deserialize<CustomizeFieldDictionary>(extraFieldsAsJson, deserializeOptions) ??
                             new CustomizeFieldDictionary();

            return dictionary;
        }

    }
}
