using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.Fields.RichTextEditor
{
    public class RichTextEditorConfiguration : FieldConfigurationBase
    {
        [StringLength(256)]
        public string Placeholder
        {
            get => _fieldConfiguration.GetConfigurationOrDefault<string>(RichTextEditorConfigurationNames.Placeholder, null);
            set => _fieldConfiguration.SetConfiguration(RichTextEditorConfigurationNames.Placeholder, value);
        }

        [Required]
        public RichTextEditorMode Mode
        {
            get => _fieldConfiguration.GetConfigurationOrDefault(RichTextEditorConfigurationNames.Mode, RichTextEditorMode.Classic);
            set => _fieldConfiguration.SetConfiguration(RichTextEditorConfigurationNames.Mode, value);
        }


        public RichTextEditorConfiguration(FieldConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
