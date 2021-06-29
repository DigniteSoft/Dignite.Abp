using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Reflection;

namespace Dignite.Abp.FieldCustomizing
{
    public static class HasCustomizedFieldsExtensions
    {
        public static bool HasField(this IHasCustomizedFields source, string name)
        {
            return source.CustomizedFields.ContainsKey(name);
        }

        public static object GetField(this IHasCustomizedFields source, string name, object defaultValue = null)
        {
            return source.CustomizedFields?.GetOrDefault(name)
                   ?? defaultValue;
        }

        public static TField GetField<TField>(this IHasCustomizedFields source, string name, TField defaultValue = default)
        {
            var value = source.GetField(name);
            if (value == null)
            {
                return defaultValue;
            }

            if (TypeHelper.IsPrimitiveExtended(typeof(TField), includeEnums: true))
            {
                var conversionType = typeof(TField);
                if (TypeHelper.IsNullable(conversionType))
                {
                    conversionType = conversionType.GetFirstGenericArgumentIfNullable();
                }

                if (conversionType == typeof(Guid))
                {
                    return (TField)TypeDescriptor.GetConverter(conversionType).ConvertFromInvariantString(value.ToString());
                }

                return (TField)Convert.ChangeType(value, conversionType, CultureInfo.InvariantCulture);
            }

            throw new AbpException("GetField<TField> does not support non-primitive types. Use non-generic GetField method and handle type casting manually.");
        }

        public static TSource SetField<TSource>(
            this TSource source,
            string name,
            object value)
            where TSource : IHasCustomizedFields
        {
            source.CustomizedFields[name] = value;

            return source;
        }

        public static TSource RemoveField<TSource>(this TSource source, string name)
            where TSource : IHasCustomizedFields
        {
            source.CustomizedFields.Remove(name);
            return source;
        }

        public static TSource SetDefaultsForExtraFields<TSource>(this TSource source, IReadOnlyList<BasicCustomizeFieldDefinition> fieldDefinitions)
            where TSource : IHasCustomizedFields
        {
            foreach (var fieldDefinition in fieldDefinitions)
            {
                if (source.HasField(fieldDefinition.Name))
                {
                    continue;
                }

                source.CustomizedFields[fieldDefinition.Name] = fieldDefinition.DefaultValue;
            }

            return source;
        }

        public static void SetDefaultsForExtraFields(object source, IReadOnlyList<BasicCustomizeFieldDefinition> fieldDefinitions)
        {
            if (!(source is IHasCustomizedFields))
            {
                throw new ArgumentException($"Given {nameof(source)} object does not implement the {nameof(IHasCustomizedFields)} interface!", nameof(source));
            }

            ((IHasCustomizedFields)source).SetDefaultsForExtraFields(fieldDefinitions);
        }

        public static void SetCustomizableFieldsToRegularProperties(this IHasCustomizedFields source)
        {
            var properties = source.GetType().GetProperties()
                .Where(x => source.CustomizedFields.ContainsKey(x.Name)
                            && x.GetSetMethod(true) != null)
                .ToList();

            foreach (var property in properties)
            {
                property.SetValue(source, source.CustomizedFields[property.Name]);
                source.RemoveField(property.Name);
            }
        }
    }
}
