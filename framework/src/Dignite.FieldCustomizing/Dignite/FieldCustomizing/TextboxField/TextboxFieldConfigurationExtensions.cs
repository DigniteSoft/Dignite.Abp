using System;

namespace Dignite.FieldCustomizing.TextboxField
{
    public static class TextboxFieldConfigurationExtensions
    {
        public static TextboxFieldProviderConfiguration GetTextboxFieldConfiguration(
            this CustomizeFieldConfiguration fieldConfiguration)
        {
            return new TextboxFieldProviderConfiguration(fieldConfiguration);
        }

        public static CustomizeFieldConfiguration UseTextboxField(
            this CustomizeFieldConfiguration fieldConfiguration,
            Action<TextboxFieldProviderConfiguration> textboxFieldConfigureAction)
        {
            fieldConfiguration.ProviderName = TextboxFieldProvider.ProviderName;

            textboxFieldConfigureAction(new TextboxFieldProviderConfiguration(fieldConfiguration));

            return fieldConfiguration;
        }
    }
}
