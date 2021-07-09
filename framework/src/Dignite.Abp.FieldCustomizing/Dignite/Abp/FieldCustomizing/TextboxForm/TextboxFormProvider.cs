

namespace Dignite.Abp.FieldCustomizing.TextboxForm
{
    public class TextboxFormProvider : FormProviderBase
    {

        public const string ProviderName = "TextboxForm";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.TextboxForm"];

        public override FormType FormType => FormType.Simple;

        public override void Validate(FormValidateArgs args)
        {
            var configuration = new TextboxFormConfiguration(args.FieldDefinition.FormConfiguration);

            if (configuration.Required && (args.Value == null || args.Value.ToString().Length==0))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:Required"], 
                        new[] { args.FieldDefinition.Name }
                        ));
            }

            if (args.Value != null && configuration.CharLimit < args.Value.ToString().Length)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["CharacterCountExceedsLimit", args.FieldDefinition.DisplayName, configuration.CharLimit],
                        new[] { args.FieldDefinition.Name }
                        ));
            }

        }

        public override FormConfigurationBase GetConfiguration(FormConfigurationData fieldConfiguration)
        {
            return fieldConfiguration.GetTextboxConfiguration();
        }
    }
}
