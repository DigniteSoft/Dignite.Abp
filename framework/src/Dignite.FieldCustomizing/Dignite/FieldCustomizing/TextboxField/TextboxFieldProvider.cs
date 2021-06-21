
namespace Dignite.FieldCustomizing.TextboxField
{
    public class TextboxFieldProvider : CustomizeFieldProviderBase
    {

        public const string ProviderName = "TextboxField";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.TextboxField"].Value;

        public override CustomizeFieldType FieldType => CustomizeFieldType.Simple;

        public override void Validate(CustomizeFieldProviderValidateArgs args)
        {
            var configuration = new TextboxFieldProviderConfiguration(args.FieldDefinition.Configuration);

            if (configuration.Required && (args.Value == null || args.Value.ToString().Length==0))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:Required"].Value, 
                        new[] { args.FieldDefinition.Name }
                        ));
            }

            if (args.Value != null && configuration.CharLimit < args.Value.ToString().Length)
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["{0} 字符限制不超 {1} 个字符",args.FieldDefinition.DisplayName, configuration.CharLimit].Value,
                        new[] { args.FieldDefinition.Name }
                        ));
            }

        }

    }
}
