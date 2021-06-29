using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement.Localization;

namespace Dignite.Abp.SettingManagement
{
    public class SettingManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(SettingManagementPermissions.GroupName, L("Settings")); //L("Settings")：The multi language of Volo.ABP.Settingmanagement.Domain.Shared module is used
            moduleGroup.AddPermission(SettingManagementPermissions.Global, L("Settings"), multiTenancySide: MultiTenancySides.Host);    //Whether it is host or tenant, the permission name is L("Settings")
            moduleGroup.AddPermission(SettingManagementPermissions.Tenant, L("Settings"), multiTenancySide: MultiTenancySides.Tenant);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpSettingManagementResource>(name);
        }
    }
}
