using Volo.Abp.Reflection;

namespace Dignite.Abp.SettingManagement
{
    public class SettingManagementPermissions
    {
        public const string GroupName = "SettingManagement";

        public const string Global = GroupName + ".Global";

        public const string Tenant = GroupName + ".Tenant";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(SettingManagementPermissions));
        }
    }
}
