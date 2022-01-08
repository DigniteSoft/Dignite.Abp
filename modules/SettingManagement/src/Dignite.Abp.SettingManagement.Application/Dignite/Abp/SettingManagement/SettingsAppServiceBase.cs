using Dignite.Abp.Settings;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using ISettingDefinitionManager = Dignite.Abp.Settings.IDigniteSettingDefinitionManager;
using Dignite.Abp.FieldCustomizing.FieldControls;

namespace Dignite.Abp.SettingManagement
{
    public abstract class SettingsAppServiceBase : SettingManagementAppServiceBase
    {
        protected ISettingDefinitionManager SettingDefinitionManager { get; }
        protected ISettingManager SettingManager { get; }
        protected IEnumerable<IFieldControlProvider> ControlProviders { get; }

        protected SettingsAppServiceBase(
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            IEnumerable<IFieldControlProvider> controlProviders)
        {
            SettingDefinitionManager = settingDefinitionManager;
            SettingManager = settingManager;
            ControlProviders = controlProviders;
        }


        public async Task<ListResultDto<SettingNavigationDto>> GetAllAsync()
        {
            var navigations = SettingDefinitionManager.GetNavigations();
            var settingValues = await GetSettingValues();
            var navigationList = new List<SettingNavigationDto>();
            foreach (var nav in navigations)
            {
                var settingDefinitions = nav.SettingDefinitions.Where(sd => 
                    settingValues.Any(sv => sv.Name == sd.Name)
                    && sd.GetFieldControlConfigurationOrNull()!=null
                    ).ToList();
                if (settingDefinitions.Any())
                {
                    var settings = new List<SettingDto>();
                    foreach (var sd in settingDefinitions)
                    {
                        var group = sd.GetGroupOrNull();
                        settings.Add(new SettingDto(
                            group == null ? null : group.Localize(StringLocalizerFactory),
                            sd.Name,
                            sd.DisplayName.Localize(StringLocalizerFactory),
                            sd.Description==null?null:sd.Description.Localize(StringLocalizerFactory),
                            settingValues.Single(sv => sv.Name == sd.Name).Value,
                            sd.GetFieldControlProviderNameOrNull(),
                            sd.GetFieldControlConfigurationOrNull()
                            ));
                    }

                    navigationList.Add(new SettingNavigationDto(
                        nav.Name,
                        nav.DisplayName.Localize(StringLocalizerFactory),
                        settings
                        ));
                }
            }

            return 
                new ListResultDto<SettingNavigationDto>(navigationList);
        }

        protected async Task UpdateAsync(UpdateSettingsInput input)
        {
            var navigation = SettingDefinitionManager.GetNavigation(input.NavigationName);
            var settingDefinitions = navigation.SettingDefinitions;
            var settings = input.CustomizedFields.Where(s => 
                settingDefinitions.Select(sd => sd.Name).Contains(s.Key)
            );

            foreach (var setting in settings)
            {
                await UpdateAsync(setting.Key, setting.Value.ToString());
            }
        }

        protected abstract Task<List<SettingValue>> GetSettingValues();

        protected abstract Task UpdateAsync(string name, string value);
    }
}
