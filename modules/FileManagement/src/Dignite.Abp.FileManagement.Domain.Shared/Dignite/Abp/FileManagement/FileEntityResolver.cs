using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FileManagement
{
    public class FileEntityResolver : IFileEntityResolver, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DigniteFileEntityResolveOptions _options;

        public FileEntityResolver(IOptions<DigniteFileEntityResolveOptions> options, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public virtual async Task<FileEntityResolveResult> ResolveBlobEntityAsync()
        {
            var result = new FileEntityResolveResult();

            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new FileEntityResolveContext(serviceScope.ServiceProvider);

                foreach (var tenantResolver in _options.BlobEntityResolvers)
                {
                    await tenantResolver.ResolveAsync(context);

                    if (context.HasResolvedTenantOrHost())
                    {
                        result.EntityType = context.EntityType;
                        result.EntityId = context.EntityId;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
