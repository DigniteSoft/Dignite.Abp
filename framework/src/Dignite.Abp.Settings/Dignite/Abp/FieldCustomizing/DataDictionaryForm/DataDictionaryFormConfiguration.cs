namespace Dignite.Abp.FieldCustomizing.DataDictionaryForm
{
    public class DataDictionaryFormConfiguration:FormConfigurationBase
    {

        /// <summary>
        /// Maximum depth of an DataDictionary hierarchy.
        /// </summary>
        public int MaxDepth
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<int>(DataDictionaryFormConfigurationNames.MaxDepth, 1);
            set => _fieldFormConfiguration.SetConfiguration(DataDictionaryFormConfigurationNames.MaxDepth, value);
        }


        public DataDictionaryFormConfiguration(FormConfigurationData fieldConfiguration)
            :base(fieldConfiguration, DataDictionaryFormProvider.ProviderName)
        {
        }
    }
}
