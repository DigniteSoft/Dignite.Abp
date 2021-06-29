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
        public static void ConfigureCustomizeFieldDefinitions<T>(this EntityTypeBuilder<T> b)
            where T : class, ICustomizeFieldDefinition
        {
            b.As<EntityTypeBuilder>().ConfigureCustomizeFieldDefinitions();
        }

        public static void ConfigureCustomizeFieldDefinitions(this EntityTypeBuilder b)
        {
            if (!b.Metadata.ClrType.IsAssignableTo<ICustomizeFieldDefinition>())
            {
                return;
            }

            b.Property<string>(nameof(ICustomizeFieldDefinition.DisplayName)).IsRequired().HasMaxLength(64);
            b.Property<string>(nameof(ICustomizeFieldDefinition.Name)).IsRequired().HasMaxLength(64);
            b.Property<CustomizeFieldFormConfiguration>(nameof(ICustomizeFieldDefinition.FormConfiguration))
                .HasColumnName(nameof(ICustomizeFieldDefinition.FormConfiguration))
                .HasConversion(
                    config => JsonConvert.SerializeObject(config, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                    jsonData => JsonConvert.DeserializeObject<CustomizeFieldFormConfiguration>(jsonData)
                    );
        }

        public static void ConfigureCustomizedFields<T>(this EntityTypeBuilder<T> b)
            where T : class, IHasCustomizedFields
        {
            b.As<EntityTypeBuilder>().TryConfigureCustomizedFields();
        }

        public static void TryConfigureCustomizedFields(this EntityTypeBuilder b)
        {
            if (!b.Metadata.ClrType.IsAssignableTo<IHasCustomizedFields>())
            {
                return;
            }

            b.Property<CustomizedFieldDictionary>(nameof(IHasCustomizedFields.CustomizedFields))
                .HasColumnName(nameof(IHasCustomizedFields.CustomizedFields))
                .HasConversion(new ExtraFieldsValueConverter())
                .Metadata.SetValueComparer(new ExtraFieldDictionaryValueComparer());
        }
    }
}
