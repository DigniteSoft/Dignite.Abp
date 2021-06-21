using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public static class BlobInfoStoreConfigurationExtensions
    {
        public static void SetBlobInfoStore<TBlobInfoStore>(
            this BlobContainerConfiguration containerConfiguration)
            where TBlobInfoStore : IBlobInfoStore
        {
            containerConfiguration.SetConfiguration(
                DigniteAbpBlobContainerConfigurationNames.BlobInfoStore,
                typeof(TBlobInfoStore));
        }
    }
}
