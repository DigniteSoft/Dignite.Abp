
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FieldCustomizing.TextboxForm
{
    public class TextboxFormProvider : CustomizeFieldFormProviderBase, ITransientDependency
    {

        public const string ProviderName = "TextboxForm";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.TextboxField"].Value;

        public override CustomizeFieldFormType FormType => CustomizeFieldFormType.Simple;

        public override void Validate(CustomizeFieldFormValidateArgs args)
        {
            var configuration = new TextboxFormProviderConfiguration(args.FieldDefinition.FormConfiguration);

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

        public override CustomizeFieldFormProviderConfigurationBase GetConfiguration(CustomizeFieldFormConfiguration fieldConfiguration)
        {
            return new TextboxFormProviderConfiguration(fieldConfiguration);
        }

    }
}
