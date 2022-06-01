
namespace Dignite.Abp.FieldCustomizing.FieldControls.DataDictionary
{
    public class DataDictionaryConfiguration:FieldControlConfigurationBase
    {

        /// <summary>
        /// Maximum depth of an DataDictionary hierarchy.
        /// </summary>
        public int MaxDepth
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<int>(DataDictionaryConfigurationNames.MaxDepth, 1);
            set => _fieldControlConfiguration.SetConfiguration(DataDictionaryConfigurationNames.MaxDepth, value);
        }


        public DataDictionaryConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
