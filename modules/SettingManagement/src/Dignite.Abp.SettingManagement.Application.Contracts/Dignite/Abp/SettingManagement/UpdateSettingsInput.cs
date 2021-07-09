using Dignite.Abp.FieldCustomizing;
using Dignite.Abp.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dignite.Abp.SettingManagement
{
    public class UpdateSettingsInput:CustomizableObject
    {
        [Required]
        public string NavigationName { get; set; }

        public override IReadOnlyList<BasicCustomizeFieldDefinition> GetFieldDefinitions(ValidationContext validationContext)
        {
            var stringLocalizerFactory = validationContext.GetRequiredService<IStringLocalizerFactory>();
            var settingDefinitionManager = validationContext.GetRequiredService<ISettingDefinitionManager>();
            return settingDefinitionManager.GetNavigation(NavigationName).SettingDefinitions
                .Select(fd => new BasicCustomizeFieldDefinition(
                        fd.Name,
                        fd.DisplayName.Localize(stringLocalizerFactory),
                        fd.DefaultValue,
                        fd.GetFormOrNull()
                        )).ToList();
        }
    }
}
