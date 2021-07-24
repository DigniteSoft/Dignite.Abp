using System.Collections.Generic;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public interface IDigniteSettingDefinitionManager:ISettingDefinitionManager
    {
        IList<SettingNavigation> GetNavigations();


        SettingNavigation GetNavigation(string navigationName);

        SettingNavigation GetNavigationOrNull(string navigationName);
    }
}
