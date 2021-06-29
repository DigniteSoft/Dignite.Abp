using System;

namespace Dignite.Abp.FieldCustomizing.TextboxForm
{
    public static class TextboxFormConfigurationExtensions
    {
        public static TextboxFormProviderConfiguration GetTextboxConfiguration(
            this CustomizeFieldFormConfiguration fieldConfiguration)
        {
            return new TextboxFormProviderConfiguration(fieldConfiguration);
        }

        public static CustomizeFieldFormConfiguration UseTextbox(
            this CustomizeFieldFormConfiguration fieldConfiguration,
            Action<TextboxFormProviderConfiguration> textboxFieldConfigureAction)
        {
            textboxFieldConfigureAction(new TextboxFormProviderConfiguration(fieldConfiguration));
            return fieldConfiguration;
        }
    }
}
