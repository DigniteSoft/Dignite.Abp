

namespace Dignite.Abp.FieldCustomizing.FieldControls.Upload
{
    public class UploadFieldControlProvider : FieldControlProviderBase
    {

        public const string ProviderName = "Upload";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.UploadControl"];

        public override FieldControlType ControlType => FieldControlType.Simple;

        public override void Validate(FieldControlValidateArgs args)
        {
        }

        public override FieldControlConfigurationBase GetConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
        {
            return new UploadConfiguration(fieldConfiguration);
        }
    }
}
