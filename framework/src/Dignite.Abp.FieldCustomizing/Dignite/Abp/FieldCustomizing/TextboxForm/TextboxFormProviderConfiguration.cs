namespace Dignite.Abp.FieldCustomizing.TextboxForm
{
    public class TextboxFormProviderConfiguration:CustomizeFieldFormProviderConfigurationBase
    {

        /// <summary>
        /// 占位符
        /// </summary>
        public string Placeholder
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<string>(TextboxFormProviderConfigurationNames.Placeholder, null);
            set => _fieldFormConfiguration.SetConfiguration(TextboxFormProviderConfigurationNames.Placeholder, value);
        }

        public TextboxFormMode Mode
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<TextboxFormMode>(TextboxFormProviderConfigurationNames.Mode, TextboxFormMode.SingleLineText);
            set => _fieldFormConfiguration.SetConfiguration(TextboxFormProviderConfigurationNames.Mode, value);
        }


        public int CharLimit
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault(TextboxFormProviderConfigurationNames.CharLimit, Mode== TextboxFormMode.SingleLineText?256:1024);
            set => _fieldFormConfiguration.SetConfiguration(TextboxFormProviderConfigurationNames.CharLimit, value);
        }

        /// <summary>
        /// 是否启用自动完成。
        /// </summary>
        public bool Autocomplete
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault(TextboxFormProviderConfigurationNames.Autocomplete, false);
            set => _fieldFormConfiguration.SetConfiguration(TextboxFormProviderConfigurationNames.Autocomplete, value);
        }



        public TextboxFormProviderConfiguration(CustomizeFieldFormConfiguration fieldConfiguration)
            :base(fieldConfiguration, TextboxFormProvider.ProviderName)
        {
        }
    }
}
