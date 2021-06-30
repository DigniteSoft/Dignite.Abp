
namespace Dignite.Abp.Settings
{
    public interface ISettingDefinitionContext:Volo.Abp.Settings.ISettingDefinitionContext
    {
        void Add(SettingNavigation navigation, params Volo.Abp.Settings.SettingDefinition[] definitions);
    }
}
