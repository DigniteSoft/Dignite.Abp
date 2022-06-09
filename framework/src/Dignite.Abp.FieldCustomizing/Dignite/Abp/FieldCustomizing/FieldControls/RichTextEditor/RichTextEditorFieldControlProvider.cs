

namespace Dignite.Abp.FieldCustomizing.FieldControls.RichTextEditor
{
    public class RichTextEditorFieldControlProvider : FieldControlProviderBase
    {

        public const string ProviderName = "RichTextEditor";

        public override string Name => ProviderName;

        public override string DisplayName => L["RichTextEditorControl"];

        public override FieldControlType ControlType => FieldControlType.Simple;

        public override void Validate(FieldControlValidateArgs args)
        {
            var configuration = new RichTextEditorConfiguration(args.FieldDefinition.Configuration);

            if (configuration.Required && (args.Value == null || args.Value.ToString().Length==0))
            {
                args.ValidationErrors.Add(
                    new System.ComponentModel.DataAnnotations.ValidationResult(
                        L["ValidateValue:Required"], 
                        new[] { args.FieldDefinition.Name }
                        ));
            }
        }

        public override FieldControlConfigurationBase GetConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
        {
            return new RichTextEditorConfiguration(fieldConfiguration);
        }
    }
}
