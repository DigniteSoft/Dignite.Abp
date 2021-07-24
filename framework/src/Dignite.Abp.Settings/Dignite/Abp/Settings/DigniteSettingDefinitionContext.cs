using System.Collections.Generic;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public class DigniteSettingDefinitionContext : SettingDefinitionContext, IDigniteSettingDefinitionContext
    {
        public DigniteSettingDefinitionContext(Dictionary<string, SettingDefinition> settings)
            :base(settings)
        {
        }

        /// <summary>
        /// Navigation of settings
        /// </summary>
        public SettingNavigation Navigation { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigation"></param>
        /// <param name="definitions"></param>
        public void Add(SettingNavigation navigation, params SettingDefinition[] definitions)
        {
            base.Add(definitions);
            Navigation = navigation;
        }
    }
}
