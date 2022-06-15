
namespace Dignite.Abp.FieldCustomizing.Fields.DataDictionary
{
    public class DataDictionaryConfiguration:FieldConfigurationBase
    {

        /// <summary>
        /// Maximum depth of an DataDictionary hierarchy.
        /// </summary>
        public int MaxDepth
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<int>(DataDictionaryConfigurationNames.MaxDepth, 1);
            set => _fieldConfiguration.SetConfiguration(DataDictionaryConfigurationNames.MaxDepth, value);
        }


        public DataDictionaryConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
