using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public class HeaderFileEntityResolveContributor : HttpFileEntityResolveContributorBase
    {
        public const string ContributorName = "Header";

        public override string Name => ContributorName;

        protected override Task<FileEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IFileEntityResolveContext context, [NotNull] HttpContext httpContext)
        {
            if (httpContext.Request.Headers.IsNullOrEmpty())
            {
                return Task.FromResult((FileEntityResolveResult)null);
            }


            var entityTypeHeader = httpContext.Request.Headers[FileEntityResolverConsts.EntityTypeKey];
            var entityIdHeader = httpContext.Request.Headers[FileEntityResolverConsts.EntityIdKey];
            if (entityTypeHeader == string.Empty || entityTypeHeader.Count < 1 || entityIdHeader == string.Empty || entityIdHeader.Count < 1)
            {
                return Task.FromResult((FileEntityResolveResult)null);
            }

            if (entityTypeHeader.Count > 1 || entityIdHeader.Count>1)
            {
                Log(context, $"HTTP request includes more than one {FileEntityResolverConsts.EntityTypeKey} header value. First one will be used. All of them: {entityTypeHeader.JoinAsString(", ")}");
            }
            var result = new FileEntityResolveResult();
            result.EntityType = entityTypeHeader.First();
            result.EntityId = entityIdHeader.First();

            return Task.FromResult(result);
        }


        protected virtual void Log(IFileEntityResolveContext context, string text)
        {
            context
                .ServiceProvider
                .GetRequiredService<ILogger<HeaderFileEntityResolveContributor>>()
                .LogWarning(text);
        }
    }
}
