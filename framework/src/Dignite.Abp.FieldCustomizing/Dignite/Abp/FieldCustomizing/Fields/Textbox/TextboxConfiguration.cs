using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.Textbox
{
    public class TextboxConfiguration:FieldConfigurationBase
    {
        [StringLength(256)]
        public string Placeholder
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(TextboxConfigurationNames.Placeholder, null);
            set => _fieldConfiguration.SetConfiguration(TextboxConfigurationNames.Placeholder, value);
        }

        [Required]
        public TextboxMode Mode
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(TextboxConfigurationNames.Mode, TextboxMode.SingleLine);
            set => _fieldConfiguration.SetConfiguration(TextboxConfigurationNames.Mode, value);
        }


        public int CharLimit
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(TextboxConfigurationNames.CharLimit, Mode== TextboxMode.SingleLine?256:1024);
            set => _fieldConfiguration.SetConfiguration(TextboxConfigurationNames.CharLimit, value);
        }


        public TextboxConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
