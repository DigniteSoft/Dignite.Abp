using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.BlobStoringManagement.MongoDB
{
    public static class BlobStoringManagementMongoDbContextExtensions
    {
        public static void ConfigureBlobStoringManagement(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new BlobStoringManagementMongoModelBuilderConfigurationOptions(
                BlobStoringManagementDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);

            builder.Entity<Blob>(x =>
            {
                x.CollectionName = BlobStoringManagementDbProperties.DbTablePrefix + "Blobs";
            });
        }
    }
}