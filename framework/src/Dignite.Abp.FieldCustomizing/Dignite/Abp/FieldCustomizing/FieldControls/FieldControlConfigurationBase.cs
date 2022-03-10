

using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.FieldControls
{
    public abstract class FieldControlConfigurationBase
    {
        protected readonly FieldControlConfigurationDictionary _fieldControlConfiguration;

        public bool Required
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(FieldControlConfigurationNames.Required, false);
            set => _fieldControlConfiguration.SetConfiguration(FieldControlConfigurationNames.Required, value);
        }

        [StringLength(256)]
        public string Description
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<string>(FieldControlConfigurationNames.Description, null);
            set => _fieldControlConfiguration.SetConfiguration(FieldControlConfigurationNames.Description, value);
        }

        public FieldControlConfigurationDictionary GetConfiguration()
        {
            return _fieldControlConfiguration;
        }

        public FieldControlConfigurationBase(
            FieldControlConfigurationDictionary fieldConfiguration)
        {
            _fieldControlConfiguration = fieldConfiguration;
        }
    }
}
