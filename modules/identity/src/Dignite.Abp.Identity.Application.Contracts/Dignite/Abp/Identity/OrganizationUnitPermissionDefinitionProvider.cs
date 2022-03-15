using Dignite.Abp.Identity.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Localization;

namespace Dignite.Abp.Identity
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IPermissionDefinitionProvider), typeof(IdentityPermissionDefinitionProvider), typeof(OrganizationUnitPermissionDefinitionProvider))]

    public class OrganizationUnitPermissionDefinitionProvider : IdentityPermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            base.Define(context);

            var identityGroup = context.GetGroup(IdentityPermissions.GroupName);

            var rolesPermission = identityGroup.AddPermission(OrganizationUnitPermissions.OrganizationUnits.Default, L("Permission:OrganizationUnitManagement"));
            rolesPermission.AddChild(OrganizationUnitPermissions.OrganizationUnits.Create, L("Permission:Create"));
            rolesPermission.AddChild(OrganizationUnitPermissions.OrganizationUnits.Update, L("Permission:Edit"));
            rolesPermission.AddChild(OrganizationUnitPermissions.OrganizationUnits.Delete, L("Permission:Delete"));
            rolesPermission.AddChild(OrganizationUnitPermissions.OrganizationUnits.SuperAuthorization, L("Permission:OrganizationUnitSuperAuthorization"));


            identityGroup
                .AddPermission(OrganizationUnitPermissions.OrganizationUnitLookup.Default, L("Permission:OrganizationUnitLookup"))
                .WithProviders(ClientPermissionValueProvider.ProviderName);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DigniteAbpIdentityResource>(name);
        }
    }
}
