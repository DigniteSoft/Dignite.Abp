
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public interface IDigniteSettingDefinitionContext:ISettingDefinitionContext
    {
        void Add(SettingNavigation navigation, params SettingDefinition[] definitions);
    }
}
