using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public static class BlobNameGeneratorConfigurationExtensions
    {
        public static void SetNameGenerator<TNameGenerator>(
            this BlobContainerConfiguration containerConfiguration)
            where TNameGenerator:IBlobNameGenerator
        {
            containerConfiguration.SetConfiguration(
                DigniteAbpBlobContainerConfigurationNames.NamingGenerator,
                typeof(TNameGenerator));
        }
    }
}
