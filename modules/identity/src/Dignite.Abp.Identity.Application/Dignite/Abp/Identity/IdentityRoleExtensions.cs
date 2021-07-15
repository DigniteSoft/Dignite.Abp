using System;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Dignite.Abp.Identity
{
    public static class IdentityRoleExtensions
    {

        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                  ObjectExtensionManager.Instance
                    .AddOrUpdateProperty<IdentityRole, Guid>(IdentityRoleExtraPropertyNames.ParentIdName);
            });
        }
    }
}
