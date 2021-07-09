using System;

namespace Dignite.Abp.FieldCustomizing.TextboxForm
{
    public static class TextboxFormConfigurationExtensions
    {
        public static TextboxFormConfiguration GetTextboxConfiguration(
            this FormConfigurationData fieldConfiguration)
        {
            return new TextboxFormConfiguration(fieldConfiguration);
        }

        public static FormConfigurationData UseTextbox(
            this FormConfigurationData fieldConfiguration,
            Action<TextboxFormConfiguration> textboxFieldConfigureAction)
        {
            textboxFieldConfigureAction(new TextboxFormConfiguration(fieldConfiguration));
            return fieldConfiguration;
        }
    }
}
