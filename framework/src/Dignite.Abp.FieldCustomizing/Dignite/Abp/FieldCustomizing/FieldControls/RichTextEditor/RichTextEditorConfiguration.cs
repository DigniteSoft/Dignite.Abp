using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.FieldCustomizing.FieldControls.RichTextEditor
{
    public class RichTextEditorConfiguration : FieldControlConfigurationBase
    {
        [StringLength(256)]
        public string Placeholder
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault<string>(RichTextEditorConfigurationNames.Placeholder, null);
            set => _fieldControlConfiguration.SetConfiguration(RichTextEditorConfigurationNames.Placeholder, value);
        }

        [Required]
        public RichTextEditorMode Mode
        {
            get => _fieldControlConfiguration.GetConfigurationOrDefault(RichTextEditorConfigurationNames.Mode, RichTextEditorMode.Classic);
            set => _fieldControlConfiguration.SetConfiguration(RichTextEditorConfigurationNames.Mode, value);
        }


        public RichTextEditorConfiguration(FieldControlConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
