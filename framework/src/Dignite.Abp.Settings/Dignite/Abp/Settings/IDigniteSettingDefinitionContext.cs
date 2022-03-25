
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Settings
{
    public interface IDigniteSettingDefinitionContext:ISettingDefinitionContext
    {
        /// <summary>
        /// Navigation of settings
        /// </summary>
        public SettingNavigation Navigation { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        void SetNavigation(string name, ILocalizableString displayName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="definitions"></param>
        void Add(ILocalizableString group, params SettingDefinition[] definitions);
    }
}
