

namespace Dignite.Abp.FieldCustomizing
{
    public abstract class FormProviderConfigurationBase
    {
        protected readonly CustomizeFieldFormConfiguration _fieldFormConfiguration;

        /// <summary>
        /// 标记字段是否为必填
        /// </summary>
        public bool Required
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault(FormProviderConfigurationNames.Required, false);
            set => _fieldFormConfiguration.SetConfiguration(FormProviderConfigurationNames.Required, value);
        }

        /// <summary>
        /// 字段的填写说明文本
        /// </summary>
        public string Description
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<string>(FormProviderConfigurationNames.Description, null);
            set => _fieldFormConfiguration.SetConfiguration(FormProviderConfigurationNames.Description, value);
        }

        public FormProviderConfigurationBase(
            CustomizeFieldFormConfiguration fieldFormConfiguration,
            string fieldFormProviderName)
        {
            _fieldFormConfiguration = fieldFormConfiguration;
            _fieldFormConfiguration.FormProviderName = fieldFormProviderName;
        }
    }
}
