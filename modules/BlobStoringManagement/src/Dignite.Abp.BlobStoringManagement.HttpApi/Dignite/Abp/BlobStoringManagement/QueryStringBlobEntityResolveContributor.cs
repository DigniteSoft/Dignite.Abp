using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Dignite.Abp.BlobStoringManagement
{
    public class QueryStringBlobEntityResolveContributor : HttpBlobEntityResolveContributorBase
    {
        public const string ContributorName = "QueryString";

        public override string Name => ContributorName;

        protected override Task<BlobEntityResolveResult> GetBlobEntityFromHttpContextOrNullAsync([NotNull] IBlobEntityResolveContext context, [NotNull] HttpContext httpContext)
        {
            if(httpContext.Request.QueryString.HasValue 
                && httpContext.Request.Query.ContainsKey(BlobEntityResolverConsts.EntityTypeKey)
                && httpContext.Request.Query.ContainsKey(BlobEntityResolverConsts.EntityIdKey))
            {
                var result = new BlobEntityResolveResult();
                result.EntityType = httpContext.Request.Query[BlobEntityResolverConsts.EntityTypeKey].ToString();
                result.EntityId = httpContext.Request.Query[BlobEntityResolverConsts.EntityIdKey].ToString();

                return Task.FromResult(result);
            }

            return null;
        }
    }
}
