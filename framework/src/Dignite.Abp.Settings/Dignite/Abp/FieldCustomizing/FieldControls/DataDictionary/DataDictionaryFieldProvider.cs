
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.Localization;
using Dignite.Abp.Settings.Localization;

namespace Dignite.Abp.FieldCustomizing.Fields.DataDictionary
{
    public class DataDictionaryFieldProvider : FieldBase
    {

        public const string ProviderName = "DataDictionary";

        public override string Name => ProviderName;

        public override string DisplayName => L["DataDictionaryControl"]; //TODO:Localization needs to be added to this project when the official version is released

        public override FieldType ControlType => FieldType.Complex;

        public override void Validate(FieldValidateArgs args)
        {
            var configuration = new DataDictionaryConfiguration(args.FieldDefinition.Configuration);

            if (configuration.Required && (args.Value == null))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:Required"], 
                        new[] { args.FieldDefinition.Name }
                        ));
            }

            try
            {
                var dataDictionaries = JsonSerializer.Deserialize<List<DataDictionary>>(args.Value.ToString());                
                if (configuration.Required && !dataDictionaries.Any())
                {
                    args.ValidationErrors.Add(
                        new System.ComponentModel.DataAnnotations.ValidationResult(
                            L["ValidateValue:Required"],
                            new[] { args.FieldDefinition.Name }
                            ));
                }
            }
            catch(Exception e)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:NotDataDictionaries"],
                        new[] { args.FieldDefinition.Name }
                        ));
            }
        }


        public override FieldConfigurationBase GetConfiguration(FieldConfigurationDictionary fieldConfiguration)
        {
            return new DataDictionaryConfiguration(fieldConfiguration);
        }
        protected override IStringLocalizer CreateLocalizer()
        {
            return StringLocalizerFactory.Create(typeof(DigniteAbpSettingsResource));
        }
    }
}
