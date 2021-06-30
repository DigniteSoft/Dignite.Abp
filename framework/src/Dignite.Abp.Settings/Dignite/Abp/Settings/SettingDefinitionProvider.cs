using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Settings
{
    public abstract class SettingDefinitionProvider : ISettingDefinitionProvider, ITransientDependency
    {
        public abstract void Define(ISettingDefinitionContext context);
    }
}