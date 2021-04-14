using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring
{
    public static class NameGeneratorConfigurationExtensions
    {
        public static void SetNameGenerator<TNameGenerator>(
            this BlobContainerConfiguration containerConfiguration)
            where TNameGenerator:INameGenerator
        {
            containerConfiguration.SetConfiguration(
                DigniteAbpBlobContainerConfigurationNames.NamingGenerator,
                typeof(TNameGenerator));
        }
    }
}
