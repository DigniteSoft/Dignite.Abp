using Dignite.Abp.Settings;
using Dignite.Abp.FieldCustomizing;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using ISettingDefinitionManager = Dignite.Abp.Settings.ISettingDefinitionManager;

namespace Dignite.Abp.SettingManagement
{
    public abstract class SettingsAppServiceBase : SettingManagementAppServiceBase, ISettingsAppService
    {
        protected ISettingDefinitionManager SettingDefinitionManager { get; }
        protected ISettingManager SettingManager { get; }
        protected IEnumerable<IFormProvider> FormProviders { get; }
        protected IFormProviderSelector FormProviderSelector;

        protected SettingsAppServiceBase(
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            IEnumerable<IFormProvider> formProviders)
        {
            SettingDefinitionManager = settingDefinitionManager;
            SettingManager = settingManager;
            FormProviders = formProviders;
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
                    && sd.GetFormOrNull()!=null
                    );
                if (settingDefinitions.Any())
                {
                    var settings = new List<SettingDto>();
                    foreach (var sd in settingDefinitions)
                    {
                        var group = sd.GetGroupOrNull();
                        var form = sd.GetFormOrNull();
                        settings.Add(new SettingDto(
                            group == null ? null : group.Localize(StringLocalizerFactory),
                            sd.Name,
                            sd.DisplayName.Localize(StringLocalizerFactory),
                            sd.Description.Localize(StringLocalizerFactory),
                            settingValues.Single(sv => sv.Name == sd.Name).Value,
                            form.FormProviderName,
                            FormProviderSelector.Get(form.FormProviderName).GetConfiguration(form)
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

        public async Task UpdateAsync( UpdateSettingsInput input)
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
