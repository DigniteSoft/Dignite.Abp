
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.Abp.FieldCustomizing.DataDictionaryForm
{
    public class DataDictionaryFormProvider : FormProviderBase
    {

        public const string ProviderName = "DataDictionaryForm";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.DataDictionaryForm"]; //TODO:Localization needs to be added to this project when the official version is released

        public override FormType FormType => FormType.Simple;

        public override void Validate(FormValidateArgs args)
        {
            var configuration = new DataDictionaryFormConfiguration(args.FieldDefinition.FormConfiguration);

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

        public override FormConfigurationBase GetConfiguration(FormConfigurationData fieldConfiguration)
        {
            return fieldConfiguration.GetConfiguration();
        }
    }
}
