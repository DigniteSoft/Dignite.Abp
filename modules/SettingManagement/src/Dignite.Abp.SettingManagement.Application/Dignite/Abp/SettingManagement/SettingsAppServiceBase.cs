using Dignite.Abp.Settings;
using Dignite.Abp.FieldCustomizing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Dignite.Abp.SettingManagement
{
    public abstract class SettingsAppServiceBase : SettingManagementAppServiceBase, ISettingsAppService
    {
        protected readonly ISettingManager _settingManager;
        private readonly IEnumerable<ISettingNavigationProvider> _navigationProviders;
        private readonly IEnumerable<ICustomizeFieldFormProvider> _formProviders;

        protected SettingsAppServiceBase(
            ISettingManager settingManager,
            IEnumerable<ISettingNavigationProvider> navigationProviders,
            IEnumerable<ICustomizeFieldFormProvider> formProviders)
        {
            _settingManager = settingManager;
            _navigationProviders = navigationProviders;
            _formProviders = formProviders;
        }

        protected abstract ISettingValueProvider SettingValueProvider { get; }

        public Task<ListResultDto<SettingNavigationDto>> GetNavigationsAsync()
        {
            var navigations = new List<SettingNavigation>();
            foreach (var provider in _navigationProviders)
            {
                var navigation = provider.Navigation;
                navigations.Add(new SettingNavigation(navigation.Name, navigation.DisplayName));
            }

            return Task.FromResult(
                new ListResultDto<SettingNavigationDto>(
                    navigations.Select(nav => new SettingNavigationDto(
                        nav.Name,
                        nav.DisplayName.Localize(StringLocalizerFactory)
                        )).ToList()
                ));
        }

        public async Task<ListResultDto<SettingDto>> GetListAsync(string navigationName)
        {
            var settingDefinitions = GetSettingDefinitions(navigationName);
            var settingValues = await GetSettingValues();
            return Task.FromResult(
                new ListResultDto<SettingDto>(GetSettings(settingDefinitions, settingValues))
                ).Result;
        }

        public async Task UpdateAsync(UpdateSettingsInput input)
        {
            var settingDefinitions = GetSettingDefinitions(input.NavigationName);
            var settings = input.Settings.Where(s => 
                settingDefinitions.Select(sd => sd.Name)
                .Contains(s.Name)
            );

            foreach (var setting in settings)
            {
                await UpdateAsync(setting.Name, setting.Value);
            }
        }

        protected abstract Task<List<SettingValue>> GetSettingValues();

        protected abstract Task UpdateAsync(string name, string value);

        protected List<SettingDefinition> GetSettingDefinitions(string navigationName)
        {
            var navigationProvider = _navigationProviders.Single(np => np.Navigation.Name == navigationName);
            var settings = new Dictionary<string, SettingDefinition>();
            var groups = new List<SettingGroupDto>();

            using (var scope = ServiceProvider.CreateScope())
            {
                var provider = scope.ServiceProvider
                    .GetRequiredService(navigationProvider.GetType())
                    .As<ISettingDefinitionProvider>();

                provider.Define(new SettingDefinitionContext(settings));
            }

            return settings.Values
                .Where(s => !s.Providers.Any() || s.Providers.Contains(SettingValueProvider.Name))
                .ToList();
        }

        private IReadOnlyList<SettingDto> GetSettings (List<SettingDefinition> settingDefinitions,List<SettingValue> settingValues)
        {
            var settings = new List<SettingDto>();
            foreach (var sd in settingDefinitions)
            {
                var group = sd.GetGroup();
                var fieldConfiguration = sd.GetForm();
                settings.Add(new SettingDto(
                          group == null?null: group.Localize(StringLocalizerFactory),
                          sd.Name,
                          sd.DisplayName.Localize(StringLocalizerFactory),
                          sd.Description.Localize(StringLocalizerFactory),
                          settingValues.Single(sv=>sv.Name==sd.Name).Value,
                          fieldConfiguration.FormProviderName,
                          _formProviders.Single(fp => fp.FormProviderName == fieldConfiguration.FormProviderName).GetConfiguration(fieldConfiguration)
                          ));
            }
            return settings.ToImmutableList();
        }
    }
}
