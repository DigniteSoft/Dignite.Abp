using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Dignite.FieldCustomizing.EntityFrameworkCore.ValueConverters
{
    public class ExtraFieldsValueConverter : ValueConverter<ExtraFieldDictionary, string>
    {
        public ExtraFieldsValueConverter()
            : base(
                d => SerializeObject(d),
                s => DeserializeObject(s))
        {

        }

        private static string SerializeObject(ExtraFieldDictionary extraFields)
        {
            var copyDictionary = new Dictionary<string, object>(extraFields);

            return JsonSerializer.Serialize(copyDictionary);
        }

        private static ExtraFieldDictionary DeserializeObject(string extraFieldsAsJson)
        {
            if (extraFieldsAsJson.IsNullOrEmpty() || extraFieldsAsJson == "{}")
            {
                return new ExtraFieldDictionary();
            }

            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());
            var dictionary = JsonSerializer.Deserialize<ExtraFieldDictionary>(extraFieldsAsJson, deserializeOptions) ??
                             new ExtraFieldDictionary();

            return dictionary;
        }

    }
}
