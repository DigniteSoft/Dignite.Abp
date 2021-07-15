using System;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Dignite.Abp.Identity
{
    public static class OrganizationUnitExtensions
    {

        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                ObjectExtensionManager.Instance
                  .AddOrUpdate<OrganizationUnit>(options =>
                    {
                        options.AddOrUpdateProperty<bool>(OrganizationUnitExtraPropertyNames.IsActiveName);
                        options.AddOrUpdateProperty<int>(OrganizationUnitExtraPropertyNames.PositionName);
                    }
                );
            });
        }
    }
}
