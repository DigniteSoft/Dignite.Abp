using Dignite.FieldCustomizing.EntityFrameworkCore.ValueComparers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Dignite.FieldCustomizing.EntityFrameworkCore.ValueConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dignite.FieldCustomizing.EntityFrameworkCore.Modeling
{
    public static class FieldsEntityTypeBuilderExtensions
    {
        public static void ConfigureCustomizeFieldDefinitions<T>(this EntityTypeBuilder<T> b)
            where T : class, IHasExtraFields
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
            b.Property<CustomizeFieldConfiguration>(nameof(ICustomizeFieldDefinition.Configuration))
                .HasColumnName(nameof(ICustomizeFieldDefinition.Configuration))
                .HasConversion(
                    config => JsonConvert.SerializeObject(config, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                    jsonData => JsonConvert.DeserializeObject<CustomizeFieldConfiguration>(jsonData)
                    );
        }

        public static void ConfigureExtraProperties<T>(this EntityTypeBuilder<T> b)
            where T : class, IHasExtraFields
        {
            b.As<EntityTypeBuilder>().TryConfigureExtraProperties();
        }

        public static void TryConfigureExtraProperties(this EntityTypeBuilder b)
        {
            if (!b.Metadata.ClrType.IsAssignableTo<IHasExtraFields>())
            {
                return;
            }

            b.Property<ExtraFieldDictionary>(nameof(IHasExtraFields.ExtraFields))
                .HasColumnName(nameof(IHasExtraFields.ExtraFields))
                .HasConversion(new ExtraFieldsValueConverter())
                .Metadata.SetValueComparer(new ExtraFieldDictionaryValueComparer());
        }
    }
}
