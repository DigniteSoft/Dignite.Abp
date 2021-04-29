
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Volo.Abp.Validation;

namespace Dignite.FieldCustomizing.TextboxField
{
    public class TextboxFieldProvider : FieldProviderBase
    {
        public const string ProviderName = "TextboxField";

        public override string Name => ProviderName;

        public override ILocalizableString DisplayName => L("DisplayName:Dignite.TextboxField");

        public override FieldProviderType Type => FieldProviderType.Simple;

        public override Task<FieldValueValidateResult> ValidateAsync(FieldProviderValidateValueArgs args)
        {
            var validateResult = new FieldValueValidateResult();
            var configuration = new TextboxFieldProviderConfiguration(args.Configuration);

            if (args.Value is not string)
            {
                //TODO...
                throw new AbpValidationException($"必须是字符类型");
            }

            if (configuration.Required && (args.Value == null || args.Value.ToString().Length==0))
            {
                //TODO
                //validateResult.Succeeded = false;
                validateResult.Errors.Add(new FieldValueValidateError(
                    "ValidateValue:Required",
                    L("ValidateValue:Required")
                    ));
            }

            if (args.Value != null && configuration.CharLimit < args.Value.ToString().Length)
            {
                //TODO...
                throw new AbpValidationException($"{args.FieldName} 字符数不能超过 {configuration.CharLimit} 个！");
            }

            return Task.FromResult( validateResult);
        }

        public override FieldProviderConfigurationBase GetConfiguration(FieldConfiguration fieldConfiguration)
        {
            return new TextboxFieldProviderConfiguration(fieldConfiguration);
        }
    }
}
