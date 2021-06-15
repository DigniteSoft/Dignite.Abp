

namespace Dignite.FieldCustomizing
{
    public class CustomizeFieldProviderConfigurationBase
    {
        protected readonly CustomizeFieldConfiguration _fieldConfiguration;

        /// <summary>
        /// 标记字段是否为必填
        /// </summary>
        public bool Required
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(CustomizeFieldProviderConfigurationNames.Required, false);
            set => _fieldConfiguration.SetConfiguration(CustomizeFieldProviderConfigurationNames.Required, value);
        }

        /// <summary>
        /// 字段的填写说明文本
        /// </summary>
        public string Description
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(CustomizeFieldProviderConfigurationNames.Description, null);
            set => _fieldConfiguration.SetConfiguration(CustomizeFieldProviderConfigurationNames.Description, value);
        }

        public CustomizeFieldProviderConfigurationBase(CustomizeFieldConfiguration fieldConfiguration)
        {
            _fieldConfiguration = fieldConfiguration;
        }
    }
}
