
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing.Fields.Select
{
    public class SelectConfiguration : FieldConfigurationBase
    {
        public string NullText
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(SelectConfigurationNames.NullText, null);
            set => _fieldConfiguration.SetConfiguration(SelectConfigurationNames.NullText, value);
        }
        public List<SelectListItem> Options
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(SelectConfigurationNames.Options, new List<SelectListItem>());
            set => _fieldConfiguration.SetConfiguration(SelectConfigurationNames.Options, value);
        }

        public bool Multiple
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(SelectConfigurationNames.Multiple, false);
            set => _fieldConfiguration.SetConfiguration(SelectConfigurationNames.Multiple, value);
        }


        public int? Size
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<int?>(SelectConfigurationNames.Size);
            set => _fieldConfiguration.SetConfiguration(SelectConfigurationNames.Size, value);
        }


        public SelectConfiguration(FieldConfigurationDictionary fieldConfiguration)
            : base(fieldConfiguration)
        {
        }
    }
}
