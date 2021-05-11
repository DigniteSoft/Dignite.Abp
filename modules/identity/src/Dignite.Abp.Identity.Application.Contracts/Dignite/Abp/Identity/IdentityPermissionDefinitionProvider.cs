using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;

namespace Dignite.Abp.Identity
{
    public class IdentityPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var identityGroup = context.AddGroup(IdentityPermissions.GroupName, L("Permission:IdentityManagement"));

            var rolesPermission = identityGroup.AddPermission(IdentityPermissions.OrganizationUnits.Default, L("Permission:OrganizationUnitManagement"));
            rolesPermission.AddChild(IdentityPermissions.OrganizationUnits.Create, L("Permission:Create"));
            rolesPermission.AddChild(IdentityPermissions.OrganizationUnits.Update, L("Permission:Edit"));
            rolesPermission.AddChild(IdentityPermissions.OrganizationUnits.Delete, L("Permission:Delete"));
            rolesPermission.AddChild(IdentityPermissions.OrganizationUnits.SuperAuthorization, L("Permission:SuperAuthorization"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
