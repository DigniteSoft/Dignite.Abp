

namespace Dignite.Abp.FieldCustomizing.FieldControls.Textbox
{
    public class TextboxFieldControlProvider : FieldControlProviderBase
    {

        public const string ProviderName = "Textbox";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.TextboxControl"];

        public override FieldControlType ControlType => FieldControlType.Simple;

        public override void Validate(FieldControlValidateArgs args)
        {
            var configuration = new TextboxConfiguration(args.FieldDefinition.Configuration);

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

        public override FieldControlConfigurationBase GetConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
        {
            return new TextboxConfiguration(fieldConfiguration);
        }
    }
}
