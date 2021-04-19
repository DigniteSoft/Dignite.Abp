using Dignite.Abp.Settings.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.SettingManagement
{
    public class SettingManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(SettingManagementPermissions.GroupName, L("Permission:SettingManagement"));
            moduleGroup.AddPermission(SettingManagementPermissions.Global, L("Permission:Global"), multiTenancySide: MultiTenancySides.Host);
            moduleGroup.AddPermission(SettingManagementPermissions.Tenant, L("Permission:Tenant"), multiTenancySide: MultiTenancySides.Tenant);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DigniteAbpSettingsResource>(name);
        }
    }
}
