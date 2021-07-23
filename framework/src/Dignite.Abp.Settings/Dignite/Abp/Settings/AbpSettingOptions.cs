
using Volo.Abp.Collections;

namespace Dignite.Abp.Settings
{
    public class AbpSettingOptions:Volo.Abp.Settings.AbpSettingOptions
    {
        public new ITypeList<ISettingDefinitionProvider> DefinitionProviders { get; }

        public AbpSettingOptions():base()
        {
            DefinitionProviders = new TypeList<ISettingDefinitionProvider>();
        }
    }
}
