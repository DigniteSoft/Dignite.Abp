namespace Dignite.FieldCustomizing.TextboxField
{
    public class TextboxFieldProviderConfiguration:FieldProviderConfigurationBase
    {
        /// <summary>
        /// 标记字段是否为必填
        /// </summary>
        public bool Required
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(TextboxFieldProviderConfigurationNames.Required, false);
            set => _fieldConfiguration.SetConfiguration(TextboxFieldProviderConfigurationNames.Required, value);
        }

        /// <summary>
        /// 字段的填写说明文本
        /// </summary>
        public string Description
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(TextboxFieldProviderConfigurationNames.Description, null);
            set => _fieldConfiguration.SetConfiguration(TextboxFieldProviderConfigurationNames.Description, value);
        }


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



        public TextboxFieldProviderConfiguration(FieldConfiguration fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
