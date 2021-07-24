using System.Collections.Generic;
using System.Collections.Immutable;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;
using System.Linq;

namespace Dignite.Abp.Settings
{
    public abstract class DigniteSettingDefinitionProvider : SettingDefinitionProvider, IDigniteSettingDefinitionProvider, ITransientDependency
    {
        public abstract void Define(IDigniteSettingDefinitionContext context);

        public override void Define(ISettingDefinitionContext context)
        {
            var settings = new Dictionary<string, SettingDefinition>();
            Define(new DigniteSettingDefinitionContext(settings));


            context.Add(
                settings.Values.ToImmutableList().ToArray()
            );
        }
    }
}