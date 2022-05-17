
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.Abp.FieldCustomizing.FieldControls.DataDictionary
{
    public class DataDictionaryFieldControlProvider : FieldControlProviderBase
    {

        public const string ProviderName = "DataDictionary";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.DataDictionary"]; //TODO:Localization needs to be added to this project when the official version is released

        public override FieldControlType ControlType => FieldControlType.Simple;

        public override void Validate(FieldControlValidateArgs args)
        {
            var configuration = new DataDictionaryConfiguration(args.FieldDefinition.Configuration);

            if (configuration.Required && (args.Value == null || args.Value.ToString().Length==0))
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
            catch
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:NotDataDictionaries"],
                        new[] { args.FieldDefinition.Name }
                        ));
            }
        }


        public override FieldControlConfigurationBase GetConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
        {
            return new DataDictionaryConfiguration(fieldConfiguration);
        }
    }
}
