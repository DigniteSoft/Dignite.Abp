

namespace Dignite.Abp.FieldCustomizing
{
    public abstract class FormConfigurationBase
    {
        protected readonly FormConfigurationData _fieldFormConfiguration;

        /// <summary>
        /// 标记字段是否为必填
        /// </summary>
        public bool Required
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault(FormConfigurationNames.Required, false);
            set => _fieldFormConfiguration.SetConfiguration(FormConfigurationNames.Required, value);
        }

        /// <summary>
        /// 字段的填写说明文本
        /// </summary>
        public string Description
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<string>(FormConfigurationNames.Description, null);
            set => _fieldFormConfiguration.SetConfiguration(FormConfigurationNames.Description, value);
        }

        public FormConfigurationBase(
            FormConfigurationData fieldFormConfiguration,
            string fieldFormProviderName)
        {
            _fieldFormConfiguration = fieldFormConfiguration;
            _fieldFormConfiguration.FormProviderName = fieldFormProviderName;
        }
    }
}
