

using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields
{
    public abstract class FieldConfigurationBase
    {
        protected readonly FieldConfigurationDictionary _fieldConfiguration;

        public bool Required
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(FieldConfigurationNames.Required, false);
            set => _fieldConfiguration.SetConfiguration(FieldConfigurationNames.Required, value);
        }

        [StringLength(256)]
        public string Description
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(FieldConfigurationNames.Description, null);
            set => _fieldConfiguration.SetConfiguration(FieldConfigurationNames.Description, value);
        }

        public FieldConfigurationDictionary GetConfiguration()
        {
            return _fieldConfiguration;
        }

        public FieldConfigurationBase(
            FieldConfigurationDictionary fieldConfiguration)
        {
            _fieldConfiguration = fieldConfiguration;
        }
    }
}
