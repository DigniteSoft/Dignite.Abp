using Volo.Abp.Reflection;

namespace Dignite.Abp.Identity
{
    public static class IdentityPermissions
    {
        public const string GroupName = "DigniteAbpIdentity";


        public static class OrganizationUnits
        {
            public const string Default = GroupName + ".OrganizationUnits";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string SuperAuthorization = Default + ".SuperAuthorization";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityPermissions));
        }
    }
}