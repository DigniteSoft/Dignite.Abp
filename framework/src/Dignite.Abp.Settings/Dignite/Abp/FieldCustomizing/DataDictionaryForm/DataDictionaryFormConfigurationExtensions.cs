using System;

namespace Dignite.Abp.FieldCustomizing.DataDictionaryForm
{
    public static class DataDictionaryFormConfigurationExtensions
    {
        public static DataDictionaryFormConfiguration GetConfiguration(
            this FormConfigurationData fieldConfiguration)
        {
            return new DataDictionaryFormConfiguration(fieldConfiguration);
        }

        public static FormConfigurationData UseDataDictionaryForm(
            this FormConfigurationData fieldConfiguration,
            Action<DataDictionaryFormConfiguration> configureAction)
        {
            configureAction(new DataDictionaryFormConfiguration(fieldConfiguration));
            return fieldConfiguration;
        }
    }
}
