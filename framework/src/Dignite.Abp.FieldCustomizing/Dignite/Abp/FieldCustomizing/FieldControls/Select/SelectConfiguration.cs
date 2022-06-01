
using System.Collections.Generic;

namespace Dignite.Abp.FieldCustomizing.FieldControls.Select
{
    public class SelectConfiguration : FieldControlConfigurationBase
    {
        public string NullText
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<string>(SelectConfigurationNames.NullText, null);
            set => _fieldControlConfiguration.SetConfiguration(SelectConfigurationNames.NullText, value);
        }
        public List<SelectListItem> Options
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(SelectConfigurationNames.Options, new List<SelectListItem>());
            set => _fieldControlConfiguration.SetConfiguration(SelectConfigurationNames.Options, value);
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
