using Shouldly;
using Xunit;

namespace Dignite.Abp.Settings
{
    public class ISettingDefinitionManager_Tests : SettingsTestBase
    {
        private readonly ISettingDefinitionManager _settingDefinitionManager;

        public ISettingDefinitionManager_Tests()
        {
            _settingDefinitionManager = GetRequiredService<ISettingDefinitionManager>();
        }

        [Fact]
        public void Should_Get_Test_Setting_Definition_Provider()
        {
            var navigations = _settingDefinitionManager.GetNavigations();
            navigations.ShouldNotBeNull();
        }
    }
}
