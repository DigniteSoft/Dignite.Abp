using Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueComparers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Dignite.Abp.FieldCustomizing.EntityFrameworkCore.ValueConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dignite.Abp.FieldCustomizing.EntityFrameworkCore.Modeling
{
    public static class FieldsEntityTypeBuilderExtensions
    {
        public static void ConfigureCustomizableFieldDefinitions<T>(this EntityTypeBuilder<T> b)
            where T : class, ICustomizeFieldDefinition
        {
            b.As<EntityTypeBuilder>().ConfigureCustomizableFieldDefinitions();
        }

        public static void ConfigureCustomizableFieldDefinitions(this EntityTypeBuilder b)
        {
            if (!b.Metadata.ClrType.IsAssignableTo<ICustomizeFieldDefinition>())
            {
                return;
            }

            b.Property<string>(nameof(ICustomizeFieldDefinition.DisplayName)).IsRequired().HasMaxLength(64);
            b.Property<string>(nameof(ICustomizeFieldDefinition.Name)).IsRequired().HasMaxLength(64);
            b.Property<FormConfigurationData>(nameof(ICustomizeFieldDefinition.FormConfiguration))
                .HasColumnName(nameof(ICustomizeFieldDefinition.FormConfiguration))
                .HasConversion(
                    config => JsonConvert.SerializeObject(config, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                    jsonData => JsonConvert.DeserializeObject<FormConfigurationData>(jsonData)
                    );
        }

        public static void ConfigureObjectCustomizedFields<T>(this EntityTypeBuilder<T> b)
            where T : class, IHasCustomizableFields
        {
            b.As<EntityTypeBuilder>().TryConfigureObjectCustomizedFields();
        }

        public static void TryConfigureObjectCustomizedFields(this EntityTypeBuilder b)
        {
            if (!b.Metadata.ClrType.IsAssignableTo<IHasCustomizableFields>())
            {
                return;
            }

            b.Property<CustomizeFieldDictionary>(nameof(IHasCustomizableFields.CustomizedFields))
                .HasColumnName(nameof(IHasCustomizableFields.CustomizedFields))
                .HasConversion(new CustomizedFieldsValueConverter())
                .Metadata.SetValueComparer(new CustomizedFieldDictionaryValueComparer());
        }
    }
}
