using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.Upload
{
    public class UploadConfiguration:FieldConfigurationBase
    {
        [StringLength(256)]
        public string Placeholder
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(UploadConfigurationNames.Placeholder, null);
            set => _fieldConfiguration.SetConfiguration(UploadConfigurationNames.Placeholder, value);
        }

        [Required]
        public bool Multiple
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(UploadConfigurationNames.Multiple, false);
            set => _fieldConfiguration.SetConfiguration(UploadConfigurationNames.Multiple, value);
        }


        [Required]
        public string Filter
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(UploadConfigurationNames.Filter);
            set => _fieldConfiguration.SetConfiguration(UploadConfigurationNames.Filter, value);
        }

        public UploadConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
