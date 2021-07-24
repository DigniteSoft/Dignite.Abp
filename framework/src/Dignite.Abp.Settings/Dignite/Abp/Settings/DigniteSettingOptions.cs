
using Volo.Abp.Collections;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public class DigniteSettingOptions:AbpSettingOptions
    {
        public ITypeList<IDigniteSettingDefinitionProvider> DigniteDefinitionProviders { get; }

        public DigniteSettingOptions():base()
        {
            DigniteDefinitionProviders = new TypeList<IDigniteSettingDefinitionProvider>();
        }
    }
}
