using System.Collections.Generic;
using Volo.Abp.Localization;
using Volo.Abp.Settings;
using JetBrains.Annotations;

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

        public void SetNavigation(string name, ILocalizableString displayName = null)
        {
            this.Navigation = new SettingNavigation(name,displayName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="definitions"></param>
        public void Add([CanBeNull]ILocalizableString group=null, params SettingDefinition[] definitions)
        {
            if (group != null)
            {
                foreach (var definition in definitions)
                {
                    definition.SetGroup(group);
                }
            }

            base.Add(definitions);
        }
    }
}
