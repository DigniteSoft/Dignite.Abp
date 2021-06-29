using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueConverters
{
    public class ExtraFieldsValueConverter : ValueConverter<CustomizedFieldDictionary, string>
    {
        public ExtraFieldsValueConverter()
            : base(
                d => SerializeObject(d),
                s => DeserializeObject(s))
        {

        }

        private static string SerializeObject(CustomizedFieldDictionary extraFields)
        {
            var copyDictionary = new Dictionary<string, object>(extraFields);

            return JsonSerializer.Serialize(copyDictionary);
        }

        private static CustomizedFieldDictionary DeserializeObject(string extraFieldsAsJson)
        {
            if (extraFieldsAsJson.IsNullOrEmpty() || extraFieldsAsJson == "{}")
            {
                return new CustomizedFieldDictionary();
            }

            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());
            var dictionary = JsonSerializer.Deserialize<CustomizedFieldDictionary>(extraFieldsAsJson, deserializeOptions) ??
                             new CustomizedFieldDictionary();

            return dictionary;
        }

    }
}
