

namespace Dignite.Abp.FieldCustomizing.FieldControls.Table
{
    public class TableFieldControlProvider : FieldControlProviderBase
    {

        public const string ProviderName = "Table";

        public override string Name => ProviderName;

        public override string DisplayName => L["TableControl"];

        public override FieldControlType ControlType => FieldControlType.Complex;

        public override void Validate(FieldControlValidateArgs args)
        {
        }

        public override FieldControlConfigurationBase GetConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
        {
            return new TableConfiguration(fieldConfiguration);
        }
    }
}
