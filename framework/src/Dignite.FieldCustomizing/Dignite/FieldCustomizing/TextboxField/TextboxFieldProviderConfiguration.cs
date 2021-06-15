namespace Dignite.FieldCustomizing.TextboxField
{
    public class TextboxFieldProviderConfiguration:CustomizeFieldProviderConfigurationBase
    {

        /// <summary>
        /// 占位符
        /// </summary>
        public string Placeholder
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(TextboxFieldProviderConfigurationNames.Placeholder, null);
            set => _fieldConfiguration.SetConfiguration(TextboxFieldProviderConfigurationNames.Placeholder, value);
        }

        public TextboxMode Mode
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<TextboxMode>(TextboxFieldProviderConfigurationNames.Mode, TextboxMode.SingleLineText);
            set => _fieldConfiguration.SetConfiguration(TextboxFieldProviderConfigurationNames.Mode, value);
        }


        public int CharLimit
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(TextboxFieldProviderConfigurationNames.CharLimit, Mode== TextboxMode.SingleLineText?256:1024);
            set => _fieldConfiguration.SetConfiguration(TextboxFieldProviderConfigurationNames.CharLimit, value);
        }

        /// <summary>
        /// 是否启用自动完成。
        /// </summary>
        public bool Autocomplete
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(TextboxFieldProviderConfigurationNames.Autocomplete, false);
            set => _fieldConfiguration.SetConfiguration(TextboxFieldProviderConfigurationNames.Autocomplete, value);
        }



        public TextboxFieldProviderConfiguration(CustomizeFieldConfiguration fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
