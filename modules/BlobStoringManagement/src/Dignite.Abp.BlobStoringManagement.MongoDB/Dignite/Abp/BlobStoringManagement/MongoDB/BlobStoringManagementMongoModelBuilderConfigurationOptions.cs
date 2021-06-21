using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.BlobStoringManagement.MongoDB
{
    public class BlobStoringManagementMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public BlobStoringManagementMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}