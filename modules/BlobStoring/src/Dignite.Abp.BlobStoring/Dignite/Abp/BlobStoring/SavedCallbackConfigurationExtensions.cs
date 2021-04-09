using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public static class SavedCallbackConfigurationExtensions
    {
        public static void SetSavedCallback<TSavedCallback>(
            this BlobContainerConfiguration containerConfiguration)
            where TSavedCallback : ISavedCallback
        {
            containerConfiguration.SetConfiguration(
                DigniteAbpBlobContainerConfigurationNames.SavedCallback,
                typeof(TSavedCallback));
        }
    }
}
