using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Dignite.Abp.BlobStoring
{
    public class DigniteAbpBlobStoringAbstractionsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ICurrentBlobInfoAccessor>(AsyncLocalCurrentBlobInfoAccessor.Instance);
        }
    }
}
