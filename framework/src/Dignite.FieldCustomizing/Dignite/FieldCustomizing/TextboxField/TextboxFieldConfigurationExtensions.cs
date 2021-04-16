using System;

namespace Dignite.FieldCustomizing.TextboxField
{
    public static class TextboxFieldConfigurationExtensions
    {
        public static TextboxFieldProviderConfiguration GetTextboxFieldConfiguration(
            this FieldConfiguration fieldConfiguration)
        {
            return new TextboxFieldProviderConfiguration(fieldConfiguration);
        }

        public static FieldConfiguration UseTextboxField(
            this FieldConfiguration fieldConfiguration,
            Action<TextboxFieldProviderConfiguration> textboxFieldConfigureAction)
        {
            fieldConfiguration.ProviderName = TextboxFieldProviderConfigurationNames.ProviderName;

            textboxFieldConfigureAction(new TextboxFieldProviderConfiguration(fieldConfiguration));

            return fieldConfiguration;
        }
    }
}
