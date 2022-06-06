using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.FieldControls.Upload
{
    public class UploadConfiguration:FieldControlConfigurationBase
    {
        [StringLength(256)]
        public string Placeholder
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<string>(UploadConfigurationNames.Placeholder, null);
            set => _fieldControlConfiguration.SetConfiguration(UploadConfigurationNames.Placeholder, value);
        }

        [Required]
        public bool Mode
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(UploadConfigurationNames.Multiple, false);
            set => _fieldControlConfiguration.SetConfiguration(UploadConfigurationNames.Multiple, value);
        }


        [Required]
        public string Filter
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<string>(UploadConfigurationNames.Filter);
            set => _fieldControlConfiguration.SetConfiguration(UploadConfigurationNames.Filter, value);
        }

        public UploadConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
