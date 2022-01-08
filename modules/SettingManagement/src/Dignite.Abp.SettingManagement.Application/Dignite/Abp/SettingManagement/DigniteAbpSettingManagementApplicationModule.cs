﻿
using Dignite.Abp.Settings;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(DigniteAbpSettingManagementApplicationContractsModule),
        typeof(DigniteAbpSettingsModule)
    )]
    public class DigniteAbpSettingManagementApplicationModule : AbpModule
    {
    }
}
