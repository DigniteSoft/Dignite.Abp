using System.Collections.Generic;

namespace Dignite.Abp.Settings
{
    public interface ISettingDefinitionManager:Volo.Abp.Settings.ISettingDefinitionManager
    {
        IList<SettingNavigation> GetNavigations();


        IReadOnlyList<Volo.Abp.Settings.SettingDefinition> GetAllOfNavigation(string navigationName);
    }
}
