using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Dignite.Abp.FileManagement
{
    public class QueryStringBlobEntityResolveContributor : HttpBlobEntityResolveContributorBase
    {
        public const string ContributorName = "QueryString";

        public override string Name => ContributorName;

        protected override Task<BlobEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IBlobEntityResolveContext context, [NotNull] HttpContext httpContext)
        {
            var result = new BlobEntityResolveResult();
            if (httpContext.Request.QueryString.HasValue
                && httpContext.Request.Query.ContainsKey(BlobEntityResolverConsts.EntityTypeKey)
                && httpContext.Request.Query.ContainsKey(BlobEntityResolverConsts.EntityIdKey))
            {
                result.EntityType = httpContext.Request.Query[BlobEntityResolverConsts.EntityTypeKey].ToString();
                result.EntityId = httpContext.Request.Query[BlobEntityResolverConsts.EntityIdKey].ToString();

                return Task.FromResult(result);
            }

            return Task.FromResult(result);
        }
    }
}
