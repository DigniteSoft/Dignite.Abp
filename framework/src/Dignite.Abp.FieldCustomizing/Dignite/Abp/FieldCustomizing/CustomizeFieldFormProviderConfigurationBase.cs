

namespace Dignite.Abp.FieldCustomizing
{
    public abstract class CustomizeFieldFormProviderConfigurationBase
    {
        protected readonly CustomizeFieldFormConfiguration _fieldFormConfiguration;

        /// <summary>
        /// 标记字段是否为必填
        /// </summary>
        public bool Required
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault(CustomizeFieldFormProviderConfigurationNames.Required, false);
            set => _fieldFormConfiguration.SetConfiguration(CustomizeFieldFormProviderConfigurationNames.Required, value);
        }

        /// <summary>
        /// 字段的填写说明文本
        /// </summary>
        public string Description
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<string>(CustomizeFieldFormProviderConfigurationNames.Description, null);
            set => _fieldFormConfiguration.SetConfiguration(CustomizeFieldFormProviderConfigurationNames.Description, value);
        }

        public CustomizeFieldFormProviderConfigurationBase(
            CustomizeFieldFormConfiguration fieldFormConfiguration,
            string fieldFormProviderName)
        {
            _fieldFormConfiguration = fieldFormConfiguration;
            _fieldFormConfiguration.FormProviderName = fieldFormProviderName;
        }
    }
}
