using System.Collections.Generic;

namespace Dignite.Abp.Settings
{
    public class SettingDefinitionContext : Volo.Abp.Settings.SettingDefinitionContext, ISettingDefinitionContext
    {
        public SettingDefinitionContext(Dictionary<string, Volo.Abp.Settings.SettingDefinition> settings)
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
        public void Add(SettingNavigation navigation, params Volo.Abp.Settings.SettingDefinition[] definitions)
        {
            base.Add(definitions);
            Navigation = navigation;
        }
    }
}
