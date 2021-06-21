using Dignite.Abp.BlobStoringManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Abp.BlobStoringManagement.Permissions
{
    public class BlobStoringManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(BlobStoringManagementPermissions.GroupName, L("Permission:BlobStoringManagement"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BlobStoringManagementResource>(name);
        }
    }
}