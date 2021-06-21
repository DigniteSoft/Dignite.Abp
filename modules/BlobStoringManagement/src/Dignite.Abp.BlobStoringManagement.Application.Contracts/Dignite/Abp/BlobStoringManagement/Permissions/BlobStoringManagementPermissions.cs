using Volo.Abp.Reflection;

namespace Dignite.Abp.BlobStoringManagement.Permissions
{
    public class BlobStoringManagementPermissions
    {
        public const string GroupName = "BlobStoringManagement";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(BlobStoringManagementPermissions));
        }
    }
}