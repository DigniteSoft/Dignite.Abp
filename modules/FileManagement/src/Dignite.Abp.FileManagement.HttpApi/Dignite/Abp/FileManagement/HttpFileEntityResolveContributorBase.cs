using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Dignite.Abp.FileManagement
{
    public abstract class HttpFileEntityResolveContributorBase : FileEntityResolveContributorBase
    {
        public override async Task ResolveAsync(IFileEntityResolveContext context)
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
                    .GetRequiredService<ILogger<HttpFileEntityResolveContributorBase>>()
                    .LogWarning(e.ToString());
            }
        }

        protected virtual async Task ResolveFromHttpContextAsync(IFileEntityResolveContext context, HttpContext httpContext)
        {

            var result = await GetBlobEntityFromHttpContextOrNullAsync(context, httpContext);
            if (result != null)
            {
                context.EntityType = result.EntityType;
                context.EntityId = result.EntityId;
            }
        }  

        protected abstract Task<FileEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IFileEntityResolveContext context, [NotNull] HttpContext httpContext);
    }
}
