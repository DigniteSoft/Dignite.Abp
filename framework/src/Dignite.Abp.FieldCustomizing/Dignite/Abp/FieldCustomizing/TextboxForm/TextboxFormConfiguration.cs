namespace Dignite.Abp.FieldCustomizing.TextboxForm
{
    public class TextboxFormConfiguration:FormConfigurationBase
    {

        /// <summary>
        /// 占位符
        /// </summary>
        public string Placeholder
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<string>(TextboxFormConfigurationNames.Placeholder, null);
            set => _fieldFormConfiguration.SetConfiguration(TextboxFormConfigurationNames.Placeholder, value);
        }

        public TextboxFormMode Mode
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<TextboxFormMode>(TextboxFormConfigurationNames.Mode, TextboxFormMode.SingleLineText);
            set => _fieldFormConfiguration.SetConfiguration(TextboxFormConfigurationNames.Mode, value);
        }


        public int CharLimit
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault(TextboxFormConfigurationNames.CharLimit, Mode== TextboxFormMode.SingleLineText?256:1024);
            set => _fieldFormConfiguration.SetConfiguration(TextboxFormConfigurationNames.CharLimit, value);
        }

        /// <summary>
        /// 是否启用自动完成。
        /// </summary>
        public bool Autocomplete
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault(TextboxFormConfigurationNames.Autocomplete, false);
            set => _fieldFormConfiguration.SetConfiguration(TextboxFormConfigurationNames.Autocomplete, value);
        }



        public TextboxFormConfiguration(CustomizeFieldFormConfiguration fieldConfiguration)
            :base(fieldConfiguration, TextboxFormProvider.ProviderName)
        {
        }
    }
}
