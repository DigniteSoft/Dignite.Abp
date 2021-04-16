using System.Collections.Generic;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public interface ISettingDefinitionManager<T>
        where T: ISettingDefinitionProvider
    {
        IReadOnlyList<SettingDefinition> GetAll();
    }
}
