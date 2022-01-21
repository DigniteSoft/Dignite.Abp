
namespace Dignite.Abp.FieldCustomizing.FieldControls.Select
{
    public class SelectConfiguration : FieldControlConfigurationBase
    {
        public string Placeholder
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<string>(SelectConfigurationNames.Placeholder, null);
            set => _fieldControlConfiguration.SetConfiguration(SelectConfigurationNames.Placeholder, value);
        }

        public bool Multiple
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(SelectConfigurationNames.Multiple, false);
            set => _fieldControlConfiguration.SetConfiguration(SelectConfigurationNames.Multiple, value);
        }


        public int? Size
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<int?>(SelectConfigurationNames.Size);
            set => _fieldControlConfiguration.SetConfiguration(SelectConfigurationNames.Size, value);
        }


        public SelectConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
            : base(fieldConfiguration)
        {
        }
    }
}
