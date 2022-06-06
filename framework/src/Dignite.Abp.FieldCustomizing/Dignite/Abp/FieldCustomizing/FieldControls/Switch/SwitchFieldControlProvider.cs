

namespace Dignite.Abp.FieldCustomizing.FieldControls.Switch
{
    public class SwitchFieldControlProvider : FieldControlProviderBase
    {

        public const string ProviderName = "Switch";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.SwitchControl"];

        public override FieldControlType ControlType => FieldControlType.Simple;

        public override void Validate(FieldControlValidateArgs args)
        {
        }

        public override FieldControlConfigurationBase GetConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
        {
            return new SwitchConfiguration(fieldConfiguration);
        }
    }
}
