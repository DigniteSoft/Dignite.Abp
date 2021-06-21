using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Abp.BlobStoringManagement.EntityFrameworkCore
{
    public class BlobStoringManagementModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public BlobStoringManagementModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}