using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public class QueryStringFileEntityResolveContributor : HttpFileEntityResolveContributorBase
    {
        public const string ContributorName = "QueryString";

        public override string Name => ContributorName;

        protected override Task<FileEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IFileEntityResolveContext context, [NotNull] HttpContext httpContext)
        {
            var result = new FileEntityResolveResult();
            if (httpContext.Request.QueryString.HasValue
                && httpContext.Request.Query.ContainsKey(FileEntityResolverConsts.EntityTypeKey)
                && httpContext.Request.Query.ContainsKey(FileEntityResolverConsts.EntityIdKey))
            {
                result.EntityType = httpContext.Request.Query[FileEntityResolverConsts.EntityTypeKey].ToString();
                result.EntityId = httpContext.Request.Query[FileEntityResolverConsts.EntityIdKey].ToString();

                return Task.FromResult(result);
            }

            return Task.FromResult(result);
        }
    }
}
