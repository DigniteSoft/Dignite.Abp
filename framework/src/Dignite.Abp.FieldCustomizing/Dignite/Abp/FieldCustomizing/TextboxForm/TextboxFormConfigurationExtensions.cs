using System;

namespace Dignite.Abp.FieldCustomizing.TextboxForm
{
    public static class TextboxFormConfigurationExtensions
    {
        public static TextboxFormConfiguration GetTextboxConfiguration(
            this CustomizeFieldFormConfiguration fieldConfiguration)
        {
            return new TextboxFormConfiguration(fieldConfiguration);
        }

        public static CustomizeFieldFormConfiguration UseTextbox(
            this CustomizeFieldFormConfiguration fieldConfiguration,
            Action<TextboxFormConfiguration> textboxFieldConfigureAction)
        {
            textboxFieldConfigureAction(new TextboxFormConfiguration(fieldConfiguration));
            return fieldConfiguration;
        }
    }
}
