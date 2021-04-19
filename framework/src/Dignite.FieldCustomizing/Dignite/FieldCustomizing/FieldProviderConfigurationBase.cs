

namespace Dignite.FieldCustomizing
{
    public abstract class FieldProviderConfigurationBase
    {
        protected readonly FieldConfiguration _fieldConfiguration;
        public FieldProviderConfigurationBase(FieldConfiguration fieldConfiguration)
        {
            _fieldConfiguration = fieldConfiguration;
        }
    }
}
