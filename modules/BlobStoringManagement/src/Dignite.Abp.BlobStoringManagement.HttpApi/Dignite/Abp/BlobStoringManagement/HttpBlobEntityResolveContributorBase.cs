using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Dignite.Abp.BlobStoringManagement
{
    public abstract class HttpBlobEntityResolveContributorBase : BlobEntityResolveContributorBase
    {
        public override async Task ResolveAsync(IBlobEntityResolveContext context)
        {
            var httpContext = context.GetHttpContext();
            if (httpContext == null)
            {
                return;
            }

            try
            {
                await ResolveFromHttpContextAsync(context, httpContext);
            }
            catch (Exception e)
            {
                context.ServiceProvider
                    .GetRequiredService<ILogger<HttpBlobEntityResolveContributorBase>>()
                    .LogWarning(e.ToString());
            }
        }

        protected virtual async Task ResolveFromHttpContextAsync(IBlobEntityResolveContext context, HttpContext httpContext)
        {

            var result = await GetBlobEntityFromHttpContextOrNullAsync(context, httpContext);
            if (result != null)
            {
                context.EntityType = result.EntityType;
                context.EntityId = result.EntityId;
            }
        }  

        protected abstract Task<BlobEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IBlobEntityResolveContext context, [NotNull] HttpContext httpContext);
    }
}
