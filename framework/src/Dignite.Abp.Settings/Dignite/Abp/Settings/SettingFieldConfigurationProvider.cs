using Dignite.FieldCustomizing;
using System.Threading.Tasks;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public class SettingFieldConfigurationProvider : IFieldConfigurationProvider
    {
        private readonly ISettingDefinitionManager _settingManager;

        public SettingFieldConfigurationProvider(ISettingDefinitionManager settingManager)
        {
            _settingManager = settingManager;
        }

        public Task<FieldConfiguration> Get(string name,object fieldDefinitionsSource=null)
        {
            var setting = _settingManager.Get(name);
            return Task.FromResult( setting.GetField());
        }
    }
}
