using System.Collections.Generic;

namespace Dignite.Abp.Settings
{
    public interface ISettingDefinitionManager:Volo.Abp.Settings.ISettingDefinitionManager
    {
        IList<SettingNavigation> GetNavigations();


        SettingNavigation GetNavigation(string navigationName);

        SettingNavigation GetNavigationOrNull(string navigationName);
    }
}
