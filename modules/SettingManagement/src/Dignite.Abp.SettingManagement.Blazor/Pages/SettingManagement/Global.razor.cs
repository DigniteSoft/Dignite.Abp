using Dignite.Abp.FieldCustomizing.Blazor;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Dignite.Abp.FieldCustomizing;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Abp.SettingManagement.Blazor.Pages.SettingManagement
{
    public partial class Global
    {
        [Inject]
        private IGlobalSettingsAppService settingsAppService { get; set; }

        [Inject]
        private IFieldControlComponentSelector fieldControlComponentSelector { get; set; }

        private IReadOnlyList<SettingNavigationDto> SettingNavigations;

        private List<FieldControlComponentParameter> fieldControlComponentParameters;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            SettingNavigations = (await settingsAppService.GetAllAsync()).Items;
            fieldControlComponentParameters = new List<FieldControlComponentParameter>();
            foreach (var settingNav in SettingNavigations)
            {
                foreach (var setting in settingNav.Settings)
                {
                    var parameter = new FieldControlComponentParameter(
                                        setting.Name,
                                        setting.DisplayName,
                                        setting.FieldControlProviderName,
                                        setting.Value,
                                        setting.FieldControlConfiguration
                                        );
                    fieldControlComponentParameters.Add(parameter);
                }
            }
        }

        private async Task SaveAsync()
        {
            var settingNav = SettingNavigations.Single(m => m.Name == "GeneralNavigation");
            var input = new UpdateGlobalSettingsInputForClientProxy(settingNav.Settings);
            input.NavigationName = settingNav.Name;
            input.CustomizedFields = new FieldCustomizing.CustomizeFieldDictionary();
            foreach (var setting in fieldControlComponentParameters)
            {
                input.CustomizedFields.Add(setting.Name, setting.Value);
            }

            await settingsAppService.UpdateAsync(input);
        }

        /// <summary>
        /// 
        /// </summary>
        private class UpdateGlobalSettingsInputForClientProxy : UpdateGlobalSettingsInput
        {
            IReadOnlyList<SettingDto> _settings;

            public UpdateGlobalSettingsInputForClientProxy(IReadOnlyList<SettingDto> settings)
            {
                _settings = settings;
            }

            public override IReadOnlyList<SettingDto> GetSettings(ValidationContext validationContext)
            {
                return _settings;
            }
        }
    }
}

