using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.FileManagement
{
    public class BlobEntityResolver : IBlobEntityResolver, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DigniteBlobEntityResolveOptions _options;

        public BlobEntityResolver(IOptions<DigniteBlobEntityResolveOptions> options, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public virtual async Task<BlobEntityResolveResult> ResolveBlobEntityAsync()
        {
            var result = new BlobEntityResolveResult();

            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new BlobEntityResolveContext(serviceScope.ServiceProvider);

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
