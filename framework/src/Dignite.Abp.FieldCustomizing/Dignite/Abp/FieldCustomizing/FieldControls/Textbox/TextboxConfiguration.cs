namespace Dignite.Abp.FieldCustomizing.FieldControls.Textbox
{
    public class TextboxConfiguration:FieldControlConfigurationBase
    {
        public string Placeholder
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<string>(TextboxConfigurationNames.Placeholder, null);
            set => _fieldControlConfiguration.SetConfiguration(TextboxConfigurationNames.Placeholder, value);
        }

        public TextboxMode Mode
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(TextboxConfigurationNames.Mode, TextboxMode.SingleLine);
            set => _fieldControlConfiguration.SetConfiguration(TextboxConfigurationNames.Mode, value);
        }


        public int CharLimit
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(TextboxConfigurationNames.CharLimit, Mode== TextboxMode.SingleLine?256:1024);
            set => _fieldControlConfiguration.SetConfiguration(TextboxConfigurationNames.CharLimit, value);
        }


        public TextboxConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
